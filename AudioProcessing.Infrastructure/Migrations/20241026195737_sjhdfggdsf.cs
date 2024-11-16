using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AudioProcessing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sjhdfggdsf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AudioProcessing");

            migrationBuilder.CreateTable(
                name: "Chats",
                schema: "AudioProcessing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatTitel = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "AudioProcessing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatResponces",
                schema: "AudioProcessing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    AudioId = table.Column<Guid>(type: "uuid", nullable: true),
                    PromtType = table.Column<string>(type: "text", nullable: false),
                    Promt = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatResponces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatResponces_Chats_ChatId",
                        column: x => x.ChatId,
                        principalSchema: "AudioProcessing",
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatResponces_ChatId",
                schema: "AudioProcessing",
                table: "ChatResponces",
                column: "ChatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatResponces",
                schema: "AudioProcessing");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "AudioProcessing");

            migrationBuilder.DropTable(
                name: "Chats",
                schema: "AudioProcessing");
        }
    }
}
