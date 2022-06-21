using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SignalRAppAngular.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatBotCollectInfo",
                columns: table => new
                {
                    CollectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectName = table.Column<string>(nullable: true),
                    CollectDecription = table.Column<string>(nullable: true),
                    BotId = table.Column<int>(nullable: false),
                    ParentCollectId = table.Column<int>(nullable: false),
                    OnCompleteRedirectMethod = table.Column<string>(nullable: true),
                    OnCompleteRedirectURI = table.Column<string>(nullable: true),
                    IsFinalCollect = table.Column<bool>(nullable: false),
                    DecisionIdentifier = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatBotCollectInfo", x => x.CollectId);
                });

            migrationBuilder.CreateTable(
                name: "ChatBotCollectQuestionInfo",
                columns: table => new
                {
                    QuestionId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(nullable: true),
                    QuestionName = table.Column<string>(nullable: true),
                    CollectId = table.Column<int>(nullable: false),
                    QuestionType = table.Column<string>(nullable: true),
                    ParentQuestionId = table.Column<long>(nullable: false),
                    BotId = table.Column<int>(nullable: false),
                    DecisionIdentifier = table.Column<string>(nullable: true),
                    OptionGroupId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatBotCollectQuestionInfo", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "ChatBotConversationDetailInfo",
                columns: table => new
                {
                    ConversationDetailId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversationHeaderId = table.Column<long>(nullable: false),
                    QuestionName = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatBotConversationDetailInfo", x => x.ConversationDetailId);
                });

            migrationBuilder.CreateTable(
                name: "ChatBotConversationHeaderInfo",
                columns: table => new
                {
                    ConversationHeaderId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversationId = table.Column<long>(nullable: false),
                    DecisionIdentifier = table.Column<string>(nullable: true),
                    ResponseJSON = table.Column<string>(nullable: true),
                    CollectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatBotConversationHeaderInfo", x => x.ConversationHeaderId);
                });

            migrationBuilder.CreateTable(
                name: "ChatBotConversationInfo",
                columns: table => new
                {
                    ConversationId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BotId = table.Column<int>(nullable: false),
                    UserIdentifier = table.Column<string>(nullable: true),
                    ChannelName = table.Column<string>(nullable: true),
                    ChannelSID = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatBotConversationInfo", x => x.ConversationId);
                });

            migrationBuilder.CreateTable(
                name: "ChatBotMasters",
                columns: table => new
                {
                    BotId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BotName = table.Column<string>(nullable: false),
                    BotDescription = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatBotMasters", x => x.BotId);
                });

            migrationBuilder.CreateTable(
                name: "ChatBotQuestionOptionGroupCodeInfo",
                columns: table => new
                {
                    OptionCodeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionGroupId = table.Column<long>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    CodeDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatBotQuestionOptionGroupCodeInfo", x => x.OptionCodeId);
                });

            migrationBuilder.CreateTable(
                name: "ChatBotQuestionOptionGroupInfo",
                columns: table => new
                {
                    OptionGroupId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(nullable: true),
                    GroupDescription = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatBotQuestionOptionGroupInfo", x => x.OptionGroupId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatBotCollectInfo");

            migrationBuilder.DropTable(
                name: "ChatBotCollectQuestionInfo");

            migrationBuilder.DropTable(
                name: "ChatBotConversationDetailInfo");

            migrationBuilder.DropTable(
                name: "ChatBotConversationHeaderInfo");

            migrationBuilder.DropTable(
                name: "ChatBotConversationInfo");

            migrationBuilder.DropTable(
                name: "ChatBotMasters");

            migrationBuilder.DropTable(
                name: "ChatBotQuestionOptionGroupCodeInfo");

            migrationBuilder.DropTable(
                name: "ChatBotQuestionOptionGroupInfo");
        }
    }
}
