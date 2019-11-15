﻿// <auto-generated />
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
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099");

            modelBuilder.Entity("ImageHUB.Repositories.Post", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Image");

                    b.Property<string>("OwnerID");

                    b.HasKey("ID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("ImageHUB.Repositories.Profile", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<string>("Email");

                    b.Property<string>("UserName");

                    b.HasKey("ID");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("ImageHUB.Repositories.ProfileFriend", b =>
                {
                    b.Property<string>("ProfileID");

                    b.Property<string>("FriendID");

                    b.Property<bool>("Accepted");

                    b.HasKey("ProfileID", "FriendID");

                    b.HasIndex("FriendID");

                    b.ToTable("ProfileFriend");
                });

            modelBuilder.Entity("ImageHUB.Repositories.Post", b =>
                {
                    b.HasOne("ImageHUB.Repositories.Profile", "Owner")
                        .WithMany("Posts")
                        .HasForeignKey("OwnerID");
                });

            modelBuilder.Entity("ImageHUB.Repositories.ProfileFriend", b =>
                {
                    b.HasOne("ImageHUB.Repositories.Profile", "Friend")
                        .WithMany("FriendsWith")
                        .HasForeignKey("FriendID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ImageHUB.Repositories.Profile", "Profile")
                        .WithMany("FriendsTo")
                        .HasForeignKey("ProfileID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
