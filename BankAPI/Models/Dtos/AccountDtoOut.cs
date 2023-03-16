namespace BankAPI.Models.Dtos;

public class AccountDtoOut
{
    public Guid id_account {get; set;}
    public string? accountNum {get; set;}
    public string? currency { get; set; }
    public decimal balance { get; set; }
    public virtual Client client { get; set; }
    public virtual Bank bank { get; set; }
}