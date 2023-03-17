using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankAPI.Models;

public class Bank
{   
    [Key]
    public Guid Id { get ; set; }
    public string BankCode { get; set; }  = string.Empty;
    public string Fullname { get; set; }  = string.Empty;
    public string Adress { get; set; }  = string.Empty;

    public Bank(string bankCode, string fullname, string adress)
    {
        BankCode = bankCode;
        Fullname = fullname;
        Adress = adress;
    }

    public Bank(){}
}