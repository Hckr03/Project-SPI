using System.ComponentModel.DataAnnotations;

namespace BankAPI.Models;

public class Account
{
    public Account()
    {
        this.Transfers = new HashSet<Transfer>();
    }

    [Key]
    public string id_account {get; set;}
    public string accountNum {get; set;}
    public string currency { get; set; }
    public decimal balance { get; set; }
    
    //se supone son mi FK
    public string docNumber { get; set; }
    public string bankId { get; set; }
    //hasta aqui se define los FK

    //relaciones entre entities
    public virtual Client client { get; set; }
    public virtual Bank Bank { get; set; }
    public virtual ICollection<Transfer> Transfers { get; set; }
}