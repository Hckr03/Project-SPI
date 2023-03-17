using System.ComponentModel.DataAnnotations;

namespace BankAPI.Models;

public class Client
{
    [Key]
    public string DocNumber { get; set; }  = string.Empty;
    public string DocType { get; set; }  = string.Empty;
    public string Fullname { get; set; }  = string.Empty;
}