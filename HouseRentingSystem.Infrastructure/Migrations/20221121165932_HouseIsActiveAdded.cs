using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastructure.Migrations
{
    public partial class HouseIsActiveAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Houses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "485419c6-1179-4598-a392-859aefae6016", "AQAAAAEAACcQAAAAEHfPhlDrIBrIzl9+3PysHwiWwMc+dMl0l9MzuXSN5gWjfqaqclFnxDAgFC4Okxz5mA==", "358e6aff-5582-4142-a4ce-dd648a038824" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "637f88ef-c7c7-491a-8aba-377c5371997f", "AQAAAAEAACcQAAAAEHJUa3ZEeLarsvV0Pp8YxKOmLAg5mrVW5nllMtunWAGVn3Mp51kxQKMNvlRr3URhlA==", "9a8fe6c5-0d67-4f19-8d24-e924c7dd0e2d" });

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsActive",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Houses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "772a87b4-36d6-4a62-b8e2-20968542ab4a", "AQAAAAEAACcQAAAAEMECQo1udzB1cIVAqHoKedU1DiLodL67qWumzyNBXMC4crWL6cEtpf4I2TZk/NEnyA==", "886936fd-6910-48ab-93eb-99ddae0def6e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b0b93709-1ed3-4b91-84d9-bc15b970f3af", "AQAAAAEAACcQAAAAEPwSj5UbJbAOW29pGxHxP8aJ9biVDzHuMwwYbgxczUqC5LxwMKEK+xs0J9Q8Z8GJpw==", "557350d4-bd94-492b-bd05-e7ff985211b7" });
        }
    }
}
