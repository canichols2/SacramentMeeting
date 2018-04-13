﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SacramentMeeting.Models;
using System;

namespace SacramentMeeting.Migrations
{
    [DbContext(typeof(SacramentContext))]
    partial class SacramentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SacramentMeeting.Models.Calling", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CallingName");

                    b.HasKey("Id");

                    b.ToTable("Calling");
                });

            modelBuilder.Entity("SacramentMeeting.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BaptizeDate");

                    b.Property<string>("FirstMiddleName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("SacramentMeeting.Models.MemberCalling", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<int?>("CallingId");

                    b.Property<int>("MemberId");

                    b.HasKey("Id");

                    b.HasIndex("CallingId");

                    b.HasIndex("MemberId");

                    b.ToTable("MemberCalling");
                });

            modelBuilder.Entity("SacramentMeeting.Models.Sacrament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BenedictionId");

                    b.Property<int>("ClosingSong");

                    b.Property<int?>("ConductingId");

                    b.Property<int?>("IntermediateSong");

                    b.Property<int?>("InvocationId");

                    b.Property<int>("OpeningSong");

                    b.Property<int?>("PresidingId");

                    b.Property<int>("SacramentSong");

                    b.Property<DateTime>("date");

                    b.HasKey("Id");

                    b.HasIndex("BenedictionId");

                    b.HasIndex("ConductingId");

                    b.HasIndex("InvocationId");

                    b.HasIndex("PresidingId");

                    b.ToTable("Sacrament");
                });

            modelBuilder.Entity("SacramentMeeting.Models.Speakers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MemberID");

                    b.Property<int>("SacramentID");

                    b.Property<int?>("TopicID");

                    b.HasKey("Id");

                    b.HasIndex("MemberID");

                    b.HasIndex("SacramentID");

                    b.HasIndex("TopicID");

                    b.ToTable("Speakers");
                });

            modelBuilder.Entity("SacramentMeeting.Models.SpeakerTopic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Topic");

                    b.HasKey("Id");

                    b.ToTable("SpeakerTopic");
                });

            modelBuilder.Entity("SacramentMeeting.Models.MemberCalling", b =>
                {
                    b.HasOne("SacramentMeeting.Models.Calling", "Calling")
                        .WithMany("MemberCalling")
                        .HasForeignKey("CallingId");

                    b.HasOne("SacramentMeeting.Models.Member", "Member")
                        .WithMany("MemberCalling")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SacramentMeeting.Models.Sacrament", b =>
                {
                    b.HasOne("SacramentMeeting.Models.Member", "Benediction")
                        .WithMany()
                        .HasForeignKey("BenedictionId");

                    b.HasOne("SacramentMeeting.Models.Member", "Conducting")
                        .WithMany()
                        .HasForeignKey("ConductingId");

                    b.HasOne("SacramentMeeting.Models.Member", "Invocation")
                        .WithMany()
                        .HasForeignKey("InvocationId");

                    b.HasOne("SacramentMeeting.Models.Member", "Presiding")
                        .WithMany()
                        .HasForeignKey("PresidingId");
                });

            modelBuilder.Entity("SacramentMeeting.Models.Speakers", b =>
                {
                    b.HasOne("SacramentMeeting.Models.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SacramentMeeting.Models.Sacrament", "Sacrament")
                        .WithMany("Speakers")
                        .HasForeignKey("SacramentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SacramentMeeting.Models.SpeakerTopic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicID");
                });
#pragma warning restore 612, 618
        }
    }
}
