﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using POS.DataContext;

#nullable disable

namespace POS.Migrations.Product
{
    [DbContext(typeof(ProductContext))]
    [Migration("20230731104345_four")]
    partial class four
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("POS.Model.Product", b =>
                {
                    b.Property<string>("ProductId")
                        .HasColumnType("TEXT");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("CategoryQ")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CusPhonenumber")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Discount")
                        .HasColumnType("REAL");

                    b.Property<double>("PricePerq")
                        .HasColumnType("REAL");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalProq")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
