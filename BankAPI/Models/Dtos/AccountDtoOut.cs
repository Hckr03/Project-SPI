namespace BankAPI.Models.Dtos;

public class AccountDtoOut
{   
    public Guid Id {get; set;}
    public string AccountNum {get; set;}
    public string Currency { get; set; }
    public decimal Balance { get; set; }
    public virtual Client Client { get; set; }
    public virtual Bank Bank { get; set; }
    public ICollection<Transfer> Transfers { get; set; }
}