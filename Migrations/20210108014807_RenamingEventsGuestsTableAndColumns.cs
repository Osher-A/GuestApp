using Microsoft.EntityFrameworkCore.Migrations;

namespace GuestApp.Migrations
{
    public partial class RenamingEventsGuestsTableAndColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventGuest_Events_EventsId",
                table: "EventGuest");

            migrationBuilder.DropForeignKey(
                name: "FK_EventGuest_Guests_GuestsId",
                table: "EventGuest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventGuest",
                table: "EventGuest");

            migrationBuilder.RenameTable(
                name: "EventGuest",
                newName: "EventsGuests");

            migrationBuilder.RenameColumn(
                name: "GuestsId",
                table: "EventsGuests",
                newName: "GuestId");

            migrationBuilder.RenameColumn(
                name: "EventsId",
                table: "EventsGuests",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_EventGuest_GuestsId",
                table: "EventsGuests",
                newName: "IX_EventsGuests_GuestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventsGuests",
                table: "EventsGuests",
                columns: new[] { "EventId", "GuestId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EventsGuests_Events_EventId",
                table: "EventsGuests",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventsGuests_Guests_GuestId",
                table: "EventsGuests",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventsGuests_Events_EventId",
                table: "EventsGuests");

            migrationBuilder.DropForeignKey(
                name: "FK_EventsGuests_Guests_GuestId",
                table: "EventsGuests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventsGuests",
                table: "EventsGuests");

            migrationBuilder.RenameTable(
                name: "EventsGuests",
                newName: "EventGuest");

            migrationBuilder.RenameColumn(
                name: "GuestId",
                table: "EventGuest",
                newName: "GuestsId");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "EventGuest",
                newName: "EventsId");

            migrationBuilder.RenameIndex(
                name: "IX_EventsGuests_GuestId",
                table: "EventGuest",
                newName: "IX_EventGuest_GuestsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventGuest",
                table: "EventGuest",
                columns: new[] { "EventsId", "GuestsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EventGuest_Events_EventsId",
                table: "EventGuest",
                column: "EventsId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventGuest_Guests_GuestsId",
                table: "EventGuest",
                column: "GuestsId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}