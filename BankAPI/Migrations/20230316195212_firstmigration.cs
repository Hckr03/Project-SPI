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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    bankCode = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    adress = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    docNumber = table.Column<string>(type: "text", nullable: false),
                    docType = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.docNumber);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    id_account = table.Column<Guid>(type: "uuid", nullable: false),
                    accountNum = table.Column<string>(type: "text", nullable: true),
                    currency = table.Column<string>(type: "text", nullable: true),
                    balance = table.Column<decimal>(type: "numeric", nullable: false),
                    docNumber = table.Column<string>(type: "text", nullable: true),
                    bankCode = table.Column<string>(type: "text", nullable: true),
                    clientdocNumber = table.Column<string>(type: "text", nullable: true),
                    bankid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.id_account);
                    table.ForeignKey(
                        name: "FK_Accounts_Banks_bankid",
                        column: x => x.bankid,
                        principalTable: "Banks",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Accounts_Clients_clientdocNumber",
                        column: x => x.clientdocNumber,
                        principalTable: "Clients",
                        principalColumn: "docNumber");
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    id_transaction = table.Column<Guid>(type: "uuid", nullable: false),
                    accountNum = table.Column<string>(type: "text", nullable: true),
                    docNumber = table.Column<string>(type: "text", nullable: true),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    state = table.Column<string>(type: "text", nullable: true),
                    accountid_account = table.Column<Guid>(type: "uuid", nullable: true),
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
                name: "IX_Accounts_bankid",
                table: "Accounts",
                column: "bankid");

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
