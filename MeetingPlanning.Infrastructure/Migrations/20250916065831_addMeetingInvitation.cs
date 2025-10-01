using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingPlanning.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addMeetingInvitation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeetingInvitation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MeetingId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingInvitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingInvitation_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetingInvitation_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingInvitation_MeetingId",
                table: "MeetingInvitation",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingInvitation_UserId",
                table: "MeetingInvitation",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeetingInvitation");
        }
    }
}
