﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SacramentMeeting.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calling",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CallingName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calling", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaptizeDate = table.Column<DateTime>(nullable: false),
                    FirstMiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerTopic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Topic = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerTopic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberCalling",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CallingId = table.Column<int>(nullable: true),
                    MemberId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberCalling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberCalling_Calling_CallingId",
                        column: x => x.CallingId,
                        principalTable: "Calling",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberCalling_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sacrament",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClosingSong = table.Column<int>(nullable: false),
                    ConductingId = table.Column<int>(nullable: true),
                    IntermediateSong = table.Column<int>(nullable: true),
                    OpeningSong = table.Column<int>(nullable: false),
                    PresidingId = table.Column<int>(nullable: true),
                    SacramentSong = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sacrament", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sacrament_Member_ConductingId",
                        column: x => x.ConductingId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sacrament_Member_PresidingId",
                        column: x => x.PresidingId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Speakers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MemberID = table.Column<int>(nullable: false),
                    SacramentID = table.Column<int>(nullable: false),
                    TopicID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Speakers_Member_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Speakers_Sacrament_SacramentID",
                        column: x => x.SacramentID,
                        principalTable: "Sacrament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Speakers_SpeakerTopic_TopicID",
                        column: x => x.TopicID,
                        principalTable: "SpeakerTopic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberCalling_CallingId",
                table: "MemberCalling",
                column: "CallingId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCalling_MemberId",
                table: "MemberCalling",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Sacrament_ConductingId",
                table: "Sacrament",
                column: "ConductingId");

            migrationBuilder.CreateIndex(
                name: "IX_Sacrament_PresidingId",
                table: "Sacrament",
                column: "PresidingId");

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_MemberID",
                table: "Speakers",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_SacramentID",
                table: "Speakers",
                column: "SacramentID");

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_TopicID",
                table: "Speakers",
                column: "TopicID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberCalling");

            migrationBuilder.DropTable(
                name: "Speakers");

            migrationBuilder.DropTable(
                name: "Calling");

            migrationBuilder.DropTable(
                name: "Sacrament");

            migrationBuilder.DropTable(
                name: "SpeakerTopic");

            migrationBuilder.DropTable(
                name: "Member");
        }
    }
}
