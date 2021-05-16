using Microsoft.EntityFrameworkCore.Migrations;

namespace GuestApp.Migrations
{
    public partial class PopulatingCityAndRegionTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT Cities (Name) VALUES('Antwerp')");
            migrationBuilder.Sql("INSERT Cities (Name) VALUES('Basel')");
            migrationBuilder.Sql("INSERT Cities (Name) VALUES('Gateshead')");
            migrationBuilder.Sql("INSERT Cities (Name) VALUES('London')");
            migrationBuilder.Sql("INSERT Cities (Name) VALUES('Manchester')");
            migrationBuilder.Sql("INSERT Cities (Name) VALUES('Zurich')");

            migrationBuilder.Sql("INSERT Regions (Name) VALUES ('NA')");
            migrationBuilder.Sql("INSERT Regions (Name) VALUES ('Edgware')");
            migrationBuilder.Sql("INSERT Regions (Name) VALUES ('Hendon')");
            migrationBuilder.Sql("INSERT Regions (Name) VALUES ('Golders Green')");
            migrationBuilder.Sql("INSERT Regions (Name) VALUES ('Prestwich')");
            migrationBuilder.Sql("INSERT Regions (Name) VALUES ('Salford')");
            migrationBuilder.Sql("INSERT Regions (Name) VALUES ('Stamford Hill')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Cities WHERE Id Between 1 AND 6");
            migrationBuilder.Sql("DELETE FROM Regions WHERE Id BETWEEN 0 AND 6");
        }
    }
}