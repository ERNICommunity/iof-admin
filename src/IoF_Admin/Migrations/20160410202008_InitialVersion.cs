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
                name: "Offices",
                columns: table => new
                {
                    OfficeID = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    City = table.Column<string>(nullable: false),
                    ContactPerson = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    CountryCode = table.Column<string>(maxLength: 2, nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.OfficeID);
                });

            migrationBuilder.CreateTable(
                name: "Aquariums",
                columns: table => new
                {
                    AquariumID = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    HardwareID = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OfficeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aquariums", x => x.AquariumID);
                    table.ForeignKey(
                        name: "FK_Aquariums_Offices_OfficeID",
                        column: x => x.OfficeID,
                        principalTable: "Offices",
                        principalColumn: "OfficeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fishes",
                columns: table => new
                {
                    FishID = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    AquariumID = table.Column<int>(nullable: false),
                    Channel = table.Column<int>(nullable: false),
                    OfficeID = table.Column<int>(nullable: false),
                    SecondsActive = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fishes", x => x.FishID);
                    table.ForeignKey(
                        name: "FK_Fishes_Aquariums_AquariumID",
                        column: x => x.AquariumID,
                        principalTable: "Aquariums",
                        principalColumn: "AquariumID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fishes_Offices_OfficeID",
                        column: x => x.OfficeID,
                        principalTable: "Offices",
                        principalColumn: "OfficeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aquariums_OfficeID",
                table: "Aquariums",
                column: "OfficeID");

            migrationBuilder.CreateIndex(
                name: "IX_Fishes_AquariumID",
                table: "Fishes",
                column: "AquariumID");

            migrationBuilder.CreateIndex(
                name: "IX_Fishes_OfficeID",
                table: "Fishes",
                column: "OfficeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fishes");

            migrationBuilder.DropTable(
                name: "Aquariums");

            migrationBuilder.DropTable(
                name: "Offices");
        }
    }
}
