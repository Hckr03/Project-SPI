using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankAPI.Models;

public class Account
{
    [Key]
    public Guid Id {get; set;}
    public string AccountNum { get; set; }  = string.Empty;
    public string Currency { get; set; }  = string.Empty;
    public decimal Balance { get; set; }
    //relaciones entre entities
    public Client Client { get; set; } = new Client();
    public Bank Bank { get; set; } = new Bank();

    public Account(string accounNum,
        string currency,
        decimal balance,
        Client client,
        Bank bank)
    {
        AccountNum = accounNum;
        Currency = currency;
        Balance = balance;
        Client = client;
        Bank = bank;
    }

    public Account(){}
}