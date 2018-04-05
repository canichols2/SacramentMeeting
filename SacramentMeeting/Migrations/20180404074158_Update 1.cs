using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SacramentMeeting.Migrations
{
    public partial class Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberCalling_Calling_CallingId",
                table: "MemberCalling");

            migrationBuilder.AlterColumn<int>(
                name: "IntermediateSong",
                table: "Sacrament",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ConductingId",
                table: "Sacrament",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PresidingId",
                table: "Sacrament",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CallingId",
                table: "MemberCalling",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Sacrament_ConductingId",
                table: "Sacrament",
                column: "ConductingId");

            migrationBuilder.CreateIndex(
                name: "IX_Sacrament_PresidingId",
                table: "Sacrament",
                column: "PresidingId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberCalling_Calling_CallingId",
                table: "MemberCalling",
                column: "CallingId",
                principalTable: "Calling",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sacrament_Member_ConductingId",
                table: "Sacrament",
                column: "ConductingId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sacrament_Member_PresidingId",
                table: "Sacrament",
                column: "PresidingId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberCalling_Calling_CallingId",
                table: "MemberCalling");

            migrationBuilder.DropForeignKey(
                name: "FK_Sacrament_Member_ConductingId",
                table: "Sacrament");

            migrationBuilder.DropForeignKey(
                name: "FK_Sacrament_Member_PresidingId",
                table: "Sacrament");

            migrationBuilder.DropIndex(
                name: "IX_Sacrament_ConductingId",
                table: "Sacrament");

            migrationBuilder.DropIndex(
                name: "IX_Sacrament_PresidingId",
                table: "Sacrament");

            migrationBuilder.DropColumn(
                name: "ConductingId",
                table: "Sacrament");

            migrationBuilder.DropColumn(
                name: "PresidingId",
                table: "Sacrament");

            migrationBuilder.AlterColumn<int>(
                name: "IntermediateSong",
                table: "Sacrament",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CallingId",
                table: "MemberCalling",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberCalling_Calling_CallingId",
                table: "MemberCalling",
                column: "CallingId",
                principalTable: "Calling",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
