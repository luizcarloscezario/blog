using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace blog.Migrations
{
    public partial class ChangeentitieConfigurationaddforeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Configurations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_ArticleId",
                table: "Configurations",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Configurations_Articles_ArticleId",
                table: "Configurations",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configurations_Articles_ArticleId",
                table: "Configurations");

            migrationBuilder.DropIndex(
                name: "IX_Configurations_ArticleId",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Configurations");
        }
    }
}
