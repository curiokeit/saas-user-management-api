using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaasUserManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPlanToTenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlanName",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanName",
                table: "Tenants");
        }
    }
}
