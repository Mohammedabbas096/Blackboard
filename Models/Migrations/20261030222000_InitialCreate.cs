using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Group1Blackboard.Models.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Majors",
                columns: table => new
                {
                    MajorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Majors", x => x.MajorId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Units = table.Column<int>(type: "INTEGER", nullable: false),
                    Schedule = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Time = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MajorId = table.Column<int>(type: "INTEGER", nullable: false),
                    InstructorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_Majors_MajorId",
                        column: x => x.MajorId,
                        principalTable: "Majors",
                        principalColumn: "MajorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Users_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Majors",
                columns: new[] { "MajorId", "Name" },
                values: new object[,]
                {
                    { 1, "Computer Science" },
                    { 2, "Information Technology" },
                    { 3, "Mathematics" },
                    { 4, "English" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "alan.turing@university.edu", "Alan", "Turing" },
                    { 2, "grace.hopper@university.edu", "Grace", "Hopper" },
                    { 3, "katherine.j@university.edu", "Katherine", "Johnson" },
                    { 4, "alex.mercer@student.edu", "Alex", "Mercer" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CourseNumber", "Description", "InstructorId", "MajorId", "Schedule", "Time", "Title", "Units" },
                values: new object[,]
                {
                    { 1, "CS 101", "Fundamental programming principles and problem solving using C#.", 1, 1, "MWF", "09:00 AM - 09:50 AM", "Introduction to Computer Science", 3 },
                    { 2, "CS 201", "Study of fundamental data structures including lists, stacks, queues, trees, and graphs.", 1, 1, "TR", "10:00 AM - 11:15 AM", "Data Structures and Algorithms", 4 },
                    { 3, "IT 210", "Building modern web applications using MVC, Razor, and Entity Framework Core.", 2, 2, "MWF", "11:00 AM - 11:50 AM", "Web Development with ASP.NET Core", 3 },
                    { 4, "IT 330", "Relational database concepts, SQL queries, normalization, and database design.", 2, 2, "TR", "01:00 PM - 02:15 PM", "Database Systems & Design", 3 },
                    { 5, "MATH 150", "Limits, continuity, derivatives, and introduction to integration.", 3, 3, "MWF", "08:00 AM - 08:50 AM", "Calculus I", 4 },
                    { 6, "ENG 101", "College-level expository and analytical writing with rhetorical focus.", 3, 4, "Online", "Asynchronous", "Academic Writing", 3 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "CourseId", "ReservationDate", "Status", "StudentId" },
                values: new object[] { 1, 1, new DateTime(2026, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), "Confirmed", 4 });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_MajorId",
                table: "Courses",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CourseId",
                table: "Reservations",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_StudentId_CourseId",
                table: "Reservations",
                columns: new[] { "StudentId", "CourseId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Majors");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
