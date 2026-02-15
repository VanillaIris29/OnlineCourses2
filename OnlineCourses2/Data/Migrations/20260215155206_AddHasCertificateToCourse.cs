using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCourses2.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHasCertificateToCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasCertificate",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasCertificate",
                table: "Courses");
        }
    }
}
