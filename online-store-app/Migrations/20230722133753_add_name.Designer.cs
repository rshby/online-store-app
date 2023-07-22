﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using online_store_app.Data;

#nullable disable

namespace online_store_app.Migrations
{
    [DbContext(typeof(OnlineStoreContext))]
    [Migration("20230722133753_add_name")]
    partial class add_name
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("online_store_app.Models.Entity.Chart", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasColumnOrder(1);

                    b.Property<int?>("Amount")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("amount")
                        .HasColumnOrder(4);

                    b.Property<int?>("ProductId")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("product_id")
                        .HasColumnOrder(3);

                    b.Property<int?>("TotalPrice")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("total_price")
                        .HasColumnOrder(5);

                    b.Property<int?>("UserId")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("user_id")
                        .HasColumnOrder(2);

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("charts");
                });

            modelBuilder.Entity("online_store_app.Models.Entity.Product", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasColumnOrder(1);

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("brand")
                        .HasColumnOrder(2);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name")
                        .HasColumnOrder(3);

                    b.Property<int?>("Price")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("price")
                        .HasColumnOrder(4);

                    b.Property<int?>("Stock")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("stock")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.ToTable("products");
                });

            modelBuilder.Entity("online_store_app.Models.Entity.User", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasColumnOrder(1);

                    b.Property<string>("Address")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("address")
                        .HasColumnOrder(7);

                    b.Property<DateTime?>("BirthDate")
                        .IsRequired()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("birth_date")
                        .HasColumnOrder(4);

                    b.Property<string>("Gender")
                        .HasMaxLength(1)
                        .HasColumnType("varchar(1)")
                        .HasColumnName("gender")
                        .HasColumnOrder(5);

                    b.Property<string>("IdentityNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)")
                        .HasColumnName("identity_number")
                        .HasColumnOrder(2);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name")
                        .HasColumnOrder(3);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("phone_number")
                        .HasColumnOrder(6);

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("online_store_app.Models.Entity.Chart", b =>
                {
                    b.HasOne("online_store_app.Models.Entity.Product", "Product")
                        .WithMany("Charts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("online_store_app.Models.Entity.User", "User")
                        .WithMany("Charts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("online_store_app.Models.Entity.Product", b =>
                {
                    b.Navigation("Charts");
                });

            modelBuilder.Entity("online_store_app.Models.Entity.User", b =>
                {
                    b.Navigation("Charts");
                });
#pragma warning restore 612, 618
        }
    }
}