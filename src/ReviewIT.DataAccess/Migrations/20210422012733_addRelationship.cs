using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReviewIT.DataAccess.Migrations
{
    public partial class addRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BrandName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    NickName = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    AverageRating = table.Column<double>(nullable: false),
                    BrandId = table.Column<int>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    CreatorUserId = table.Column<string>(nullable: true),
                    PostContent = table.Column<string>(nullable: false),
                    RatingForDevice = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_Users_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PostId = table.Column<int>(nullable: false),
                    CreatorUserId = table.Column<string>(nullable: true),
                    CommentContent = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ParentCommentId = table.Column<int>(nullable: true),
                    IsArchived = table.Column<bool>(nullable: false),
                    CommentId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_CommentId1",
                        column: x => x.CommentId1,
                        principalTable: "Comments",
                        principalColumn: "CommentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Users_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostVotes",
                columns: table => new
                {
                    PostVoteId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PostId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    IsUp = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostVotes", x => x.PostVoteId);
                    table.ForeignKey(
                        name: "FK_PostVotes_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostVotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentVotes",
                columns: table => new
                {
                    CommentVoteId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommentId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    IsUp = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentVotes", x => x.CommentVoteId);
                    table.ForeignKey(
                        name: "FK_CommentVotes_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "CommentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentVotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "BrandId", "BrandName", "Description", "ImagePath" },
                values: new object[] { 1, "Apple", "Who is  the superior brand?", "/Brand_Apple.png" });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "BrandId", "BrandName", "Description", "ImagePath" },
                values: new object[] { 2, "Samsung", "Obviously, I am the superior brand", "/Brand_Samsung.jpg" });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "BrandId", "BrandName", "Description", "ImagePath" },
                values: new object[] { 3, "Google", "Nope, I am obviously better.", "/Brand_Google.jpg" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreationDate", "EmailAddress", "IsAdmin", "IsArchived", "NickName", "PasswordHash" },
                values: new object[] { "73398", new DateTime(2020, 7, 9, 6, 34, 0, 0, DateTimeKind.Unspecified), "bkilleend@yahoo.co.jp", false, false, "Hvmema7791", "q1IJj55" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreationDate", "EmailAddress", "IsAdmin", "IsArchived", "NickName", "PasswordHash" },
                values: new object[] { "21564", new DateTime(2019, 4, 13, 10, 8, 0, 0, DateTimeKind.Unspecified), "klawriec@ucoz.ru", false, false, "Xtyfpx1524", "gTTP0LBwRH" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreationDate", "EmailAddress", "IsAdmin", "IsArchived", "NickName", "PasswordHash" },
                values: new object[] { "85497", new DateTime(2020, 11, 2, 23, 10, 0, 0, DateTimeKind.Unspecified), "bhessayb@cisco.com", false, false, "Zttafw0061", "FgHq8ir" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreationDate", "EmailAddress", "IsAdmin", "IsArchived", "NickName", "PasswordHash" },
                values: new object[] { "61437", new DateTime(2020, 10, 11, 11, 2, 0, 0, DateTimeKind.Unspecified), "pswetta@blogger.com", false, false, "Khqcgy0148", "Jd2kLQ" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreationDate", "EmailAddress", "IsAdmin", "IsArchived", "NickName", "PasswordHash" },
                values: new object[] { "26600", new DateTime(2019, 3, 21, 3, 35, 0, 0, DateTimeKind.Unspecified), "wkasman9@homestead.com", false, false, "Jilfpv3327", "JH9HVJs3Z0B" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreationDate", "EmailAddress", "IsAdmin", "IsArchived", "NickName", "PasswordHash" },
                values: new object[] { "67588", new DateTime(2019, 6, 24, 14, 37, 0, 0, DateTimeKind.Unspecified), "mskedge8@blog.com", false, false, "Wlgtgm9390", "v6eGUm8f" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreationDate", "EmailAddress", "IsAdmin", "IsArchived", "NickName", "PasswordHash" },
                values: new object[] { "4345", new DateTime(2020, 3, 24, 18, 34, 0, 0, DateTimeKind.Unspecified), "ntrimbey7@blog.com", false, false, "Hmsffu2509", "HlNiWB" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreationDate", "EmailAddress", "IsAdmin", "IsArchived", "NickName", "PasswordHash" },
                values: new object[] { "6561", new DateTime(2020, 11, 8, 9, 44, 0, 0, DateTimeKind.Unspecified), "pbockin6@hugedomains.com", false, false, "Thusls4964", "tBPWKuaj9N" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreationDate", "EmailAddress", "IsAdmin", "IsArchived", "NickName", "PasswordHash" },
                values: new object[] { "78347", new DateTime(2019, 5, 31, 0, 37, 0, 0, DateTimeKind.Unspecified), "chuggill5@flickr.com", false, false, "Wifrmn8717", "OlXbLqtXJfK" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreationDate", "EmailAddress", "IsAdmin", "IsArchived", "NickName", "PasswordHash" },
                values: new object[] { "79354", new DateTime(2019, 1, 15, 7, 24, 0, 0, DateTimeKind.Unspecified), "kvillage4@census.gov", false, false, "Hdrvya8006", "IcgWFtGC" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreationDate", "EmailAddress", "IsAdmin", "IsArchived", "NickName", "PasswordHash" },
                values: new object[] { "42923", new DateTime(2018, 5, 21, 5, 14, 0, 0, DateTimeKind.Unspecified), "bbyrne3@reverbnation.com", false, false, "Kyxfcn8147", "KNiWZVMh" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreationDate", "EmailAddress", "IsAdmin", "IsArchived", "NickName", "PasswordHash" },
                values: new object[] { "77416", new DateTime(2020, 4, 22, 18, 44, 0, 0, DateTimeKind.Unspecified), "gmacrury2@facebook.com", false, false, "Htdkzb1212", "AQAAAAEAACcQAAAAECffeqvrSVRq7+dQVIcsWl7qFDLJ5vBN2hu+fHe4Ehufd2cZAle/uHSYp4+Q9tFEcw==" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreationDate", "EmailAddress", "IsAdmin", "IsArchived", "NickName", "PasswordHash" },
                values: new object[] { "83912", new DateTime(2020, 7, 30, 3, 8, 0, 0, DateTimeKind.Unspecified), "mborrett1@google.cn", false, false, "Zxbcdq4196", "AQAAAAEAACcQAAAAEISBV6ylpl+pyZphD91uplpIB9VUWfpM6MtUevbUhgAy+JbCK/PNr5su5t34OthPHg==" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreationDate", "EmailAddress", "IsAdmin", "IsArchived", "NickName", "PasswordHash" },
                values: new object[] { "63062", new DateTime(2020, 2, 27, 21, 13, 0, 0, DateTimeKind.Unspecified), "npeedell0@howstuffworks.com", false, false, "Bytrxa8980", "AQAAAAEAACcQAAAAEGoXSEkWUu0jowjLG6p7gUnxysrgWdglb7qB3mp9amSJw4Y5rvPqKhDo11DSd2JppA==" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreationDate", "EmailAddress", "IsAdmin", "IsArchived", "NickName", "PasswordHash" },
                values: new object[] { "67706", new DateTime(2019, 4, 1, 16, 27, 0, 0, DateTimeKind.Unspecified), "ocoselye@jalbum.net", false, false, "Iafqzl3913", "sWEDyx35s" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreationDate", "EmailAddress", "IsAdmin", "IsArchived", "NickName", "PasswordHash" },
                values: new object[] { "admin", new DateTime(2019, 4, 1, 16, 27, 0, 0, DateTimeKind.Unspecified), "admin@admin.admin", true, false, "admin", "AQAAAAEAACcQAAAAEEViKHj5p9eE8g8LKuNpezIep9S4K75RXujhlWbFDwH1FTiW+3AUEP9oBXgjvlgKIQ==" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CreationDate", "CreatorUserId", "IsArchived", "PostContent", "ProductId", "RatingForDevice", "Title" },
                values: new object[] { 9, new DateTime(2019, 11, 3, 10, 15, 0, 0, DateTimeKind.Unspecified), "67588", false, @"Aenean lectus. Pellentesque eget nunc. Donec quis orci eget orci vehicula condimentum.
Curabitur in libero ut massa volutpat convallis. Morbi odio odio, elementum eu, interdum eu, tincidunt in, leo. Maecenas pulvinar lobortis est.
Phasellus sit amet erat. Nulla tempus. Vivamus in felis eu sapien cursus vestibulum.", 9, 2, "orci luctus et ultrices posuere cubilia curae mauris viverra diam vitae" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CreationDate", "CreatorUserId", "IsArchived", "PostContent", "ProductId", "RatingForDevice", "Title" },
                values: new object[] { 7, new DateTime(2020, 1, 20, 22, 50, 0, 0, DateTimeKind.Unspecified), "6561", false, "Donec diam neque, vestibulum eget, vulputate ut, ultrices vel, augue. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Donec pharetra, magna vestibulum aliquet ultrices, erat tortor sollicitudin mi, sit amet lobortis sapien sapien non mi. Integer ac neque.", 7, 4, "suspendisse potenti in eleifend quam a odio in hac habitasse platea dictumst maecenas ut" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CreationDate", "CreatorUserId", "IsArchived", "PostContent", "ProductId", "RatingForDevice", "Title" },
                values: new object[] { 6, new DateTime(2019, 5, 6, 17, 46, 0, 0, DateTimeKind.Unspecified), "78347", false, "Phasellus sit amet erat. Nulla tempus. Vivamus in felis eu sapien cursus vestibulum.", 6, 0, "elit ac nulla sed vel enim sit amet nunc viverra dapibus nulla suscipit ligula in lacus curabitur at" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CreationDate", "CreatorUserId", "IsArchived", "PostContent", "ProductId", "RatingForDevice", "Title" },
                values: new object[] { 5, new DateTime(2018, 11, 20, 19, 25, 0, 0, DateTimeKind.Unspecified), "79354", false, @"In hac habitasse platea dictumst. Etiam faucibus cursus urna. Ut tellus.
Nulla ut erat id mauris vulputate elementum. Nullam varius. Nulla facilisi.", 5, 3, "donec semper sapien a libero nam dui proin leo odio porttitor id" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CreationDate", "CreatorUserId", "IsArchived", "PostContent", "ProductId", "RatingForDevice", "Title" },
                values: new object[] { 4, new DateTime(2018, 6, 27, 10, 9, 0, 0, DateTimeKind.Unspecified), "42923", false, @"Aenean lectus. Pellentesque eget nunc. Donec quis orci eget orci vehicula condimentum.
Curabitur in libero ut massa volutpat convallis. Morbi odio odio, elementum eu, interdum eu, tincidunt in, leo. Maecenas pulvinar lobortis est.
Phasellus sit amet erat. Nulla tempus. Vivamus in felis eu sapien cursus vestibulum.", 4, 0, "habitasse platea dictumst aliquam augue quam sollicitudin vitae consectetuer eget rutrum at" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CreationDate", "CreatorUserId", "IsArchived", "PostContent", "ProductId", "RatingForDevice", "Title" },
                values: new object[] { 3, new DateTime(2018, 4, 25, 3, 42, 0, 0, DateTimeKind.Unspecified), "77416", false, "Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vivamus vestibulum sagittis sapien. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.", 3, 5, "integer pede justo lacinia eget tincidunt eget tempus vel pede morbi porttitor lorem id ligula suspendisse ornare" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CreationDate", "CreatorUserId", "IsArchived", "PostContent", "ProductId", "RatingForDevice", "Title" },
                values: new object[] { 2, new DateTime(2018, 12, 4, 1, 51, 0, 0, DateTimeKind.Unspecified), "83912", false, "Vestibulum quam sapien, varius ut, blandit non, interdum in, ante. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Duis faucibus accumsan odio. Curabitur convallis.", 2, 5, "nulla dapibus dolor vel est donec odio justo sollicitudin ut suscipit a feugiat" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CreationDate", "CreatorUserId", "IsArchived", "PostContent", "ProductId", "RatingForDevice", "Title" },
                values: new object[] { 1, new DateTime(2018, 11, 22, 21, 1, 0, 0, DateTimeKind.Unspecified), "63062", false, @"Suspendisse potenti. In eleifend quam a odio. In hac habitasse platea dictumst.
Maecenas ut massa quis augue luctus tincidunt. Nulla mollis molestie lorem. Quisque ut erat.", 1, 1, "eget congue eget semper rutrum nulla nunc purus phasellus in felis" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CreationDate", "CreatorUserId", "IsArchived", "PostContent", "ProductId", "RatingForDevice", "Title" },
                values: new object[] { 8, new DateTime(2021, 2, 5, 20, 57, 0, 0, DateTimeKind.Unspecified), "4345", false, "In hac habitasse platea dictumst. Etiam faucibus cursus urna. Ut tellus.", 8, 4, "nulla suspendisse potenti cras in purus eu magna vulputate luctus cum" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "BrandId", "Description", "ImagePath", "IsArchived", "Name", "Price", "ReleaseDate" },
                values: new object[] { 18, 0.0, 3, "This is fluff for now", "/pixel5.jpg", false, "Pixel 5", 929.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "BrandId", "Description", "ImagePath", "IsArchived", "Name", "Price", "ReleaseDate" },
                values: new object[] { 17, 0.0, 3, "This is fluff for now", "/pixel4.jpg", false, "Pixel 4", 869.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "BrandId", "Description", "ImagePath", "IsArchived", "Name", "Price", "ReleaseDate" },
                values: new object[] { 16, 0.0, 3, "This is fluff for now", "/pixel3.jpg", false, "Pixel 3", 799.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "BrandId", "Description", "ImagePath", "IsArchived", "Name", "Price", "ReleaseDate" },
                values: new object[] { 15, 0.0, 2, "This is fluff for now", "/galaxyS21.jpg", false, "Samsung Galaxy S21", 1249.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "BrandId", "Description", "ImagePath", "IsArchived", "Name", "Price", "ReleaseDate" },
                values: new object[] { 14, 0.0, 2, "This is fluff for now", "/galaxyS20.jpg", false, "Samsung Galaxy S20", 1049.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "BrandId", "Description", "ImagePath", "IsArchived", "Name", "Price", "ReleaseDate" },
                values: new object[] { 12, 0.0, 2, "This is fluff for now", "/galaxyNote20.jpg", false, "Samsung Galaxy Note 20", 1149.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "BrandId", "Description", "ImagePath", "IsArchived", "Name", "Price", "ReleaseDate" },
                values: new object[] { 11, 0.0, 2, "This is fluff for now", "/galaxyNote10.jpg", false, "Samsung Galaxy Note 10", 1049.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "BrandId", "Description", "ImagePath", "IsArchived", "Name", "Price", "ReleaseDate" },
                values: new object[] { 10, 0.0, 2, "This is fluff for now", "/galaxyNote09.jpg", false, "Samsung Galaxy Note 09", 949.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "BrandId", "Description", "ImagePath", "IsArchived", "Name", "Price", "ReleaseDate" },
                values: new object[] { 9, 0.0, 1, "This is fluff for now", "/iphoneXR.jpg", false, "iPhone XR", 749.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "BrandId", "Description", "ImagePath", "IsArchived", "Name", "Price", "ReleaseDate" },
                values: new object[] { 8, 0.0, 1, "This is fluff for now", "/iphone11.jpg", false, "iPhone 11", 799.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "BrandId", "Description", "ImagePath", "IsArchived", "Name", "Price", "ReleaseDate" },
                values: new object[] { 7, 0.0, 1, "This is fluff for now", "/iphone11pro&max.jpg", false, "iPhone 11 Pro", 849.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "BrandId", "Description", "ImagePath", "IsArchived", "Name", "Price", "ReleaseDate" },
                values: new object[] { 6, 0.0, 1, "This is fluff for now", "/iphone11pro&max.jpg", false, "iPhone 11 Pro Max", 999.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "BrandId", "Description", "ImagePath", "IsArchived", "Name", "Price", "ReleaseDate" },
                values: new object[] { 5, 0.0, 1, "This is fluff for now", "/iphone_SE.jpg", false, "iPhone SE", 999.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "BrandId", "Description", "ImagePath", "IsArchived", "Name", "Price", "ReleaseDate" },
                values: new object[] { 4, 0.0, 1, "This is fluff for now", "/iphone12&mini.jpg", false, "iPhone 12 mini", 999.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "BrandId", "Description", "ImagePath", "IsArchived", "Name", "Price", "ReleaseDate" },
                values: new object[] { 3, 0.0, 1, "This is fluff for now", "/iphone12&mini.jpg", false, "iPhone 12", 1049.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "BrandId", "Description", "ImagePath", "IsArchived", "Name", "Price", "ReleaseDate" },
                values: new object[] { 2, 0.0, 1, "This is fluff for now", "/iphone12max&pro.jpg", false, "iPhone 12 Pro", 1149.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "BrandId", "Description", "ImagePath", "IsArchived", "Name", "Price", "ReleaseDate" },
                values: new object[] { 13, 0.0, 2, "This is fluff for now", "/galaxyS10.jpg", false, "Samsung Galaxy S10", 849.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "BrandId", "Description", "ImagePath", "IsArchived", "Name", "Price", "ReleaseDate" },
                values: new object[] { 1, 0.0, 1, "This is fluff for now", "/iphone12max&pro.jpg", false, "iPhone 12 Pro Max", 1249.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 7155, @"Phasellus sit amet erat. Nulla tempus. Vivamus in felis eu sapien cursus vestibulum.
Proin eu mi. Nulla ac enim. In tempor, turpis nec euismod scelerisque, quam turpis adipiscing lorem, vitae mattis nibh ligula nec sem.", null, new DateTime(2019, 6, 15, 6, 23, 0, 0, DateTimeKind.Unspecified), "63062", false, null, 1 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 81677, @"Sed sagittis. Nam congue, risus semper porta volutpat, quam pede lobortis ligula, sit amet eleifend pede libero quis orci. Nullam molestie nibh in lectus.
Pellentesque at nulla. Suspendisse potenti. Cras in purus eu magna vulputate luctus.", null, new DateTime(2019, 7, 7, 0, 23, 0, 0, DateTimeKind.Unspecified), "79354", false, 5159, 7 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 75077, @"Mauris enim leo, rhoncus sed, vestibulum sit amet, cursus id, turpis. Integer aliquet, massa id lobortis convallis, tortor risus dapibus augue, vel accumsan tellus nisi eu orci. Mauris lacinia sapien quis libero.
Nullam sit amet turpis elementum ligula vehicula consequat. Morbi a ipsum. Integer a nibh.", null, new DateTime(2020, 10, 25, 23, 47, 0, 0, DateTimeKind.Unspecified), "83912", false, null, 4 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 72706, @"In congue. Etiam justo. Etiam pretium iaculis justo.
In hac habitasse platea dictumst. Etiam faucibus cursus urna. Ut tellus.", null, new DateTime(2020, 7, 8, 3, 18, 0, 0, DateTimeKind.Unspecified), "4345", false, 75077, 4 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 44515, @"Suspendisse potenti. In eleifend quam a odio. In hac habitasse platea dictumst.
Maecenas ut massa quis augue luctus tincidunt. Nulla mollis molestie lorem. Quisque ut erat.
Curabitur gravida nisi at nibh. In hac habitasse platea dictumst. Aliquam augue quam, sollicitudin vitae, consectetuer eget, rutrum at, lorem.", null, new DateTime(2018, 7, 11, 5, 26, 0, 0, DateTimeKind.Unspecified), "63062", false, 72706, 4 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 15986, @"Integer tincidunt ante vel ipsum. Praesent blandit lacinia erat. Vestibulum sed magna at nunc commodo placerat.
Praesent blandit. Nam nulla. Integer pede justo, lacinia eget, tincidunt eget, tempus vel, pede.
Morbi porttitor lorem id ligula. Suspendisse ornare consequat lectus. In est risus, auctor sed, tristique in, tempus sit amet, sem.", null, new DateTime(2019, 4, 5, 19, 4, 0, 0, DateTimeKind.Unspecified), "61437", false, 25133, 8 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 77379, "Donec diam neque, vestibulum eget, vulputate ut, ultrices vel, augue. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Donec pharetra, magna vestibulum aliquet ultrices, erat tortor sollicitudin mi, sit amet lobortis sapien sapien non mi. Integer ac neque.", null, new DateTime(2018, 3, 22, 12, 27, 0, 0, DateTimeKind.Unspecified), "26600", false, 17963, 8 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 25133, "Morbi porttitor lorem id ligula. Suspendisse ornare consequat lectus. In est risus, auctor sed, tristique in, tempus sit amet, sem.", null, new DateTime(2019, 12, 12, 16, 22, 0, 0, DateTimeKind.Unspecified), "42923", false, 17963, 8 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 60624, @"Proin eu mi. Nulla ac enim. In tempor, turpis nec euismod scelerisque, quam turpis adipiscing lorem, vitae mattis nibh ligula nec sem.
Duis aliquam convallis nunc. Proin at turpis a pede posuere nonummy. Integer non velit.
Donec diam neque, vestibulum eget, vulputate ut, ultrices vel, augue. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Donec pharetra, magna vestibulum aliquet ultrices, erat tortor sollicitudin mi, sit amet lobortis sapien sapien non mi. Integer ac neque.", null, new DateTime(2020, 1, 14, 6, 37, 0, 0, DateTimeKind.Unspecified), "6561", false, null, 5 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 46382, "Mauris enim leo, rhoncus sed, vestibulum sit amet, cursus id, turpis. Integer aliquet, massa id lobortis convallis, tortor risus dapibus augue, vel accumsan tellus nisi eu orci. Mauris lacinia sapien quis libero.", null, new DateTime(2018, 9, 8, 22, 30, 0, 0, DateTimeKind.Unspecified), "42923", false, null, 9 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 8832, "Cras non velit nec nisi vulputate nonummy. Maecenas tincidunt lacus at velit. Vivamus vel nulla eget eros elementum pellentesque.", null, new DateTime(2018, 2, 28, 21, 26, 0, 0, DateTimeKind.Unspecified), "21564", false, 60624, 5 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 17963, "Cras non velit nec nisi vulputate nonummy. Maecenas tincidunt lacus at velit. Vivamus vel nulla eget eros elementum pellentesque.", null, new DateTime(2018, 4, 9, 12, 55, 0, 0, DateTimeKind.Unspecified), "61437", false, null, 8 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 5500, @"Sed sagittis. Nam congue, risus semper porta volutpat, quam pede lobortis ligula, sit amet eleifend pede libero quis orci. Nullam molestie nibh in lectus.
Pellentesque at nulla. Suspendisse potenti. Cras in purus eu magna vulputate luctus.", null, new DateTime(2019, 6, 10, 22, 43, 0, 0, DateTimeKind.Unspecified), "21564", false, null, 6 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 43501, @"Duis bibendum, felis sed interdum venenatis, turpis enim blandit mi, in porttitor pede justo eu massa. Donec dapibus. Duis at velit eu est congue elementum.
In hac habitasse platea dictumst. Morbi vestibulum, velit id pretium iaculis, diam erat fermentum justo, nec condimentum neque sapien placerat ante. Nulla justo.
Aliquam quis turpis eget elit sodales scelerisque. Mauris sit amet eros. Suspendisse accumsan tortor quis turpis.", null, new DateTime(2020, 5, 22, 5, 15, 0, 0, DateTimeKind.Unspecified), "6561", false, 5500, 6 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 61911, @"Duis bibendum, felis sed interdum venenatis, turpis enim blandit mi, in porttitor pede justo eu massa. Donec dapibus. Duis at velit eu est congue elementum.
In hac habitasse platea dictumst. Morbi vestibulum, velit id pretium iaculis, diam erat fermentum justo, nec condimentum neque sapien placerat ante. Nulla justo.", null, new DateTime(2019, 3, 13, 2, 31, 0, 0, DateTimeKind.Unspecified), "21564", false, 5500, 6 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 15907, @"Nam ultrices, libero non mattis pulvinar, nulla pede ullamcorper augue, a suscipit nulla elit ac nulla. Sed vel enim sit amet nunc viverra dapibus. Nulla suscipit ligula in lacus.
Curabitur at ipsum ac tellus semper interdum. Mauris ullamcorper purus sit amet nulla. Quisque arcu libero, rutrum ac, lobortis vel, dapibus at, diam.", null, new DateTime(2020, 12, 24, 3, 32, 0, 0, DateTimeKind.Unspecified), "63062", false, 5500, 6 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 5399, "Aenean lectus. Pellentesque eget nunc. Donec quis orci eget orci vehicula condimentum.", null, new DateTime(2020, 9, 29, 21, 47, 0, 0, DateTimeKind.Unspecified), "21564", false, 43501, 6 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 52881, "Fusce consequat. Nulla nisl. Nunc nisl.", null, new DateTime(2019, 6, 28, 19, 22, 0, 0, DateTimeKind.Unspecified), "85497", false, 43501, 6 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 74034, @"Vestibulum quam sapien, varius ut, blandit non, interdum in, ante. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Duis faucibus accumsan odio. Curabitur convallis.
Duis consequat dui nec nisi volutpat eleifend. Donec ut dolor. Morbi vel lectus in quam fringilla rhoncus.", null, new DateTime(2020, 2, 25, 13, 47, 0, 0, DateTimeKind.Unspecified), "85497", false, 5159, 7 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 80156, "Integer tincidunt ante vel ipsum. Praesent blandit lacinia erat. Vestibulum sed magna at nunc commodo placerat.", null, new DateTime(2019, 8, 15, 16, 19, 0, 0, DateTimeKind.Unspecified), "78347", false, 8832, 5 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 66609, "Phasellus in felis. Donec semper sapien a libero. Nam dui.", null, new DateTime(2019, 10, 5, 6, 2, 0, 0, DateTimeKind.Unspecified), "67588", false, 56806, 3 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 5159, @"Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Proin risus. Praesent lectus.
Vestibulum quam sapien, varius ut, blandit non, interdum in, ante. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Duis faucibus accumsan odio. Curabitur convallis.
Duis consequat dui nec nisi volutpat eleifend. Donec ut dolor. Morbi vel lectus in quam fringilla rhoncus.", null, new DateTime(2018, 9, 6, 10, 32, 0, 0, DateTimeKind.Unspecified), "78347", false, null, 7 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 56806, @"Maecenas ut massa quis augue luctus tincidunt. Nulla mollis molestie lorem. Quisque ut erat.
Curabitur gravida nisi at nibh. In hac habitasse platea dictumst. Aliquam augue quam, sollicitudin vitae, consectetuer eget, rutrum at, lorem.
Integer tincidunt ante vel ipsum. Praesent blandit lacinia erat. Vestibulum sed magna at nunc commodo placerat.", null, new DateTime(2018, 10, 30, 3, 57, 0, 0, DateTimeKind.Unspecified), "73398", false, 62903, 3 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 48229, @"Duis aliquam convallis nunc. Proin at turpis a pede posuere nonummy. Integer non velit.
Donec diam neque, vestibulum eget, vulputate ut, ultrices vel, augue. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Donec pharetra, magna vestibulum aliquet ultrices, erat tortor sollicitudin mi, sit amet lobortis sapien sapien non mi. Integer ac neque.
Duis bibendum. Morbi non quam nec dui luctus rutrum. Nulla tellus.", null, new DateTime(2018, 5, 9, 6, 23, 0, 0, DateTimeKind.Unspecified), "83912", false, 7155, 1 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 55854, @"Duis bibendum, felis sed interdum venenatis, turpis enim blandit mi, in porttitor pede justo eu massa. Donec dapibus. Duis at velit eu est congue elementum.
In hac habitasse platea dictumst. Morbi vestibulum, velit id pretium iaculis, diam erat fermentum justo, nec condimentum neque sapien placerat ante. Nulla justo.
Aliquam quis turpis eget elit sodales scelerisque. Mauris sit amet eros. Suspendisse accumsan tortor quis turpis.", null, new DateTime(2020, 5, 6, 16, 46, 0, 0, DateTimeKind.Unspecified), "77416", false, 7155, 1 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 87232, "Phasellus sit amet erat. Nulla tempus. Vivamus in felis eu sapien cursus vestibulum.", null, new DateTime(2020, 6, 11, 12, 20, 0, 0, DateTimeKind.Unspecified), "67706", false, 48229, 1 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 9541, "Fusce consequat. Nulla nisl. Nunc nisl.", null, new DateTime(2020, 4, 16, 20, 28, 0, 0, DateTimeKind.Unspecified), "42923", false, 55854, 1 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 53822, @"In quis justo. Maecenas rhoncus aliquam lacus. Morbi quis tortor id nulla ultrices aliquet.
Maecenas leo odio, condimentum id, luctus nec, molestie sed, justo. Pellentesque viverra pede ac diam. Cras pellentesque volutpat dui.", null, new DateTime(2021, 1, 5, 1, 46, 0, 0, DateTimeKind.Unspecified), "73398", false, 62903, 3 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 27571, @"Morbi porttitor lorem id ligula. Suspendisse ornare consequat lectus. In est risus, auctor sed, tristique in, tempus sit amet, sem.
Fusce consequat. Nulla nisl. Nunc nisl.
Duis bibendum, felis sed interdum venenatis, turpis enim blandit mi, in porttitor pede justo eu massa. Donec dapibus. Duis at velit eu est congue elementum.", null, new DateTime(2020, 3, 1, 9, 15, 0, 0, DateTimeKind.Unspecified), "77416", false, 33112, 2 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 62852, @"Suspendisse potenti. In eleifend quam a odio. In hac habitasse platea dictumst.
Maecenas ut massa quis augue luctus tincidunt. Nulla mollis molestie lorem. Quisque ut erat.", null, new DateTime(2019, 8, 3, 22, 21, 0, 0, DateTimeKind.Unspecified), "83912", false, 27571, 2 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 43195, @"In quis justo. Maecenas rhoncus aliquam lacus. Morbi quis tortor id nulla ultrices aliquet.
Maecenas leo odio, condimentum id, luctus nec, molestie sed, justo. Pellentesque viverra pede ac diam. Cras pellentesque volutpat dui.
Maecenas tristique, est et tempus semper, est quam pharetra magna, ac consequat metus sapien ut nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Mauris viverra diam vitae quam. Suspendisse potenti.", null, new DateTime(2021, 2, 5, 16, 29, 0, 0, DateTimeKind.Unspecified), "67706", false, 62852, 2 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 33112, "Fusce posuere felis sed lacus. Morbi sem mauris, laoreet ut, rhoncus aliquet, pulvinar sed, nisl. Nunc rhoncus dui vel sem.", null, new DateTime(2020, 5, 22, 7, 49, 0, 0, DateTimeKind.Unspecified), "67706", false, null, 2 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 71712, @"In hac habitasse platea dictumst. Etiam faucibus cursus urna. Ut tellus.
Nulla ut erat id mauris vulputate elementum. Nullam varius. Nulla facilisi.", null, new DateTime(2018, 6, 17, 9, 49, 0, 0, DateTimeKind.Unspecified), "67588", false, 51175, 9 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 51175, "Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vivamus vestibulum sagittis sapien. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.", null, new DateTime(2020, 7, 18, 18, 14, 0, 0, DateTimeKind.Unspecified), "67588", false, 16852, 9 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 16852, "Fusce consequat. Nulla nisl. Nunc nisl.", null, new DateTime(2019, 12, 4, 18, 11, 0, 0, DateTimeKind.Unspecified), "79354", false, 46382, 9 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 62903, @"In congue. Etiam justo. Etiam pretium iaculis justo.
In hac habitasse platea dictumst. Etiam faucibus cursus urna. Ut tellus.
Nulla ut erat id mauris vulputate elementum. Nullam varius. Nulla facilisi.", null, new DateTime(2018, 5, 27, 12, 48, 0, 0, DateTimeKind.Unspecified), "63062", false, null, 3 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 66096, @"Vestibulum quam sapien, varius ut, blandit non, interdum in, ante. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Duis faucibus accumsan odio. Curabitur convallis.
Duis consequat dui nec nisi volutpat eleifend. Donec ut dolor. Morbi vel lectus in quam fringilla rhoncus.", null, new DateTime(2020, 5, 18, 17, 29, 0, 0, DateTimeKind.Unspecified), "26600", false, 46382, 9 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentId1", "CreationDate", "CreatorUserId", "IsArchived", "ParentCommentId", "PostId" },
                values: new object[] { 47216, @"Phasellus sit amet erat. Nulla tempus. Vivamus in felis eu sapien cursus vestibulum.
Proin eu mi. Nulla ac enim. In tempor, turpis nec euismod scelerisque, quam turpis adipiscing lorem, vitae mattis nibh ligula nec sem.", null, new DateTime(2020, 4, 19, 4, 10, 0, 0, DateTimeKind.Unspecified), "67588", false, 43195, 2 });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 85004, new DateTime(2019, 1, 11, 21, 51, 0, 0, DateTimeKind.Unspecified), false, false, 8, "83912" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 64214, new DateTime(2020, 5, 7, 21, 30, 0, 0, DateTimeKind.Unspecified), false, true, 7, "26600" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 20858, new DateTime(2019, 8, 2, 7, 42, 0, 0, DateTimeKind.Unspecified), false, false, 8, "67706" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 51950, new DateTime(2020, 2, 12, 1, 48, 0, 0, DateTimeKind.Unspecified), false, true, 9, "78347" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 29030, new DateTime(2020, 1, 13, 20, 33, 0, 0, DateTimeKind.Unspecified), false, false, 9, "42923" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 28775, new DateTime(2019, 1, 21, 12, 31, 0, 0, DateTimeKind.Unspecified), false, true, 7, "61437" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 79265, new DateTime(2020, 4, 13, 19, 20, 0, 0, DateTimeKind.Unspecified), false, true, 7, "85497" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 74359, new DateTime(2019, 7, 9, 7, 33, 0, 0, DateTimeKind.Unspecified), false, false, 7, "21564" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 51984, new DateTime(2019, 7, 7, 0, 42, 0, 0, DateTimeKind.Unspecified), false, false, 9, "77416" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 47030, new DateTime(2019, 4, 13, 1, 34, 0, 0, DateTimeKind.Unspecified), false, false, 8, "73398" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 80709, new DateTime(2020, 2, 2, 3, 31, 0, 0, DateTimeKind.Unspecified), false, true, 9, "79354" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 10443, new DateTime(2018, 12, 26, 23, 34, 0, 0, DateTimeKind.Unspecified), false, false, 8, "63062" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 36433, new DateTime(2020, 6, 7, 9, 55, 0, 0, DateTimeKind.Unspecified), false, true, 5, "77416" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 15615, new DateTime(2019, 4, 21, 4, 31, 0, 0, DateTimeKind.Unspecified), false, false, 6, "4345" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 47995, new DateTime(2019, 12, 26, 19, 49, 0, 0, DateTimeKind.Unspecified), false, false, 1, "63062" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 34143, new DateTime(2018, 12, 28, 0, 36, 0, 0, DateTimeKind.Unspecified), false, true, 1, "83912" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 40119, new DateTime(2018, 7, 17, 13, 29, 0, 0, DateTimeKind.Unspecified), false, true, 1, "77416" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 37162, new DateTime(2018, 11, 25, 5, 54, 0, 0, DateTimeKind.Unspecified), false, true, 1, "42923" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 10544, new DateTime(2018, 8, 1, 1, 10, 0, 0, DateTimeKind.Unspecified), false, false, 2, "79354" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 82944, new DateTime(2018, 3, 29, 0, 37, 0, 0, DateTimeKind.Unspecified), false, false, 2, "78347" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 61035, new DateTime(2019, 3, 30, 12, 10, 0, 0, DateTimeKind.Unspecified), false, false, 2, "6561" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 77873, new DateTime(2020, 1, 16, 18, 47, 0, 0, DateTimeKind.Unspecified), false, true, 2, "4345" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 74012, new DateTime(2020, 4, 10, 20, 52, 0, 0, DateTimeKind.Unspecified), false, false, 3, "67588" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 1966, new DateTime(2019, 4, 18, 19, 1, 0, 0, DateTimeKind.Unspecified), false, false, 3, "26600" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 13319, new DateTime(2018, 8, 24, 11, 33, 0, 0, DateTimeKind.Unspecified), false, true, 6, "67588" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 52786, new DateTime(2019, 7, 30, 11, 14, 0, 0, DateTimeKind.Unspecified), false, false, 3, "61437" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 57865, new DateTime(2020, 6, 14, 22, 51, 0, 0, DateTimeKind.Unspecified), false, false, 4, "21564" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 77670, new DateTime(2020, 3, 2, 16, 14, 0, 0, DateTimeKind.Unspecified), false, true, 4, "73398" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 9873, new DateTime(2019, 9, 9, 10, 30, 0, 0, DateTimeKind.Unspecified), false, false, 4, "67706" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 84465, new DateTime(2019, 6, 10, 23, 3, 0, 0, DateTimeKind.Unspecified), false, true, 4, "63062" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 43453, new DateTime(2019, 6, 24, 16, 13, 0, 0, DateTimeKind.Unspecified), false, false, 5, "83912" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 33000, new DateTime(2020, 11, 14, 11, 44, 0, 0, DateTimeKind.Unspecified), false, true, 9, "6561" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 20089, new DateTime(2020, 12, 6, 21, 13, 0, 0, DateTimeKind.Unspecified), false, false, 5, "42923" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 42537, new DateTime(2019, 4, 15, 22, 7, 0, 0, DateTimeKind.Unspecified), false, true, 5, "79354" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 84847, new DateTime(2018, 11, 12, 19, 30, 0, 0, DateTimeKind.Unspecified), false, false, 6, "78347" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 46496, new DateTime(2018, 6, 27, 20, 47, 0, 0, DateTimeKind.Unspecified), false, false, 6, "6561" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 39810, new DateTime(2020, 9, 7, 15, 58, 0, 0, DateTimeKind.Unspecified), false, true, 3, "85497" });

            migrationBuilder.InsertData(
                table: "PostVotes",
                columns: new[] { "PostVoteId", "CreationDate", "IsArchived", "IsUp", "PostId", "UserId" },
                values: new object[] { 28643, new DateTime(2018, 9, 26, 0, 28, 0, 0, DateTimeKind.Unspecified), false, true, 9, "4345" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 62563, 7155, new DateTime(2018, 7, 23, 20, 22, 0, 0, DateTimeKind.Unspecified), false, false, "63062" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 37575, 43501, new DateTime(2021, 2, 8, 23, 2, 0, 0, DateTimeKind.Unspecified), false, true, "83912" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 20541, 61911, new DateTime(2018, 8, 25, 17, 31, 0, 0, DateTimeKind.Unspecified), false, true, "61437" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 37291, 15907, new DateTime(2018, 3, 2, 3, 45, 0, 0, DateTimeKind.Unspecified), false, false, "4345" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 20999, 5399, new DateTime(2021, 2, 14, 3, 55, 0, 0, DateTimeKind.Unspecified), false, true, "42923" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 57672, 52881, new DateTime(2018, 6, 21, 12, 22, 0, 0, DateTimeKind.Unspecified), false, false, "78347" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 62190, 5159, new DateTime(2020, 7, 29, 11, 12, 0, 0, DateTimeKind.Unspecified), false, false, "26600" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 35519, 81677, new DateTime(2020, 6, 8, 8, 15, 0, 0, DateTimeKind.Unspecified), false, false, "77416" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 78546, 74034, new DateTime(2019, 9, 23, 10, 39, 0, 0, DateTimeKind.Unspecified), false, false, "78347" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 88693, 17963, new DateTime(2019, 4, 22, 17, 3, 0, 0, DateTimeKind.Unspecified), false, true, "79354" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 55617, 25133, new DateTime(2018, 5, 22, 17, 8, 0, 0, DateTimeKind.Unspecified), false, true, "77416" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 22185, 77379, new DateTime(2020, 7, 12, 5, 8, 0, 0, DateTimeKind.Unspecified), false, true, "26600" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 78543, 15986, new DateTime(2018, 4, 14, 23, 11, 0, 0, DateTimeKind.Unspecified), false, false, "79354" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 73781, 46382, new DateTime(2018, 6, 25, 0, 14, 0, 0, DateTimeKind.Unspecified), false, true, "77416" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 51272, 66096, new DateTime(2018, 5, 5, 22, 43, 0, 0, DateTimeKind.Unspecified), false, true, "61437" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 16966, 16852, new DateTime(2018, 6, 27, 20, 2, 0, 0, DateTimeKind.Unspecified), false, false, "78347" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 62706, 5500, new DateTime(2020, 10, 6, 22, 26, 0, 0, DateTimeKind.Unspecified), false, false, "67588" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 25805, 80156, new DateTime(2019, 10, 17, 22, 51, 0, 0, DateTimeKind.Unspecified), false, true, "4345" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 32387, 8832, new DateTime(2019, 12, 22, 6, 48, 0, 0, DateTimeKind.Unspecified), false, true, "67588" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 64607, 60624, new DateTime(2021, 1, 12, 12, 10, 0, 0, DateTimeKind.Unspecified), false, true, "83912" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 26760, 48229, new DateTime(2019, 10, 17, 14, 30, 0, 0, DateTimeKind.Unspecified), false, true, "83912" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 31394, 55854, new DateTime(2019, 9, 1, 15, 5, 0, 0, DateTimeKind.Unspecified), false, true, "63062" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 50312, 87232, new DateTime(2018, 6, 21, 19, 8, 0, 0, DateTimeKind.Unspecified), false, false, "77416" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 14200, 9541, new DateTime(2019, 7, 25, 15, 49, 0, 0, DateTimeKind.Unspecified), false, false, "83912" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 42574, 33112, new DateTime(2020, 12, 5, 23, 4, 0, 0, DateTimeKind.Unspecified), false, true, "42923" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 24309, 27571, new DateTime(2020, 12, 29, 0, 4, 0, 0, DateTimeKind.Unspecified), false, false, "83912" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 89339, 62852, new DateTime(2019, 6, 26, 4, 36, 0, 0, DateTimeKind.Unspecified), false, false, "21564" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 39316, 51175, new DateTime(2019, 7, 27, 2, 59, 0, 0, DateTimeKind.Unspecified), false, false, "79354" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 48135, 43195, new DateTime(2019, 3, 19, 8, 54, 0, 0, DateTimeKind.Unspecified), false, true, "42923" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 67785, 62903, new DateTime(2019, 11, 12, 15, 49, 0, 0, DateTimeKind.Unspecified), false, true, "6561" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 52972, 56806, new DateTime(2018, 4, 29, 14, 24, 0, 0, DateTimeKind.Unspecified), false, true, "63062" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 82330, 53822, new DateTime(2018, 10, 1, 5, 14, 0, 0, DateTimeKind.Unspecified), false, false, "6561" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 76289, 66609, new DateTime(2019, 6, 21, 16, 11, 0, 0, DateTimeKind.Unspecified), false, true, "42923" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 28431, 75077, new DateTime(2018, 11, 14, 6, 29, 0, 0, DateTimeKind.Unspecified), false, false, "4345" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 75814, 72706, new DateTime(2020, 8, 12, 4, 33, 0, 0, DateTimeKind.Unspecified), false, false, "73398" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 72701, 44515, new DateTime(2020, 5, 8, 14, 17, 0, 0, DateTimeKind.Unspecified), false, false, "6561" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 64023, 47216, new DateTime(2018, 11, 18, 7, 28, 0, 0, DateTimeKind.Unspecified), false, true, "83912" });

            migrationBuilder.InsertData(
                table: "CommentVotes",
                columns: new[] { "CommentVoteId", "CommentId", "CreationDate", "IsArchived", "IsUp", "UserId" },
                values: new object[] { 41021, 71712, new DateTime(2018, 12, 31, 1, 30, 0, 0, DateTimeKind.Unspecified), false, true, "85497" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentId1",
                table: "Comments",
                column: "CommentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CreatorUserId",
                table: "Comments",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentVotes_CommentId",
                table: "CommentVotes",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentVotes_UserId",
                table: "CommentVotes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatorUserId",
                table: "Posts",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostVotes_PostId",
                table: "PostVotes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostVotes_UserId",
                table: "PostVotes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentVotes");

            migrationBuilder.DropTable(
                name: "PostVotes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
