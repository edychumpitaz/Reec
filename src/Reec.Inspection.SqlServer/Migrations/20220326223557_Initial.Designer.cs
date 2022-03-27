﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Reec.Inspection.SqlServer;

namespace Reec.Inspection.SqlServer.Migrations
{
    [DbContext(typeof(DbContextSqlServer))]
    [Migration("20220326223557_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Reec.Inspection.BeLogHttp", b =>
                {
                    b.Property<int>("IdLogHttp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationName")
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("CategoryDescription")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ContentType")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("DateTime2(7)");

                    b.Property<string>("CreateUser")
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40)
                        .IsUnicode(false);

                    b.Property<string>("ExceptionMessage")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Host")
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("HostPort")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("HttpStatusCode")
                        .HasColumnType("int");

                    b.Property<string>("InnerExceptionMessage")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("IpAddress")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30);

                    b.Property<bool>("IsHttps")
                        .HasColumnType("bit");

                    b.Property<string>("MessageUser")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Method")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Path")
                        .HasColumnType("varchar(max)");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<string>("Protocol")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("RequestBody")
                        .HasColumnType("text");

                    b.Property<string>("RequestHeader")
                        .HasColumnType("text");

                    b.Property<string>("Scheme")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Source")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("StackTrace")
                        .HasColumnType("text");

                    b.Property<string>("TraceIdentifier")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("IdLogHttp");

                    b.ToTable("LogHttp");
                });
#pragma warning restore 612, 618
        }
    }
}