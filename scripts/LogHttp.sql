

CREATE TABLE [dbo].[LogHttp](
	[IdLogHttp] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](100) NULL,
	[Category] [int] NOT NULL,
	[CategoryDescription] [varchar](50) NULL,
	[HttpStatusCode] [int] NOT NULL,
	[MessageUser] [varchar](max) NULL,
	[ExceptionMessage] [varchar](max) NULL,
	[InnerExceptionMessage] [varchar](max) NULL,
	[Protocol] [varchar](50) NULL,
	[IsHttps] [bit] NOT NULL,
	[Method] [varchar](100) NULL,
	[Scheme] [varchar](30) NULL,
	[Host] [varchar](150) NULL,
	[Port] [int] NOT NULL,
	[HostPort] [varchar](200) NULL,
	[Path] [varchar](max) NULL,
	[Source] [varchar](200) NULL,
	[TraceIdentifier] [varchar](100) NULL,
	[ContentType] [varchar](100) NULL,
	[RequestHeader] [text] NULL,
	[RequestBody] [text] NULL,
	[StackTrace] [text] NULL,
	[IpAddress] [varchar](30) NULL,
	[CreateUser] [varchar](40) NULL,
	[CreateDate] [datetime2](7) NULL,
 CONSTRAINT [PK_LogHttp] PRIMARY KEY CLUSTERED 
(
	[IdLogHttp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


