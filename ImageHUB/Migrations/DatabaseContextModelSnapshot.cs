﻿// <auto-generated />
using System;
using ImageHUB.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ImageHUB.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ImageHUB.Entities.Post", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("OwnerID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("ImageHUB.Entities.PostTag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PostID")
                        .HasColumnType("int");

                    b.Property<int>("TagID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TagID");

                    b.HasIndex("PostID", "TagID");

                    b.ToTable("PostTag");
                });

            modelBuilder.Entity("ImageHUB.Entities.Profile", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("ImageHUB.Entities.ProfileFriend", b =>
                {
                    b.Property<int>("ProfileID")
                        .HasColumnType("int");

                    b.Property<int>("FriendID")
                        .HasColumnType("int");

                    b.Property<bool>("Accepted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.HasKey("ProfileID", "FriendID");

                    b.HasIndex("FriendID");

                    b.ToTable("ProfileFriend");
                });

            modelBuilder.Entity("ImageHUB.Entities.Tag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("ImageHUB.Entities.Post", b =>
                {
                    b.HasOne("ImageHUB.Entities.Profile", "Owner")
                        .WithMany("Posts")
                        .HasForeignKey("OwnerID");
                });

            modelBuilder.Entity("ImageHUB.Entities.PostTag", b =>
                {
                    b.HasOne("ImageHUB.Entities.Post", "Post")
                        .WithMany("Tags")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ImageHUB.Entities.Tag", "Tag")
                        .WithMany("Posts")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ImageHUB.Entities.ProfileFriend", b =>
                {
                    b.HasOne("ImageHUB.Entities.Profile", "Friend")
                        .WithMany("FriendsWith")
                        .HasForeignKey("FriendID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ImageHUB.Entities.Profile", "Profile")
                        .WithMany("FriendsTo")
                        .HasForeignKey("ProfileID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
