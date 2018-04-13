using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SacramentMeeting.Migrations
{
    public partial class AddingBenedictionandInvocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BenedictionId",
                table: "Sacrament",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InvocationId",
                table: "Sacrament",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sacrament_BenedictionId",
                table: "Sacrament",
                column: "BenedictionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sacrament_InvocationId",
                table: "Sacrament",
                column: "InvocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sacrament_Member_BenedictionId",
                table: "Sacrament",
                column: "BenedictionId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sacrament_Member_InvocationId",
                table: "Sacrament",
                column: "InvocationId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sacrament_Member_BenedictionId",
                table: "Sacrament");

            migrationBuilder.DropForeignKey(
                name: "FK_Sacrament_Member_InvocationId",
                table: "Sacrament");

            migrationBuilder.DropIndex(
                name: "IX_Sacrament_BenedictionId",
                table: "Sacrament");

            migrationBuilder.DropIndex(
                name: "IX_Sacrament_InvocationId",
                table: "Sacrament");

            migrationBuilder.DropColumn(
                name: "BenedictionId",
                table: "Sacrament");

            migrationBuilder.DropColumn(
                name: "InvocationId",
                table: "Sacrament");
        }
    }
}
