using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankAPI.Models;

public class Transfer
{
    [Key]
    public Guid Id { get; set; }
    //se supone son mi FK
    public Account FromAccount { get; set; } = new Account();
    public Client FromClient { get; set; } = new Client();
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public Account ToAccount { get; set;} = new Account();
    public Client ToClient { get; set; } = new Client();
    public string State { get; set; } = string.Empty;

    public Transfer(Account fromAccountNum, 
    Client fromClientDocNumber, 
    Bank fromBank, 
    decimal amount, 
    Account toAccountNum, 
    Client toClientDocNumber, 
    Bank toBank, 
    string state,
    Account account,
    Client client)
    {
        FromAccount = fromAccountNum;
        FromClient = fromClientDocNumber;
        Amount = amount;
        ToAccount = toAccountNum;
        ToClient = toClientDocNumber;
        State = state;
    }

    public Transfer()
    {
        
    }
}