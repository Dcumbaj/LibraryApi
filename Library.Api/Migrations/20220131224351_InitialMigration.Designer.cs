﻿// <auto-generated />
using System;
using Library.Api.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Library.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220131224351_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Library.Api.Models.Bibliotheca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bibliothecas");
                });

            modelBuilder.Entity("Library.Api.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BibliothecaId")
                        .HasColumnType("int");

                    b.Property<int>("BookCategoryId")
                        .HasColumnType("int");

                    b.Property<decimal>("BookPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("NumberOfCopies")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfRents")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BibliothecaId");

                    b.HasIndex("BookCategoryId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Library.Api.Models.BookCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookCategories");
                });

            modelBuilder.Entity("Library.Api.Models.Book", b =>
                {
                    b.HasOne("Library.Api.Models.Bibliotheca", "Bibliotheca")
                        .WithMany()
                        .HasForeignKey("BibliothecaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Api.Models.BookCategory", "BookCategory")
                        .WithMany()
                        .HasForeignKey("BookCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bibliotheca");

                    b.Navigation("BookCategory");
                });
#pragma warning restore 612, 618
        }
    }
}
