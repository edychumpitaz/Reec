using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reec.Inspection.SqlServer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogHttp",
                columns: table => new
                {
                    IdLogHttp = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationName = table.Column<string>(type: "varchar(100)", nullable: true),
                    Category = table.Column<int>(nullable: false),
                    CategoryDescription = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    HttpStatusCode = table.Column<int>(nullable: false),
                    MessageUser = table.Column<string>(type: "varchar(max)", nullable: true),
                    ExceptionMessage = table.Column<string>(type: "varchar(max)", nullable: true),
                    InnerExceptionMessage = table.Column<string>(type: "varchar(max)", nullable: true),
                    Path = table.Column<string>(type: "varchar(max)", nullable: true),
                    Method = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Host = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Port = table.Column<int>(nullable: false),
                    HostPort = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Source = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    TraceIdentifier = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    ContentType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    RequestHeader = table.Column<string>(type: "text", nullable: true),
                    RequestBody = table.Column<string>(type: "text", nullable: true),
                    StackTrace = table.Column<string>(type: "text", nullable: true),
                    IpAddress = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    CreateUser = table.Column<string>(unicode: false, maxLength: 40, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "DateTime2(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogHttp", x => x.IdLogHttp);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogHttp");
        }
    }
}
