using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IoF_Admin.Migrations
{
    public partial class InitialVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Office",
                columns: table => new
                {
                    OfficeID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    City = table.Column<string>(nullable: false),
                    ContactPerson = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    CountryCode = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Office", x => x.OfficeID);
                });
            migrationBuilder.CreateTable(
                name: "Aquarium",
                columns: table => new
                {
                    AquariumID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HardwareID = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OfficeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aquarium", x => x.AquariumID);
                    table.ForeignKey(
                        name: "FK_Aquarium_Office_OfficeID",
                        column: x => x.OfficeID,
                        principalTable: "Office",
                        principalColumn: "OfficeID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Fish",
                columns: table => new
                {
                    FishID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AquariumID = table.Column<int>(nullable: false),
                    Channel = table.Column<int>(nullable: false),
                    OfficeID = table.Column<int>(nullable: false),
                    SecondsActive = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fish", x => x.FishID);
                    table.ForeignKey(
                        name: "FK_Fish_Aquarium_AquariumID",
                        column: x => x.AquariumID,
                        principalTable: "Aquarium",
                        principalColumn: "AquariumID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fish_Office_OfficeID",
                        column: x => x.OfficeID,
                        principalTable: "Office",
                        principalColumn: "OfficeID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Fish");
            migrationBuilder.DropTable("Aquarium");
            migrationBuilder.DropTable("Office");
        }
    }
}
