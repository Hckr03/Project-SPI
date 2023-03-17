namespace BankAPI.Models.Dtos;

public class ClientDtoOut
{
    public string DocNumber {get; set;}
    public string DocType {get; set;}
    public string Fullname { get; set; }
    public ICollection<Account> Accounts { get; set; }
    public ICollection<Transfer> Transfers { get; set; }
}