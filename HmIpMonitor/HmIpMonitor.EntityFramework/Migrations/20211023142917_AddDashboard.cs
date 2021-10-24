using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HmIpMonitor.EntityFramework.Migrations
{
    public partial class AddDashboard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dashboard",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dashboard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DashboardDeviceParameter",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DashboardId = table.Column<long>(type: "bigint", nullable: false),
                    DeviceParameterId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardDeviceParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DashboardDeviceParameter_Dashboard_DashboardId",
                        column: x => x.DashboardId,
                        principalTable: "Dashboard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DashboardDeviceParameter_DeviceParameter_DeviceParameterId",
                        column: x => x.DeviceParameterId,
                        principalTable: "DeviceParameter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DashboardDeviceParameter_DashboardId",
                table: "DashboardDeviceParameter",
                column: "DashboardId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardDeviceParameter_DeviceParameterId",
                table: "DashboardDeviceParameter",
                column: "DeviceParameterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DashboardDeviceParameter");

            migrationBuilder.DropTable(
                name: "Dashboard");
        }
    }
}
