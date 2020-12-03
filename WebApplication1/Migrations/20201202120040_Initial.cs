using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Knowledges",
                columns: table => new
                {
                    KnowledgeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KnowledgeName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knowledges", x => x.KnowledgeId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeResults",
                columns: table => new
                {
                    KnowledgeResultId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KnowledgeId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Result = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeResults", x => x.KnowledgeResultId);
                    table.ForeignKey(
                        name: "FK_KnowledgeResults_Knowledges_KnowledgeId",
                        column: x => x.KnowledgeId,
                        principalTable: "Knowledges",
                        principalColumn: "KnowledgeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KnowledgeResults_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionString = table.Column<string>(maxLength: 200, nullable: false),
                    KnowledgeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_Knowledges_KnowledgeId",
                        column: x => x.KnowledgeId,
                        principalTable: "Knowledges",
                        principalColumn: "KnowledgeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerString = table.Column<string>(maxLength: 200, nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    CorrectAnswer = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionResults",
                columns: table => new
                {
                    QuestionResultId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(nullable: false),
                    KnowledgeResultId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionResults", x => x.QuestionResultId);
                    table.ForeignKey(
                        name: "FK_QuestionResults_KnowledgeResults_KnowledgeResultId",
                        column: x => x.KnowledgeResultId,
                        principalTable: "KnowledgeResults",
                        principalColumn: "KnowledgeResultId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionResults_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnswerResults",
                columns: table => new
                {
                    AnswerResultId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerId = table.Column<int>(nullable: false),
                    QuestionResultId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerResults", x => x.AnswerResultId);
                    table.ForeignKey(
                        name: "FK_AnswerResults_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "AnswerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerResults_QuestionResults_QuestionResultId",
                        column: x => x.QuestionResultId,
                        principalTable: "QuestionResults",
                        principalColumn: "QuestionResultId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "ddee1616-3825-42ee-a6bb-3ac478619a4d", "Admin", "ADMIN" },
                    { 2, "b744a278-8b55-4d29-93aa-93be5b442b1e", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Knowledges",
                columns: new[] { "KnowledgeId", "KnowledgeName" },
                values: new object[,]
                {
                    { 1, "C# essentials" },
                    { 2, "Javascript" },
                    { 3, "SQL" },
                    { 4, "OOP" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "KnowledgeId", "QuestionString" },
                values: new object[,]
                {
                    { 1, 1, "Which of the following operator can be used to access the member function of a class" },
                    { 18, 4, "Which of the following keywords can be used to access a member of the base class from derived class?" },
                    { 17, 4, "Which of the following Access specifiers can be used for an interface?" },
                    { 16, 4, "Which of the following options define the correct way of implementing an interface data by the class employee?" },
                    { 15, 3, "Which SQL statement is used to return only different values?" },
                    { 14, 3, "Which operator is used to search for a specified pattern in a column?" },
                    { 13, 3, "Which SQL keyword is used to sort the result-set" },
                    { 12, 3, "With SQL, how can you delete the records where the FirstName is Peter in the Persons Table?" },
                    { 11, 3, "With SQL, how do you select a column named FirstName from a table named Person, whree the value of the column FirstNamae starts with \"a\"?" },
                    { 10, 2, "Which of the following is NOT a JavaScript object?" },
                    { 9, 2, @"var x = 0;
                do
                { console.log(x) } 
                while (x > 0)?" },
                    { 8, 2, "The JavaScipt statement a = new Array(2,4)" },
                    { 7, 2, "After executing the Javascript statement a=(new Araray(10)).toString(), what is the value of a?" },
                    { 6, 2, "Which of the folowing is not a valid function call?" },
                    { 5, 1, "Which of the following operator is not an equality operators" },
                    { 4, 1, "What is the correct name of a method which has the same name as that of class and used to destroy objects?" },
                    { 3, 1, "Which of the following statements correctly tell the differences between ‘=’ and ‘==’ in C#" },
                    { 2, 1, "Which of the following gives the correct count of the constructors that a class can define?" },
                    { 19, 4, "Which of the following keyword, enables to modify the data and behavior of a base class by replacing its member with a new derived member?" },
                    { 20, 4, "Which of the following options represents the type of class which does not have its own objects but acts as a base class for its subclass?" }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "AnswerId", "AnswerString", "CorrectAnswer", "QuestionId" },
                values: new object[,]
                {
                    { 1, ":", false, 1 },
                    { 58, "Select unique", false, 15 },
                    { 57, "Select distinct", true, 15 },
                    { 56, "End", false, 14 },
                    { 55, "LIKE", true, 14 },
                    { 54, "FROM", false, 14 },
                    { 53, "GET", false, 14 },
                    { 52, "Sort", false, 13 },
                    { 59, "Select Different", false, 15 },
                    { 51, "Order by", true, 13 },
                    { 49, "Sort by", false, 13 },
                    { 48, "None of the above", false, 12 },
                    { 47, "Delete FirstName = 'Peter' from Persons", false, 12 },
                    { 46, "Delete row FirstName = 'Peter' from Persons", false, 12 },
                    { 45, "Delete from Persons where FirstName = 'Peter'", true, 12 },
                    { 44, "select * from Persons where FirstName = '%a%'", false, 11 },
                    { 43, "select * from Persons where FirstName Like '%a'", true, 11 },
                    { 50, "Order", false, 13 },
                    { 42, "select * from Persons where FirstName='a'", false, 11 },
                    { 60, "<>=", false, 15 },
                    { 62, "class employee implements data {}", false, 16 },
                    { 78, "Sealed class", false, 20 },
                    { 77, "Static class", false, 20 },
                    { 76, "base", false, 19 },
                    { 75, "new", true, 19 },
                    { 74, "overrides", false, 19 },
                    { 73, "overloads", false, 19 },
                    { 72, "None of the above", false, 18 },
                    { 61, "class employee : data {}", true, 16 },
                    { 71, "this", false, 18 },
                    { 69, "upper", false, 18 },
                    { 68, "All of the above", false, 17 },
                    { 67, "Private", false, 17 },
                    { 66, "Protected", false, 17 },
                    { 65, "Public", true, 17 },
                    { 64, "None of the mentioned", false, 16 },
                    { 63, "class employee imports data {}", false, 16 },
                    { 70, "base", true, 18 },
                    { 41, "select * from Persons where FirstName Like 'a%'", false, 11 },
                    { 40, "var obj = new Object();", false, 10 },
                    { 39, "var obj = { name = \"Steve\"};", true, 10 },
                    { 17, ">=", false, 5 },
                    { 16, "End", false, 4 },
                    { 15, "Constructor", false, 4 },
                    { 14, "Finalize()", false, 4 },
                    { 13, "Destructor", true, 4 },
                    { 12, "None of the above", false, 3 },
                    { 11, "No difference between both operators", false, 3 },
                    { 18, "!=", false, 5 },
                    { 10, "‘=’ operator is used to assign values from one variable to another variable, ‘==’ operator is used to compare value between two variables", true, 3 },
                    { 8, "None of the above", false, 2 },
                    { 7, "Any number", true, 2 },
                    { 6, "2", false, 2 },
                    { 5, "1", false, 2 },
                    { 4, "#", false, 1 },
                    { 3, ".", true, 1 },
                    { 2, "::", false, 1 },
                    { 9, "‘==’ operator is used to assign values from one variable to another variable,  ‘=’ operator is used to compare value between two variables", false, 3 },
                    { 19, "<=", false, 5 },
                    { 20, "<>=", true, 5 },
                    { 21, "var x = myFunc()", false, 6 },
                    { 38, "var obj = { name: \"Steve\"};", false, 10 },
                    { 37, "var obj = {};", false, 10 },
                    { 36, "Object", false, 9 },
                    { 35, "No output", false, 9 },
                    { 34, "null", false, 9 },
                    { 33, "0", true, 9 },
                    { 32, "defines a three-element array whose elements have indexes 2 through 4", false, 8 },
                    { 31, "defines an array a andd assigns the values 2 and 4 to a[0] and a[1]", true, 8 },
                    { 30, "defines an array a and assigns the values 2 and 4 to a[1] and a[2]", false, 8 },
                    { 29, "defines a new two-dimentional array a whose dimentions are 2 and 4", false, 8 },
                    { 28, "This statement will cause an error", false, 7 },
                    { 27, "string \"..........\"", true, 7 },
                    { 26, "array of 10 empty strings", false, 7 },
                    { 25, "string \"10\"", false, 7 },
                    { 24, "myfunc()", false, 6 },
                    { 23, "x = myfunc()", false, 6 },
                    { 22, "myfunc", true, 6 },
                    { 79, "Abstract class", true, 20 },
                    { 80, "Derived class", false, 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerResults_AnswerId",
                table: "AnswerResults",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerResults_QuestionResultId",
                table: "AnswerResults",
                column: "QuestionResultId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeResults_KnowledgeId",
                table: "KnowledgeResults",
                column: "KnowledgeId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeResults_UserId",
                table: "KnowledgeResults",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResults_KnowledgeResultId",
                table: "QuestionResults",
                column: "KnowledgeResultId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResults_QuestionId",
                table: "QuestionResults",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_KnowledgeId",
                table: "Questions",
                column: "KnowledgeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerResults");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "QuestionResults");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "KnowledgeResults");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Knowledges");
        }
    }
}
