using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotaStat.Data.EntityFramework.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Heroes",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeekPatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekId = table.Column<int>(type: "int", nullable: false),
                    Patch = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekPatches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    SteamId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MyProperty = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.SteamId);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_Role",
                        column: x => x.Role,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrentWinrateAllies",
                columns: table => new
                {
                    MainHero = table.Column<byte>(type: "tinyint", nullable: false),
                    ComparedHero = table.Column<byte>(type: "tinyint", nullable: false),
                    WeekPatchId = table.Column<int>(type: "int", nullable: false),
                    WinsOfMain = table.Column<int>(type: "int", nullable: false),
                    LosesOfMain = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentWinrateAllies", x => new { x.MainHero, x.ComparedHero });
                    table.ForeignKey(
                        name: "FK_CurrentWinrateAllies_Heroes_ComparedHero",
                        column: x => x.ComparedHero,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrentWinrateAllies_Heroes_MainHero",
                        column: x => x.MainHero,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrentWinrateAllies_WeekPatches_WeekPatchId",
                        column: x => x.WeekPatchId,
                        principalTable: "WeekPatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrentWinrateEnemies",
                columns: table => new
                {
                    MainHero = table.Column<byte>(type: "tinyint", nullable: false),
                    ComparedHero = table.Column<byte>(type: "tinyint", nullable: false),
                    WeekPatchId = table.Column<int>(type: "int", nullable: false),
                    WinsOfMain = table.Column<int>(type: "int", nullable: false),
                    LosesOfMain = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentWinrateEnemies", x => new { x.MainHero, x.ComparedHero });
                    table.ForeignKey(
                        name: "FK_CurrentWinrateEnemies_Heroes_ComparedHero",
                        column: x => x.ComparedHero,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrentWinrateEnemies_Heroes_MainHero",
                        column: x => x.MainHero,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrentWinrateEnemies_WeekPatches_WeekPatchId",
                        column: x => x.WeekPatchId,
                        principalTable: "WeekPatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeeklyHeroWinRates",
                columns: table => new
                {
                    WeekPatchId = table.Column<int>(type: "int", nullable: false),
                    HeroId = table.Column<byte>(type: "tinyint", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    AllGames = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyHeroWinRates", x => new { x.WeekPatchId, x.HeroId });
                    table.ForeignKey(
                        name: "FK_WeeklyHeroWinRates_Heroes_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeeklyHeroWinRates_WeekPatches_WeekPatchId",
                        column: x => x.WeekPatchId,
                        principalTable: "WeekPatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrentWinrateAllies_ComparedHero",
                table: "CurrentWinrateAllies",
                column: "ComparedHero");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentWinrateAllies_WeekPatchId",
                table: "CurrentWinrateAllies",
                column: "WeekPatchId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentWinrateEnemies_ComparedHero",
                table: "CurrentWinrateEnemies",
                column: "ComparedHero");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentWinrateEnemies_WeekPatchId",
                table: "CurrentWinrateEnemies",
                column: "WeekPatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role",
                table: "Users",
                column: "Role");

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyHeroWinRates_HeroId",
                table: "WeeklyHeroWinRates",
                column: "HeroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentWinrateAllies");

            migrationBuilder.DropTable(
                name: "CurrentWinrateEnemies");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WeeklyHeroWinRates");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Heroes");

            migrationBuilder.DropTable(
                name: "WeekPatches");
        }
    }
}
