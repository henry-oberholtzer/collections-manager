﻿// <auto-generated />
using System;
using CollectionsManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CollectionsManager.Migrations
{
    [DbContext(typeof(CollectionsManagerContext))]
    [Migration("20240207214923_AddTagsToEditForm")]
    partial class AddTagsToEditForm
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CollectionsManager.Models.Collection", b =>
                {
                    b.Property<int>("CollectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CollectionId");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("CollectionsManager.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CollectionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<double>("Value")
                        .HasColumnType("double");

                    b.HasKey("ItemId");

                    b.HasIndex("CollectionId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("CollectionsManager.Models.ItemTagJoinEntity", b =>
                {
                    b.Property<int>("ItemTagJoinEntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("ItemTagJoinEntityId");

                    b.HasIndex("ItemId");

                    b.HasIndex("TagId");

                    b.ToTable("ItemTagJoinEntities");
                });

            modelBuilder.Entity("CollectionsManager.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("CollectionsManager.Models.Item", b =>
                {
                    b.HasOne("CollectionsManager.Models.Collection", "Collection")
                        .WithMany("Items")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collection");
                });

            modelBuilder.Entity("CollectionsManager.Models.ItemTagJoinEntity", b =>
                {
                    b.HasOne("CollectionsManager.Models.Item", "Item")
                        .WithMany("ItemTagJoinEntities")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CollectionsManager.Models.Tag", "Tag")
                        .WithMany("ItemTagJoinEntities")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("CollectionsManager.Models.Collection", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("CollectionsManager.Models.Item", b =>
                {
                    b.Navigation("ItemTagJoinEntities");
                });

            modelBuilder.Entity("CollectionsManager.Models.Tag", b =>
                {
                    b.Navigation("ItemTagJoinEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
