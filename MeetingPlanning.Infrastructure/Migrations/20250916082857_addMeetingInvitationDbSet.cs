using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingPlanning.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addMeetingInvitationDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingInvitation_Meetings_MeetingId",
                table: "MeetingInvitation");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingInvitation_Users_UserId",
                table: "MeetingInvitation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeetingInvitation",
                table: "MeetingInvitation");

            migrationBuilder.RenameTable(
                name: "MeetingInvitation",
                newName: "MeetingInvitations");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingInvitation_UserId",
                table: "MeetingInvitations",
                newName: "IX_MeetingInvitations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingInvitation_MeetingId",
                table: "MeetingInvitations",
                newName: "IX_MeetingInvitations_MeetingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetingInvitations",
                table: "MeetingInvitations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingInvitations_Meetings_MeetingId",
                table: "MeetingInvitations",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingInvitations_Users_UserId",
                table: "MeetingInvitations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingInvitations_Meetings_MeetingId",
                table: "MeetingInvitations");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingInvitations_Users_UserId",
                table: "MeetingInvitations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeetingInvitations",
                table: "MeetingInvitations");

            migrationBuilder.RenameTable(
                name: "MeetingInvitations",
                newName: "MeetingInvitation");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingInvitations_UserId",
                table: "MeetingInvitation",
                newName: "IX_MeetingInvitation_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingInvitations_MeetingId",
                table: "MeetingInvitation",
                newName: "IX_MeetingInvitation_MeetingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetingInvitation",
                table: "MeetingInvitation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingInvitation_Meetings_MeetingId",
                table: "MeetingInvitation",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingInvitation_Users_UserId",
                table: "MeetingInvitation",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
