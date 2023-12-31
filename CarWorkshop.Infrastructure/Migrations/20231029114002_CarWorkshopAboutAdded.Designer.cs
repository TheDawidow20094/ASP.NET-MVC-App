﻿// <auto-generated />
using System;
using CarWorkshop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarWorkshop.Infrastructure.Migrations
{
    [DbContext(typeof(CarWorkshopDbContext))]
    [Migration("20231029114002_CarWorkshopAboutAdded")]
    partial class CarWorkshopAboutAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("CarWorkshop.Domain.Entities.CarWorkshop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("About")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("EncodedName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CarWorkshops");
                });

            modelBuilder.Entity("CarWorkshop.Domain.Entities.CarWorkshop", b =>
                {
                    b.OwnsOne("CarWorkshop.Domain.Entities.CarWorkshopContactDetails", "ContactDetails", b1 =>
                        {
                            b1.Property<int>("CarWorkshopId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("City")
                                .HasColumnType("TEXT");

                            b1.Property<string>("PhoneNo")
                                .HasColumnType("TEXT");

                            b1.Property<string>("PostalCode")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Street")
                                .HasColumnType("TEXT");

                            b1.HasKey("CarWorkshopId");

                            b1.ToTable("CarWorkshops");

                            b1.WithOwner()
                                .HasForeignKey("CarWorkshopId");
                        });

                    b.Navigation("ContactDetails")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
