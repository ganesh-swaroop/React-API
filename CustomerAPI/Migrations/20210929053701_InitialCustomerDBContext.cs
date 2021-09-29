using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerAPI.Migrations
{
    public partial class InitialCustomerDBContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "commonCodes",
                columns: table => new
                {
                    CodeType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CMCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CDDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSysParam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commonCodes", x => new { x.CodeType, x.CMCode });
                });

            migrationBuilder.CreateTable(
                name: "customerDef",
                columns: table => new
                {
                    CustomerID = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CustomerCD = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Salutation = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ShortName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    DateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddrLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddrLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddrLine3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GooglePlus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SPID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlertChannel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSysCust = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CegidDataPost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customerDef", x => x.CustomerID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "commonCodes");

            migrationBuilder.DropTable(
                name: "customerDef");
        }
    }
}
