using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VertexEMSBackend.Migrations
{
    public partial class addimageIdaspnetuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfilePictures");

            migrationBuilder.RenameColumn(
                name: "empId",
                table: "AspNetUsers",
                newName: "ProfileIMG");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfileIMG",
                table: "AspNetUsers",
                newName: "empId");

            migrationBuilder.CreateTable(
                name: "ProfilePictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProfileIMG = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    empId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfilePictures_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePictures_EmployeeId",
                table: "ProfilePictures",
                column: "EmployeeId");
        }
    }
}
