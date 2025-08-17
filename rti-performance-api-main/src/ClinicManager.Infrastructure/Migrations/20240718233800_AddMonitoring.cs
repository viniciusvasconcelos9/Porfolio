using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMonitoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Monitorings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PreExerciseBloodPressure = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PostExerciseBloodPressure = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PreExerciseSpo2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PreExerciseHeartRate = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DuringExerciseHeartRate = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PostExerciseHeartRate = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BloodGlucose = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TrainingHeartRateAndPercentage = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PerceivedExertion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Observations = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitorings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Monitorings_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Monitorings_ClientId",
                table: "Monitorings",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Monitorings");
        }
    }
}
