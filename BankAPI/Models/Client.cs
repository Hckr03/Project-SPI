using System.ComponentModel.DataAnnotations;

namespace BankAPI.Models;

public class Client
{
    public Client()
    {
        this.Accounts = new HashSet<Account>();
        this.Transfers = new HashSet<Transfer>();
    }

    [Key]
    public string docNumber {get; set;}
    public string docType {get; set;}
    public string name { get; set; }

    public virtual ICollection<Account> Accounts { get; set; }
    public virtual ICollection<Transfer> Transfers { get; set; }
}