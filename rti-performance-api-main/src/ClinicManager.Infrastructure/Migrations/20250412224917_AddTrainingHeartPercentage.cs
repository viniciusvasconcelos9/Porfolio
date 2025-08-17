using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainingHeartPercentage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TrainingHeartRateAndPercentage",
                table: "Metrics",
                newName: "TrainingHeartRate");

            migrationBuilder.AddColumn<string>(
                name: "TrainingHeartPercentage",
                table: "Metrics",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrainingHeartPercentage",
                table: "Metrics");

            migrationBuilder.RenameColumn(
                name: "TrainingHeartRate",
                table: "Metrics",
                newName: "TrainingHeartRateAndPercentage");
        }
    }
}
