using Microsoft.EntityFrameworkCore.Migrations;

namespace HmIpMonitor.Migrations
{
    public partial class AddThresholdDirection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ValueThresholdDirectionRight",
                table: "DeviceParameter",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValueThresholdDirectionRight",
                table: "DeviceParameter");
        }
    }
}
