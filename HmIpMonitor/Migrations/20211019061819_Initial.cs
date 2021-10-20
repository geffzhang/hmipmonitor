using Microsoft.EntityFrameworkCore.Migrations;

namespace HmIpMonitor.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HmIpDevice",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Channel = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Parameter = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HmIpDevice", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HmIpDevice");
        }
    }
}
