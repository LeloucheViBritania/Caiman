﻿// <auto-generated />
using System;
using CaimanProject.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CaimanProject.Migrations
{
    [DbContext(typeof(DbCaimanContext))]
    partial class DbCaimanContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CaimanProject.Models.Competence", b =>
                {
                    b.Property<int>("CompetenceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompetenceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MemberId")
                        .HasColumnType("int");

                    b.HasKey("CompetenceId");

                    b.HasIndex("MemberId");

                    b.ToTable("Competences");
                });

            modelBuilder.Entity("CaimanProject.Models.Contact", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactFonction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ContactNumber")
                        .HasColumnType("int");

                    b.Property<string>("ContactPname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactSite")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContactId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("CaimanProject.Models.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsChecked")
                        .HasColumnType("bit");

                    b.Property<string>("MemberCommune")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MemberDateArchive")
                        .HasColumnType("datetime2");

                    b.Property<string>("MemberDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("MemberIsArchived")
                        .HasColumnType("bit");

                    b.Property<string>("MemberLieuNaissance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberMail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MemberMissionActive")
                        .HasColumnType("int");

                    b.Property<int>("MemberMissonFin")
                        .HasColumnType("int");

                    b.Property<DateTime>("MemberNaissance")
                        .HasColumnType("datetime2");

                    b.Property<string>("MemberName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MemberNote")
                        .HasColumnType("int");

                    b.Property<int>("MemberPhone")
                        .HasColumnType("int");

                    b.Property<string>("MemberPnom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberQuartier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberSex")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpecialiteId")
                        .HasColumnType("int");

                    b.Property<int>("TransportId")
                        .HasColumnType("int");

                    b.HasKey("MemberId");

                    b.HasIndex("SpecialiteId");

                    b.HasIndex("TransportId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("CaimanProject.Models.NoteP", b =>
                {
                    b.Property<int>("NotePId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("NotePDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NotePDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjetId")
                        .HasColumnType("int");

                    b.HasKey("NotePId");

                    b.HasIndex("ProjetId");

                    b.ToTable("NotePs");
                });

            modelBuilder.Entity("CaimanProject.Models.Projet", b =>
                {
                    b.Property<int>("ProjetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsArchieved")
                        .HasColumnType("bit");

                    b.Property<string>("ProjetCahierCharge")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ProjetDateDebut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ProjetDateFin")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProjetDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjetMoney")
                        .HasColumnType("int");

                    b.Property<string>("ProjetName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjetProgressBar")
                        .HasColumnType("int");

                    b.HasKey("ProjetId");

                    b.ToTable("Projets");
                });

            modelBuilder.Entity("CaimanProject.Models.ProjetMember", b =>
                {
                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("ProjetId")
                        .HasColumnType("int");

                    b.HasKey("MemberId", "ProjetId");

                    b.HasIndex("ProjetId");

                    b.ToTable("ProjetMembers");
                });

            modelBuilder.Entity("CaimanProject.Models.SocialNetwork", b =>
                {
                    b.Property<int>("SocialNetworkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MemberId")
                        .HasColumnType("int");

                    b.Property<string>("NetworkLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NetworkName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SocialNetworkId");

                    b.HasIndex("MemberId");

                    b.ToTable("SocialNetworks");
                });

            modelBuilder.Entity("CaimanProject.Models.Specialite", b =>
                {
                    b.Property<int>("SpecialiteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageSpecialité")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecialiteColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecialiteName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url_Image")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SpecialiteId");

                    b.ToTable("Specialites");
                });

            modelBuilder.Entity("CaimanProject.Models.Transport", b =>
                {
                    b.Property<int>("TransportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TranportName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TransportId");

                    b.ToTable("Transports");
                });

            modelBuilder.Entity("CaimanProject.Models.Competence", b =>
                {
                    b.HasOne("CaimanProject.Models.Member", "Member")
                        .WithMany("Competences")
                        .HasForeignKey("MemberId");
                });

            modelBuilder.Entity("CaimanProject.Models.Member", b =>
                {
                    b.HasOne("CaimanProject.Models.Specialite", null)
                        .WithMany("Members")
                        .HasForeignKey("SpecialiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CaimanProject.Models.Transport", null)
                        .WithMany("Members")
                        .HasForeignKey("TransportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CaimanProject.Models.NoteP", b =>
                {
                    b.HasOne("CaimanProject.Models.Projet", "Projet")
                        .WithMany("NotePs")
                        .HasForeignKey("ProjetId");
                });

            modelBuilder.Entity("CaimanProject.Models.ProjetMember", b =>
                {
                    b.HasOne("CaimanProject.Models.Member", "Member")
                        .WithMany("ProjetMembers")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CaimanProject.Models.Projet", "Projet")
                        .WithMany("ProjetMembers")
                        .HasForeignKey("ProjetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CaimanProject.Models.SocialNetwork", b =>
                {
                    b.HasOne("CaimanProject.Models.Member", "Member")
                        .WithMany("SocialNetworks")
                        .HasForeignKey("MemberId");
                });
#pragma warning restore 612, 618
        }
    }
}
