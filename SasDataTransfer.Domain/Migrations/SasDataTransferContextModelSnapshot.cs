﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SasDataTransfer.Domain;

#nullable disable

namespace SasDataTransfer.Domain.Migrations
{
    [DbContext(typeof(SasDataTransferContext))]
    partial class SasDataTransferContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SasDataTransfer.Domain.Models.Analysis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AnalysisName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProtocolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnalysisName")
                        .IsUnique();

                    b.HasIndex("ProtocolId");

                    b.ToTable("Analysis");
                });

            modelBuilder.Entity("SasDataTransfer.Domain.Models.Protocol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ProtocolName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ProtocolName")
                        .IsUnique();

                    b.ToTable("Protocol");
                });

            modelBuilder.Entity("SasDataTransfer.Domain.Models.SasDataSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DataSetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransferId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransferId");

                    b.ToTable("SasDataSet");
                });

            modelBuilder.Entity("SasDataTransfer.Domain.Models.Transfer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AnalysisId")
                        .HasColumnType("int");

                    b.Property<string>("InputLoc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsTransferSuccessful")
                        .HasColumnType("bit");

                    b.Property<string>("OutputLoc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SjmJobName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserAccount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnalysisId");

                    b.ToTable("Transfer");
                });

            modelBuilder.Entity("SasDataTransfer.Domain.Models.Variable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DatasetId")
                        .HasColumnType("int");

                    b.Property<string>("VariableName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DatasetId");

                    b.ToTable("Variable");
                });

            modelBuilder.Entity("SasDataTransfer.Domain.Models.Analysis", b =>
                {
                    b.HasOne("SasDataTransfer.Domain.Models.Protocol", "Protocol")
                        .WithMany("Analyses")
                        .HasForeignKey("ProtocolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Protocol");
                });

            modelBuilder.Entity("SasDataTransfer.Domain.Models.SasDataSet", b =>
                {
                    b.HasOne("SasDataTransfer.Domain.Models.Transfer", "Transfer")
                        .WithMany("SasDataSets")
                        .HasForeignKey("TransferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transfer");
                });

            modelBuilder.Entity("SasDataTransfer.Domain.Models.Transfer", b =>
                {
                    b.HasOne("SasDataTransfer.Domain.Models.Analysis", "Analysis")
                        .WithMany("Transfers")
                        .HasForeignKey("AnalysisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Analysis");
                });

            modelBuilder.Entity("SasDataTransfer.Domain.Models.Variable", b =>
                {
                    b.HasOne("SasDataTransfer.Domain.Models.SasDataSet", "DataSet")
                        .WithMany("Variables")
                        .HasForeignKey("DatasetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DataSet");
                });

            modelBuilder.Entity("SasDataTransfer.Domain.Models.Analysis", b =>
                {
                    b.Navigation("Transfers");
                });

            modelBuilder.Entity("SasDataTransfer.Domain.Models.Protocol", b =>
                {
                    b.Navigation("Analyses");
                });

            modelBuilder.Entity("SasDataTransfer.Domain.Models.SasDataSet", b =>
                {
                    b.Navigation("Variables");
                });

            modelBuilder.Entity("SasDataTransfer.Domain.Models.Transfer", b =>
                {
                    b.Navigation("SasDataSets");
                });
#pragma warning restore 612, 618
        }
    }
}