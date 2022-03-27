using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static Reec.Inspection.ReecEnums;

namespace Reec.Inspection
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public class ReecExceptionMiddleware<TDbContext>
            where TDbContext : InspectionDbContext
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ReecExceptionMiddleware<TDbContext>> _logger;
        private readonly TDbContext _dbContext;
        private readonly ReecExceptionOptions _reecExceptionOptions;

        public ReecExceptionMiddleware(RequestDelegate next,
                                        ILogger<ReecExceptionMiddleware<TDbContext>> logger,
                                        TDbContext dbContext,
                                        ReecExceptionOptions reecExceptionOptions)
        {
            this._next = next;
            this._logger = logger;
            //var scope = applicationBuilder.ApplicationServices.CreateScope();
            this._dbContext = dbContext;
            this._reecExceptionOptions = reecExceptionOptions;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                /*                 
                var tamanioMinimo = 1024 * 45; //Kb
                var tamanioMaximo = 1024 * 1024 * 15; //15Mb
                httpContext.Request.EnableBuffering(tamanioMinimo, tamanioMaximo);
                */

                httpContext.Request.EnableBuffering();
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }


        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {

            try
            {
                httpContext.Response.ContentType = "application/json";
                ReecMessage reecMessage = null;
                BeLogHttp beLogHttp;
                ReecException reecException = null;
                string exceptionMessage = null;
                //Error controlado
                if (exception.GetType() == typeof(ReecException))
                {
                    reecException = (ReecException)exception;
                    reecMessage = reecException.ReecMessage;
                    exceptionMessage = reecException.ExceptionMessage;

                    switch (reecMessage.Category)
                    {
                        case Category.OK:
                            httpContext.Response.StatusCode = (int)HttpStatusCode.OK; break;
                        case Category.PartialContent:
                            httpContext.Response.StatusCode = (int)HttpStatusCode.PartialContent; break;
                        case Category.Unauthorized:
                            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized; break;
                        case Category.Forbidden:
                            httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden; break;

                        case Category.Warning:
                        case Category.BusinessLogic:
                        case Category.BusinessLogicLegacy:
                            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest; break;
                        //httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound; break;

                        case Category.InternalServerError:
                            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError; break;
                        case Category.BadGateway:
                            httpContext.Response.StatusCode = (int)HttpStatusCode.BadGateway; break;
                        case Category.GatewayTimeout:
                            httpContext.Response.StatusCode = (int)HttpStatusCode.GatewayTimeout; break;
                        default:
                            break;
                    }
                }
                else
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    reecMessage = new ReecMessage(Category.InternalServerError, "Error no controlado del sistema.");
                }

                //Obtenemos datos de Error para guardarlo
                beLogHttp = await ErrorControlado(reecMessage, httpContext, exception, exceptionMessage);
                reecMessage.Path = beLogHttp.Path;
                reecMessage.TraceIdentifier = beLogHttp.TraceIdentifier;

                if (reecMessage.Category >= Category.Unauthorized)
                {

                    await _dbContext.Set<BeLogHttp>().AddAsync(beLogHttp);
                    var vResult = await _dbContext.SaveChangesAsync();
                    if (vResult > 0)
                        reecMessage.Id = beLogHttp.IdLogHttp;

                    if (reecMessage.Category >= Category.InternalServerError)
                        _logger.LogError(exception, string.Join("\n\r", reecMessage.Message));
                    else
                        _logger.LogWarning(string.Join("\n\r", reecMessage.Message));

                }

                var settings = new JsonSerializerSettings
                {
                    //ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    ContractResolver = new DefaultContractResolver(),
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                };

                var json = JsonConvert.SerializeObject(reecMessage, settings);

                await httpContext.Response.WriteAsync(json);

            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                _logger.LogError(ex, "Ocurrio un error al guardar log en Base de Datos.");
                var reecMessage = new ReecMessage(Category.InternalServerError, "Ocurrio un error al guardar log en Base de Datos.", httpContext.Request.Path.Value)
                {
                    TraceIdentifier = httpContext.TraceIdentifier
                };

                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver(),
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                };

                var json = JsonConvert.SerializeObject(reecMessage, settings);
                await httpContext.Response.WriteAsync(json);
            }

        }


        private async Task<BeLogHttp> ErrorControlado(ReecMessage reecMessage, HttpContext httpContext, Exception ex, string exceptionMessage)
        {

            httpContext.Request.Body.Position = 0;
            string RequestBody = null;
            //Aquí obtenemos la información que envio el cliente al servidor.
            if (httpContext.Request.Body.Length > 0)
            {
                using var sr = new StreamReader(httpContext.Request.Body);
                RequestBody = await sr.ReadToEndAsync();
            }

            var settings = new JsonSerializerSettings
            {
                //ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ContractResolver = new DefaultContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            BeLogHttp beLogHttp = new BeLogHttp
            {
                ApplicationName = _reecExceptionOptions.ApplicationName,
                Category = reecMessage.Category,
                CategoryDescription = reecMessage.Category.ToString(),
                HttpStatusCode = (HttpStatusCode)httpContext.Response.StatusCode,
                MessageUser = JsonConvert.SerializeObject(reecMessage.Message, settings),
                Path = httpContext.Request.Path.Value,
                Method = httpContext.Request.Method,
                Host = httpContext.Request.Host.Host,
                Port = httpContext.Request.Host.Port.GetValueOrDefault(),
                HostPort = httpContext.Request.Host.Value,
                Source = ex.Source,
                StackTrace = ex.StackTrace,
                RequestBody = RequestBody,
                ExceptionMessage = exceptionMessage,

                CreateDate = DateTime.Now,
                TraceIdentifier = httpContext.TraceIdentifier,
                IpAddress = httpContext.Connection.RemoteIpAddress.ToString(),
                ContentType = httpContext.Request.ContentType,
                Protocol = httpContext.Request.Protocol,
                Scheme = httpContext.Request.Scheme,
                IsHttps = httpContext.Request.IsHttps,

            };

            if (httpContext.User.Identity.IsAuthenticated)
                beLogHttp.CreateUser = httpContext.User.Identity.Name; //obtener claim name


            if (reecMessage.Category != Category.InternalServerError && string.IsNullOrWhiteSpace(exceptionMessage))
                beLogHttp.StackTrace = null;
            else if (reecMessage.Category == Category.InternalServerError)
                beLogHttp.ExceptionMessage = ex.Message;


            if (ex.InnerException != null)
                beLogHttp.InnerExceptionMessage = ex.InnerException.Message;


            var header = httpContext.Request.Headers.Select(t => new { t.Key, Value = t.Value.ToString() });
            if (_reecExceptionOptions.HeaderKeysInclude != null && _reecExceptionOptions.HeaderKeysInclude.Count > 0)
            {
                header = (from a in header
                          join b in _reecExceptionOptions.HeaderKeysInclude
                          on a.Key equals b
                          select a).ToList();
            }
            else if (_reecExceptionOptions.HeaderKeysExclude != null && _reecExceptionOptions.HeaderKeysExclude.Count > 0)
            {
                var exclude = (from a in header
                               join b in _reecExceptionOptions.HeaderKeysExclude
                               on a.Key equals b
                               select a).ToList();
                header = header.Except(exclude).ToList();
            }


            beLogHttp.RequestHeader = JsonConvert.SerializeObject(header, settings);
            return beLogHttp;
        }


    }



}
