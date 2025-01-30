﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace TheEmployeeAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("Benefit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("BaseCost")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Benefits");
                });

            modelBuilder.Entity("Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address1")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address2")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("SocialSecurityNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .HasColumnType("TEXT");

                    b.Property<string>("ZipCode")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EmployeeBenefit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BenefitId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("CostToEmployee")
                        .HasColumnType("TEXT");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BenefitId");

                    b.HasIndex("EmployeeId", "BenefitId")
                        .IsUnique();

                    b.ToTable("EmployeeBenefits");
                });

            modelBuilder.Entity("EmployeeBenefit", b =>
                {
                    b.HasOne("Benefit", "Benefit")
                        .WithMany()
                        .HasForeignKey("BenefitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Employee", "Employee")
                        .WithMany("Benefits")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Benefit");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Employee", b =>
                {
                    b.Navigation("Benefits");
                });
#pragma warning restore 612, 618
        }
    }
}
