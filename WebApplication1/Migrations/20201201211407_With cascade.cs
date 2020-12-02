using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class Withcascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Knowledges_Knowledges_KnowledgeId1",
                table: "Knowledges");

            migrationBuilder.DropIndex(
                name: "IX_Knowledges_KnowledgeId1",
                table: "Knowledges");

            migrationBuilder.DropColumn(
                name: "KnowledgeId1",
                table: "Knowledges");

            migrationBuilder.AlterColumn<int>(
                name: "KnowledgeId",
                table: "Knowledges",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "099358ab-c05e-4007-a636-2c97f186ca69");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5dcbc093-fce8-4aae-89c1-b1012c6bc220");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e1d4a3f6-e0a8-4b5d-8295-4cdd671d3cb6", "$2a$11$5TiRvCEsYfcxuoDM.pAe4Ogs2qQVR4ifLgXEufT6COe6e.7VHRa1e", "9e01c15c-4b00-4e29-939e-33969e844474" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e6a6200d-c975-4061-8566-bb539bb3eb9f", "$2a$11$kTFt8bjU6eWzHqNgWUFFgOdPyaieUlTTcFUvS2kMR9/r/kpPlTcPW" });

            migrationBuilder.UpdateData(
                table: "KnowledgeResults",
                keyColumn: "KnowledgeResultId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2020, 12, 1, 23, 14, 6, 794, DateTimeKind.Local).AddTicks(5867));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "KnowledgeId",
                table: "Knowledges",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "KnowledgeId1",
                table: "Knowledges",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e6f9c230-41d8-434c-8140-6713a021b651");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "dc278b06-eeaa-4827-8a5e-826b48d9e452");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0f57b6b-83c9-4190-a11d-8a3283478c36", "$2a$11$mXtAYK.LhjXJjYnt6lJY5egRJ32yJBrIDT6QVYcvU/Dohll1y4J0S", "7cb1bbe1-df5c-4412-a1b2-73bf7efa4283" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1034c4d6-65b2-4d39-8b0e-c49429c7bbab", "$2a$11$pww0XLN9Njtew/QDxTigB.KSpXPPFOOnRJwHuSwKwZfQ8Ps0hyjeC" });

            migrationBuilder.UpdateData(
                table: "KnowledgeResults",
                keyColumn: "KnowledgeResultId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2020, 11, 28, 14, 59, 0, 797, DateTimeKind.Local).AddTicks(7534));

            migrationBuilder.CreateIndex(
                name: "IX_Knowledges_KnowledgeId1",
                table: "Knowledges",
                column: "KnowledgeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Knowledges_Knowledges_KnowledgeId1",
                table: "Knowledges",
                column: "KnowledgeId1",
                principalTable: "Knowledges",
                principalColumn: "KnowledgeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
