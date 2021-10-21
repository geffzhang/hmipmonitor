using Microsoft.EntityFrameworkCore.Migrations;

namespace HmIpMonitor.Migrations
{
    public partial class AddThresholds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ValueErrorThreshold",
                table: "DeviceParameter",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValueWarnThreshold",
                table: "DeviceParameter",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValueErrorThreshold",
                table: "DeviceParameter");

            migrationBuilder.DropColumn(
                name: "ValueWarnThreshold",
                table: "DeviceParameter");
        }
    }
}
