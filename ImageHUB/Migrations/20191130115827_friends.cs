using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageHUB.Migrations
{
    public partial class friends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfileFriend",
                columns: table => new
                {
                    ProfileID = table.Column<int>(nullable: false),
                    FriendID = table.Column<int>(nullable: false),
                    ID = table.Column<int>(nullable: false),
                    Accepted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileFriend", x => new { x.ProfileID, x.FriendID });
                    table.ForeignKey(
                        name: "FK_ProfileFriend_Profiles_FriendID",
                        column: x => x.FriendID,
                        principalTable: "Profiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileFriend_Profiles_ProfileID",
                        column: x => x.ProfileID,
                        principalTable: "Profiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileFriend_FriendID",
                table: "ProfileFriend",
                column: "FriendID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileFriend");
        }
    }
}
