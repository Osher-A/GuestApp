using Microsoft.EntityFrameworkCore.Migrations;

namespace GuestApp.Migrations
{
    public partial class InsertingUsersIdInEventTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT Users (Email, Password, Id)
                                   VALUES ('oamoscovitch@gmail.com', '229865Oa','SjlIykYCcxQ5aK0FoDvuw1Veyuh2');
                                   INSERT Users (Email, Password, Id)
                                   VALUES ('c.spielman@hotmail.com', '229865Cs', 'dVwFHt0137SH7O6bMfeNxlZhIKT2');
                                   UPDATE Events
                                   SET UserId = 'SjlIykYCcxQ5aK0FoDvuw1Veyuh2'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}