﻿// <auto-generated />
using System;
using GTMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GTMS.Data.Migrations.Gtms
{
    [DbContext(typeof(GtmsContext))]
    partial class GtmsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GTMS.Models.Player", b =>
                {
                    b.Property<int>("uniqueID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("TeamuniqueID")
                        .HasColumnType("int");

                    b.Property<int>("age")
                        .HasColumnType("int");

                    b.Property<float>("height")
                        .HasColumnType("real");

                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("team")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("weight")
                        .HasColumnType("real");

                    b.HasKey("uniqueID");

                    b.HasIndex("TeamuniqueID");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("GTMS.Models.Team", b =>
                {
                    b.Property<int>("uniqueID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("entrenador")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("uniqueID");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("GTMS.Models.Player", b =>
                {
                    b.HasOne("GTMS.Models.Team", null)
                        .WithMany("Players")
                        .HasForeignKey("TeamuniqueID");
                });
#pragma warning restore 612, 618
        }
    }
}
