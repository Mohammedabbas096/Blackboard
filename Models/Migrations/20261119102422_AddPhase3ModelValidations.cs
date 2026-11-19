using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Group1Blackboard.Models.Migrations
{
    /// <inheritdoc />
    public partial class AddPhase3ModelValidations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SSN",
                table: "Users",
                type: "TEXT",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "Users",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Courses",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Semester",
                table: "Courses",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1,
                columns: new[] { "CourseNumber", "Location", "Semester" },
                values: new object[] { "CS101", "Hall 101", "Fall 2026" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 2,
                columns: new[] { "CourseNumber", "Location", "Semester" },
                values: new object[] { "CS201", "Lab 204", "Fall 2026" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 3,
                columns: new[] { "CourseNumber", "Location", "Semester" },
                values: new object[] { "IT210", "Technology Center 105", "Fall 2026" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 4,
                columns: new[] { "CourseNumber", "Location", "Semester" },
                values: new object[] { "IT330", "Technology Center 210", "Spring 2027" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 5,
                columns: new[] { "CourseNumber", "Location", "Semester" },
                values: new object[] { "MATH150", "Science Hall 301", "Fall 2026" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 6,
                columns: new[] { "CourseNumber", "Location", "Semester" },
                values: new object[] { "ENG101", "Online", "Fall 2026" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "DOB", "SSN", "UserType" },
                values: new object[] { new DateTime(1954, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "111-11-1111", "Instructor" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "DOB", "SSN", "UserType" },
                values: new object[] { new DateTime(1960, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "222-22-2222", "Instructor" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "DOB", "SSN", "UserType" },
                values: new object[] { new DateTime(1968, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "333-33-3333", "Instructor" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "DOB", "SSN", "UserType" },
                values: new object[] { new DateTime(2002, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "444-44-4444", "Student" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SSN",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Courses");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1,
                column: "CourseNumber",
                value: "CS 101");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 2,
                column: "CourseNumber",
                value: "CS 201");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 3,
                column: "CourseNumber",
                value: "IT 210");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 4,
                column: "CourseNumber",
                value: "IT 330");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 5,
                column: "CourseNumber",
                value: "MATH 150");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 6,
                column: "CourseNumber",
                value: "ENG 101");
        }
    }
}
