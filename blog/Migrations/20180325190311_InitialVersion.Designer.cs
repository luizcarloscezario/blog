﻿// <auto-generated />
using blog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace blog.Migrations
{
    [DbContext(typeof(CrossBlogDbContext))]
    [Migration("20180325190311_InitialVersion")]
    partial class InitialVersion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("blog.Domain.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("Created_At");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("Published");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("Updated_At");

                    b.HasKey("Id");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("blog.Domain.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ArticleId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("Created_At");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<bool>("Published");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("Updated_At");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("blog.Domain.Comment", b =>
                {
                    b.HasOne("blog.Domain.Article", "Article")
                        .WithMany()
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
