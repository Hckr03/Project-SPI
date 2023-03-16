using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankAPI.Models;

public class Client
{
    public Client()
    {
        this.accounts = new HashSet<Account>();
        this.transfers = new HashSet<Transfer>();
    }

    [Key]
    public string docNumber {get; set;}
    public string? docType {get; set;}
    public string? name { get; set; }

    [JsonIgnore]
    public virtual ICollection<Account?> accounts { get; set; }
    [JsonIgnore]
    public virtual ICollection<Transfer?> transfers { get; set; }
}