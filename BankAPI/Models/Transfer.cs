using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankAPI.Models;

public class Transfer
{
    [Key]
    public Guid? id_transaction {get; set;}

    //se supone son mi FK
    public string? accountNum {get; set;}
    public string? docNumber { get; set; }
    //hasta aqui se define los FK

    public DateTime date { get; set; }
    public decimal amount { get; set; }
    public string? state { get; set; }

    //relacion entre entity
    [JsonIgnore]
    public virtual Account? account { get; set; }
    [JsonIgnore]
    public virtual Client? client { get; set; }
}