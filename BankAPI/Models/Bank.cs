using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankAPI.Models;

public class Bank
{   
    [Key]
    public Guid Id { get ; set;}
    public string BankCode {get; set;}
    public string Fullname {get; set;}
    public string Adress { get; set; }

    public ICollection<Account> Accounts {get; set;}

    public Bank( string bankCode, string fullname, string adress)
    {
        BankCode = bankCode;
        Fullname = fullname;
        Adress = adress;
    }

    public Bank(){}
}