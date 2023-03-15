using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankAPI.Models;

public class Account
{
    public Account()
    {
        this.transfers = new HashSet<Transfer>();
    }

    [Key]
    public Guid? id_account {get; set;}
    public string? accountNum {get; set;}
    public string? currency { get; set; }
    public decimal balance { get; set; }
    
    //se supone son mi FK
    public string? docNumber { get; set; }
    public string? bankCode { get; set; }
    //hasta aqui se define los FK

    //relaciones entre entities
    [JsonIgnore]
    public virtual Client? client { get; set; }
    [JsonIgnore]
    public virtual Bank? bank { get; set; }
    [JsonIgnore]
    public virtual ICollection<Transfer> transfers { get; set; }
}