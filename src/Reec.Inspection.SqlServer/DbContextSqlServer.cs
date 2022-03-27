using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Reec.Inspection.SqlServer
{
    public class DbContextSqlServer : InspectionDbContext
    {
        private readonly ReecExceptionOptions _reecExceptionOptions;

        public DbContextSqlServer([NotNull] DbContextOptions<DbContextSqlServer> options,
            ReecExceptionOptions reecExceptionOptions) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.AutoDetectChangesEnabled = true;
            this._reecExceptionOptions = reecExceptionOptions;
        }

        //public DbContextSqlServer(ReecExceptionOptions reecExceptionOptions)
        //{
        //    ChangeTracker.LazyLoadingEnabled = false;
        //    ChangeTracker.AutoDetectChangesEnabled = true;
        //    this._reecExceptionOptions = reecExceptionOptions;
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BeLogHttp>(entity =>
            {

                if (_reecExceptionOptions == null)
                    entity.ToTable("LogHttp");
                else
                {
                    if (_reecExceptionOptions != null && string.IsNullOrWhiteSpace(_reecExceptionOptions.Schema))
                        entity.ToTable(_reecExceptionOptions.TableName);
                    else
                        entity.ToTable(_reecExceptionOptions.TableName, _reecExceptionOptions.Schema);
                }

                entity.HasKey(e => e.IdLogHttp);
                entity.Property(e => e.IdLogHttp).ValueGeneratedOnAdd();
                entity.Property(e => e.ApplicationName).HasColumnType("varchar(100)");
                entity.Property(e => e.CategoryDescription).HasMaxLength(50).HasColumnType("varchar(50)");
                entity.Property(e => e.MessageUser).HasColumnType("varchar(max)");
                entity.Property(e => e.ExceptionMessage).HasColumnType("varchar(max)");
                entity.Property(e => e.InnerExceptionMessage).HasColumnType("varchar(max)");
                entity.Property(e => e.Protocol).HasColumnType("varchar(50)");
                entity.Property(e => e.Method).HasMaxLength(100).HasColumnType("varchar(100)");
                entity.Property(e => e.Scheme).HasMaxLength(30).HasColumnType("varchar(30)");
                entity.Property(e => e.Host).HasMaxLength(150).HasColumnType("varchar(150)");
                entity.Property(e => e.HostPort).HasMaxLength(200).HasColumnType("varchar(200)");
                entity.Property(e => e.Path).HasColumnType("varchar(max)");
                entity.Property(e => e.Source).HasMaxLength(200).HasColumnType("varchar(200)");
                entity.Property(e => e.TraceIdentifier).HasMaxLength(100).HasColumnType("varchar(100)");
                entity.Property(e => e.ContentType).HasMaxLength(100).HasColumnType("varchar(100)");
                entity.Property(e => e.RequestHeader).HasColumnType("text");
                entity.Property(e => e.RequestBody).HasColumnType("text");
                entity.Property(e => e.StackTrace).HasColumnType("text");
                entity.Property(e => e.IpAddress).HasMaxLength(30).HasColumnType("varchar(30)");
                entity.Property(e => e.CreateUser).HasMaxLength(40).IsUnicode(false);
                entity.Property(e => e.CreateDate).HasColumnType("DateTime2(7)");

                //entity.Property(e => e.UpdateDate).HasColumnType("DateTime2(7)");
                //entity.Property(e => e.UpdateUser).HasMaxLength(40).IsUnicode(false);

            });

            base.OnModelCreating(modelBuilder);

        }


    }


}
