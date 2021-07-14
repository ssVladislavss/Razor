using Microsoft.EntityFrameworkCore.Migrations;

namespace RasorPagesLessons.Services.Migrations
{
    public partial class AddNewEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string newEmployee = @"Create procedure spAddNewEmployee
                                  @Name nvarchar(100),
                                  @Email nvarchar(100),
                                  @PhotoPath nvarchar(100),
                                  @Dept int
                                  as
                                  Begin
                                     INSERT INTO Employees (Name, Email, PhotoPath, Department)
                                     VALUES (@Name, @Email, @PhotoPath, @Dept)
                                  End";
            migrationBuilder.Sql(newEmployee);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string newEmployee = @"Drop procedure spAddNewEmployee";
            migrationBuilder.Sql(newEmployee);
        }
    }
}
