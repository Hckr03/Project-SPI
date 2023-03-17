using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankAPI.Models;

public class Account
{
    [Key]
    public Guid Id {get; set;}
    public string AccountNum {get; set;}
    public string Currency { get; set; }
    public decimal Balance { get; set; }
    
    //se supone son mi FK
    public string ClientDocNumber { get; set; }
    public Guid? BankId { get; set; }
    //hasta aqui se define los FK

    //relaciones entre entities
    public Client Client { get; set; }
    public Bank Bank { get; set; }
    public ICollection<Transfer> Transfers { get; set; }

    public Account(string accountNumber, string currency, decimal balance, string clientDocNumber, Guid bankId)
    {
        AccountNum = accountNumber;
        Currency = currency;
        Balance = balance;
        ClientDocNumber = clientDocNumber;
        BankId = bankId;
    }

    public Account(){}
}