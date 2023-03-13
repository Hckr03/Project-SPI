using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankAPI.Models;

public class Client
{
    public Client()
    {
        this.Accounts = new HashSet<Account>();
        this.Transfers = new HashSet<Transfer>();
    }

    [Key]
    public string? docNumber {get; set;}
    public string? docType {get; set;}
    public string? name { get; set; }

    [JsonIgnore]
    public virtual ICollection<Account> Accounts { get; set; }
    [JsonIgnore]
    public virtual ICollection<Transfer> Transfers { get; set; }
}