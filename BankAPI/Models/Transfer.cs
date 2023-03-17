using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankAPI.Models;

public class Transfer
{
    [Key]
    public Guid Id {get; set;}

    //se supone son mi FK
    public string AccountNum {get; set;}
    public string ClientDocNumber { get; set; }
    //hasta aqui se define los FK

    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string State { get; set; }

    //relacion entre entity
    [JsonIgnore]
    public virtual Account Account { get; set; }
    [JsonIgnore]
    public virtual Client Client { get; set; }

    public Transfer(string accountNumber, string clientDocNumber, DateTime date, decimal amount, string state)
    {
        AccountNum = accountNumber;
        ClientDocNumber = clientDocNumber;
        Date = date;
        Amount = amount;
        State = state;
    }

    public Transfer(){}
}