using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAPI.Migrations
{
    /// <inheritdoc />
    public partial class firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    bankCode = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    adress = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.bankCode);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    docNumber = table.Column<string>(type: "text", nullable: false),
                    docType = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.docNumber);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    id_account = table.Column<string>(type: "text", nullable: false),
                    accountNum = table.Column<string>(type: "text", nullable: false),
                    currency = table.Column<string>(type: "text", nullable: false),
                    balance = table.Column<decimal>(type: "numeric", nullable: false),
                    docNumber = table.Column<string>(type: "text", nullable: false),
                    bankId = table.Column<string>(type: "text", nullable: false),
                    clientdocNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.id_account);
                    table.ForeignKey(
                        name: "FK_Accounts_Banks_bankId",
                        column: x => x.bankId,
                        principalTable: "Banks",
                        principalColumn: "bankCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Clients_clientdocNumber",
                        column: x => x.clientdocNumber,
                        principalTable: "Clients",
                        principalColumn: "docNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    id_transaction = table.Column<string>(type: "text", nullable: false),
                    accountNum = table.Column<string>(type: "text", nullable: false),
                    docNumber = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    state = table.Column<string>(type: "text", nullable: false),
                    accountid_account = table.Column<string>(type: "text", nullable: true),
                    clientdocNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.id_transaction);
                    table.ForeignKey(
                        name: "FK_Transfers_Accounts_accountid_account",
                        column: x => x.accountid_account,
                        principalTable: "Accounts",
                        principalColumn: "id_account");
                    table.ForeignKey(
                        name: "FK_Transfers_Clients_clientdocNumber",
                        column: x => x.clientdocNumber,
                        principalTable: "Clients",
                        principalColumn: "docNumber");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_bankId",
                table: "Accounts",
                column: "bankId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_clientdocNumber",
                table: "Accounts",
                column: "clientdocNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_accountid_account",
                table: "Transfers",
                column: "accountid_account");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_clientdocNumber",
                table: "Transfers",
                column: "clientdocNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
