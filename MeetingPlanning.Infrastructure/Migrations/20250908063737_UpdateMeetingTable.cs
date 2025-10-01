using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingPlanning.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMeetingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meeting_MeetingRooms_MeetingRoomId",
                table: "Meeting");

            migrationBuilder.DropForeignKey(
                name: "FK_Meeting_Users_UserId",
                table: "Meeting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meeting",
                table: "Meeting");

            migrationBuilder.RenameTable(
                name: "Meeting",
                newName: "Meetings");

            migrationBuilder.RenameIndex(
                name: "IX_Meeting_UserId",
                table: "Meetings",
                newName: "IX_Meetings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Meeting_MeetingRoomId",
                table: "Meetings",
                newName: "IX_Meetings_MeetingRoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meetings",
                table: "Meetings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_MeetingRooms_MeetingRoomId",
                table: "Meetings",
                column: "MeetingRoomId",
                principalTable: "MeetingRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Users_UserId",
                table: "Meetings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_MeetingRooms_MeetingRoomId",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Users_UserId",
                table: "Meetings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meetings",
                table: "Meetings");

            migrationBuilder.RenameTable(
                name: "Meetings",
                newName: "Meeting");

            migrationBuilder.RenameIndex(
                name: "IX_Meetings_UserId",
                table: "Meeting",
                newName: "IX_Meeting_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Meetings_MeetingRoomId",
                table: "Meeting",
                newName: "IX_Meeting_MeetingRoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meeting",
                table: "Meeting",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Meeting_MeetingRooms_MeetingRoomId",
                table: "Meeting",
                column: "MeetingRoomId",
                principalTable: "MeetingRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meeting_Users_UserId",
                table: "Meeting",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
