using Microsoft.EntityFrameworkCore.Migrations;

namespace RasorPagesLessons.Services.Migrations
{
    public partial class CodeFirstSpGetEmployeeById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create procedure CodeFirstSpGetEmployeeById
                                    @Id int
                                    as
                                        Begin
                                            Select * from Employees
                                            where Id = @Id
                                        End";
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Drop procedure CodeFirstSpGetEmployeeById";
                                    
            migrationBuilder.Sql(procedure);
        }
    }
}
