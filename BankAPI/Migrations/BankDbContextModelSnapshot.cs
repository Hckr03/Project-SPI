﻿// <auto-generated />
using System;
using BankAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BankAPI.Migrations
{
    [DbContext(typeof(BankDbContext))]
    partial class BankDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BankAPI.Models.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccountNum")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("BankId")
                        .HasColumnType("uuid");

                    b.Property<string>("ClientDocNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.HasIndex("ClientDocNumber");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BankAPI.Models.Bank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BankCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("BankAPI.Models.Client", b =>
                {
                    b.Property<string>("DocNumber")
                        .HasColumnType("text");

                    b.Property<string>("DocType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DocNumber");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("BankAPI.Models.Transfer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid");

                    b.Property<string>("AccountNum")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("ClientDocNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("ClientDocNumber");

                    b.ToTable("Transfers");
                });

            modelBuilder.Entity("BankAPI.Models.Account", b =>
                {
                    b.HasOne("BankAPI.Models.Bank", "Bank")
                        .WithMany("Accounts")
                        .HasForeignKey("BankId");

                    b.HasOne("BankAPI.Models.Client", "Client")
                        .WithMany("accounts")
                        .HasForeignKey("ClientDocNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("BankAPI.Models.Transfer", b =>
                {
                    b.HasOne("BankAPI.Models.Account", "Account")
                        .WithMany("Transfers")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BankAPI.Models.Client", "Client")
                        .WithMany("transfers")
                        .HasForeignKey("ClientDocNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("BankAPI.Models.Account", b =>
                {
                    b.Navigation("Transfers");
                });

            modelBuilder.Entity("BankAPI.Models.Bank", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("BankAPI.Models.Client", b =>
                {
                    b.Navigation("accounts");

                    b.Navigation("transfers");
                });
#pragma warning restore 612, 618
        }
    }
}
