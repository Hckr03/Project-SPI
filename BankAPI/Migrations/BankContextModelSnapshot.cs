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
    [DbContext(typeof(BankContext))]
    partial class BankContextModelSnapshot : ModelSnapshot
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
                    b.Property<string>("id_account")
                        .HasColumnType("text");

                    b.Property<string>("accountNum")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("balance")
                        .HasColumnType("numeric");

                    b.Property<string>("bankId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("clientdocNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("currency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("docNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id_account");

                    b.HasIndex("bankId");

                    b.HasIndex("clientdocNumber");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BankAPI.Models.Bank", b =>
                {
                    b.Property<string>("bankCode")
                        .HasColumnType("text");

                    b.Property<string>("adress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("bankCode");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("BankAPI.Models.Client", b =>
                {
                    b.Property<string>("docNumber")
                        .HasColumnType("text");

                    b.Property<string>("docType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("docNumber");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("BankAPI.Models.Transfer", b =>
                {
                    b.Property<string>("id_transaction")
                        .HasColumnType("text");

                    b.Property<string>("ClientdocNumber")
                        .HasColumnType("text");

                    b.Property<string>("accountNum")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("accountid_account")
                        .HasColumnType("text");

                    b.Property<decimal>("amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("docNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("state")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id_transaction");

                    b.HasIndex("ClientdocNumber");

                    b.HasIndex("accountid_account");

                    b.ToTable("Transfers");
                });

            modelBuilder.Entity("BankAPI.Models.Account", b =>
                {
                    b.HasOne("BankAPI.Models.Bank", "Bank")
                        .WithMany("Accounts")
                        .HasForeignKey("bankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BankAPI.Models.Client", "client")
                        .WithMany("Accounts")
                        .HasForeignKey("clientdocNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");

                    b.Navigation("client");
                });

            modelBuilder.Entity("BankAPI.Models.Transfer", b =>
                {
                    b.HasOne("BankAPI.Models.Client", null)
                        .WithMany("Transfers")
                        .HasForeignKey("ClientdocNumber");

                    b.HasOne("BankAPI.Models.Account", "account")
                        .WithMany("Transfers")
                        .HasForeignKey("accountid_account");

                    b.Navigation("account");
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
                    b.Navigation("Accounts");

                    b.Navigation("Transfers");
                });
#pragma warning restore 612, 618
        }
    }
}
