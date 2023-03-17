using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankAPI.Models;

public class Client
{
    [Key]
    public string DocNumber {get; set;}
    public string DocType {get; set;}
    public string Fullname { get; set; }

    public ICollection<Account> Accounts { get; set; }
    public ICollection<Transfer> Transfers { get; set; }

    public Client(string docNumber, string docType, string fullname)
    {
        DocNumber = docNumber;
        DocType = docType;
        Fullname = fullname;
    }
}