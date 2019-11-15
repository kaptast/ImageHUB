using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageHUB.Migrations
{
    public partial class ManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Image = table.Column<string>(nullable: true),
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OwnerID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Posts_Profiles_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Profiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileFriend",
                columns: table => new
                {
                    ProfileID = table.Column<string>(nullable: false),
                    FriendID = table.Column<string>(nullable: false),
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
                name: "IX_Posts_OwnerID",
                table: "Posts",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileFriend_FriendID",
                table: "ProfileFriend",
                column: "FriendID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "ProfileFriend");

            migrationBuilder.DropTable(
                name: "Profiles");
        }
    }
}
