namespace BankAPI.Models.Dtos;

public class BankDtoOut
{   
    public Guid Id { get ; set;}
    public string Fullname {get; set;}
    public string Adress { get; set; }
    public String BankCode {get; set;}
    public ICollection<Account> Accounts {get; set;}
}