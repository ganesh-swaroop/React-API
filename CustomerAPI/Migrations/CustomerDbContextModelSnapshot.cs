﻿// <auto-generated />
using System;
using CustomerAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CustomerAPI.Migrations
{
    [DbContext(typeof(CustomerDbContext))]
    partial class CustomerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CustomerAPI.Models.CustomerDef", b =>
                {
                    b.Property<string>("CustomerID")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("AddrLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddrLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddrLine3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AlertChannel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CegidDataPost")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CityID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerCD")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("DateOfBirth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Facebook")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FaxNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("GooglePlus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsActive")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsSysCust")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("MobileNumber")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ModID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SPID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salutation")
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<string>("ShortName")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("StateID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Twitter")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerID");

                    b.ToTable("CustomerDef");
                });
#pragma warning restore 612, 618
        }
    }
}
