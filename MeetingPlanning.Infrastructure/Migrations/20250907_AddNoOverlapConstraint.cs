using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Infrastructure.Migrations
{
    public partial class _20250907_AddNoOverlapConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE EXTENSION IF NOT EXISTS btree_gist;");

            // Null EndTime olan satırları doldur
            migrationBuilder.Sql(@"
        UPDATE ""Meetings""
        SET ""EndTime"" = ""StartTime"" + ""Duration""
        WHERE ""EndTime"" IS NULL;
    ");

            migrationBuilder.Sql(@"
        ALTER TABLE ""Meetings""
        ADD CONSTRAINT no_overlapping_meetings
        EXCLUDE USING gist (
            ""MeetingRoomId"" WITH =,
            tsrange(""StartTime"", ""EndTime"", '[]') WITH &&
        );
    ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE ""Meetings"" DROP CONSTRAINT IF EXISTS no_overlapping_meetings;");
        }

    }
}
