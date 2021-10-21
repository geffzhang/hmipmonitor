using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HmIpMonitor.EntityFramework.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HmIpDevice",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HmIpDevice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceParameter",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Channel = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Parameter = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ValueWarnThreshold = table.Column<double>(type: "double precision", nullable: false),
                    ValueErrorThreshold = table.Column<double>(type: "double precision", nullable: false),
                    ValueThresholdDirectionRight = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceParameter_HmIpDevice_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "HmIpDevice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DataPoint",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceParameterId = table.Column<long>(type: "bigint", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    Quality = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataPoint_DeviceParameter_DeviceParameterId",
                        column: x => x.DeviceParameterId,
                        principalTable: "DeviceParameter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataPoint_DeviceParameterId",
                table: "DataPoint",
                column: "DeviceParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceParameter_DeviceId",
                table: "DeviceParameter",
                column: "DeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataPoint");

            migrationBuilder.DropTable(
                name: "DeviceParameter");

            migrationBuilder.DropTable(
                name: "HmIpDevice");
        }
    }
}
