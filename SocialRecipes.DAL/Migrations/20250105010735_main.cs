using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialRecipes.DAL.Migrations
{
    /// <inheritdoc />
    public partial class main : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_recipes_recipe_id",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_users_user_id",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_followers_users_followed_user_id",
                table: "followers");

            migrationBuilder.DropForeignKey(
                name: "FK_followers_users_following_user_id",
                table: "followers");

            migrationBuilder.DropForeignKey(
                name: "FK_messages_users_receiver_id",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "FK_messages_users_sender_id",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "FK_recipes_users_user_id",
                table: "recipes");

            migrationBuilder.DropIndex(
                name: "IX_users_email",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_messages",
                table: "messages");

            migrationBuilder.RenameTable(
                name: "messages",
                newName: "Messages");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "recipes",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "recipes",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "likes",
                table: "recipes",
                newName: "Likes");

            migrationBuilder.RenameColumn(
                name: "image",
                table: "recipes",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "recipes",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "body",
                table: "recipes",
                newName: "Body");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "recipes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "recipes",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "date_time",
                table: "recipes",
                newName: "DateTime");

            migrationBuilder.RenameIndex(
                name: "IX_recipes_user_id",
                table: "recipes",
                newName: "IX_recipes_UserId");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Messages",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Messages",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "sent_at",
                table: "Messages",
                newName: "SentAt");

            migrationBuilder.RenameColumn(
                name: "sender_id",
                table: "Messages",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "receiver_id",
                table: "Messages",
                newName: "ReceiverId");

            migrationBuilder.RenameColumn(
                name: "is_read",
                table: "Messages",
                newName: "IsRead");

            migrationBuilder.RenameIndex(
                name: "IX_messages_sender_id",
                table: "Messages",
                newName: "IX_Messages_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_messages_receiver_id",
                table: "Messages",
                newName: "IX_Messages_ReceiverId");

            migrationBuilder.RenameColumn(
                name: "following_date",
                table: "followers",
                newName: "FollowingDate");

            migrationBuilder.RenameColumn(
                name: "following_user_id",
                table: "followers",
                newName: "FollowingUserId");

            migrationBuilder.RenameColumn(
                name: "followed_user_id",
                table: "followers",
                newName: "FollowedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_followers_following_user_id",
                table: "followers",
                newName: "IX_followers_FollowingUserId");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "comments",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "comments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "comments",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "recipe_id",
                table: "comments",
                newName: "RecipeId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "comments",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_comments_user_id",
                table: "comments",
                newName: "IX_comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_comments_recipe_id",
                table: "comments",
                newName: "IX_comments_RecipeId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "recipes",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "recipes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "recipes",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Messages",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "comments",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_recipes_RecipeId",
                table: "comments",
                column: "RecipeId",
                principalTable: "recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_users_UserId",
                table: "comments",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_followers_users_FollowedUserId",
                table: "followers",
                column: "FollowedUserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_followers_users_FollowingUserId",
                table: "followers",
                column: "FollowingUserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_users_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_users_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_recipes_users_UserId",
                table: "recipes",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_recipes_RecipeId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_users_UserId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_followers_users_FollowedUserId",
                table: "followers");

            migrationBuilder.DropForeignKey(
                name: "FK_followers_users_FollowingUserId",
                table: "followers");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_users_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_users_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_recipes_users_UserId",
                table: "recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "messages");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "users",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "users",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "recipes",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "recipes",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Likes",
                table: "recipes",
                newName: "likes");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "recipes",
                newName: "image");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "recipes",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Body",
                table: "recipes",
                newName: "body");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "recipes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "recipes",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "recipes",
                newName: "date_time");

            migrationBuilder.RenameIndex(
                name: "IX_recipes_UserId",
                table: "recipes",
                newName: "IX_recipes_user_id");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "messages",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "messages",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SentAt",
                table: "messages",
                newName: "sent_at");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "messages",
                newName: "sender_id");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "messages",
                newName: "receiver_id");

            migrationBuilder.RenameColumn(
                name: "IsRead",
                table: "messages",
                newName: "is_read");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_SenderId",
                table: "messages",
                newName: "IX_messages_sender_id");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ReceiverId",
                table: "messages",
                newName: "IX_messages_receiver_id");

            migrationBuilder.RenameColumn(
                name: "FollowingDate",
                table: "followers",
                newName: "following_date");

            migrationBuilder.RenameColumn(
                name: "FollowingUserId",
                table: "followers",
                newName: "following_user_id");

            migrationBuilder.RenameColumn(
                name: "FollowedUserId",
                table: "followers",
                newName: "followed_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_followers_FollowingUserId",
                table: "followers",
                newName: "IX_followers_following_user_id");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "comments",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "comments",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "comments",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "comments",
                newName: "recipe_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "comments",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_comments_UserId",
                table: "comments",
                newName: "IX_comments_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_comments_RecipeId",
                table: "comments",
                newName: "IX_comments_recipe_id");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "recipes",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "recipes",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "body",
                table: "recipes",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "messages",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "comments",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_messages",
                table: "messages",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_recipes_recipe_id",
                table: "comments",
                column: "recipe_id",
                principalTable: "recipes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_users_user_id",
                table: "comments",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_followers_users_followed_user_id",
                table: "followers",
                column: "followed_user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_followers_users_following_user_id",
                table: "followers",
                column: "following_user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_messages_users_receiver_id",
                table: "messages",
                column: "receiver_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_messages_users_sender_id",
                table: "messages",
                column: "sender_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_recipes_users_user_id",
                table: "recipes",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
