﻿// <auto-generated />
using System;
using ConsoleApp.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConsoleApp.Migrations
{
    [DbContext(typeof(CombGuidDbContext))]
    [Migration("20191224165413_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ConsoleApp.Entities.TableWithExtendedUuidCreateSequential", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnotherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.HasIndex("AnotherId", "Value");

                    b.ToTable("TableWithExtendedUuidCreateSequential");
                });

            modelBuilder.Entity("ConsoleApp.Entities.TableWithNewSequentialIdAsDefault", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnotherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.HasIndex("AnotherId", "Value");

                    b.ToTable("TableWithNewSequentialIdAsDefault");
                });

            modelBuilder.Entity("ConsoleApp.Entities.TableWithRegularGuid", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnotherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.HasIndex("AnotherId", "Value");

                    b.ToTable("TableWithRegularGuid");
                });

            modelBuilder.Entity("ConsoleApp.Entities.TableWithSpanCustomGuidComb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnotherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.HasIndex("AnotherId", "Value");

                    b.ToTable("TableWithSpanCustomGuidComb");
                });
#pragma warning restore 612, 618
        }
    }
}
