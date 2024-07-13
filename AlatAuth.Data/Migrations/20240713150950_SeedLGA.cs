using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlatAuth.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedLGA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
             table: "LGAs",
             columns: new[] { "Id","StateId", "Name", "CreatedAt", "UpdatedAt", "IsDeleted" },
             values: new object[]
             {1, 1,"Ikeja",DateTime.Now,DateTime.Now,false});

            migrationBuilder.InsertData(
             table: "LGAs",
             columns: new[] { "Id", "StateId", "Name", "CreatedAt", "UpdatedAt", "IsDeleted" },
             values: new object[]
             {2, 1,"Agege",DateTime.Now,DateTime.Now,false});
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
