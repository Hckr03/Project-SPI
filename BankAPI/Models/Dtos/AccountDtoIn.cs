using System.Text.Json.Serialization;

namespace BankAPI.Models.Dtos;

public class AccountDtoIn
{
    [JsonIgnore]
    public Guid id_account {get; set;}
    public string? accountNum {get; set;}
    public string? currency { get; set; }
    public decimal balance { get; set; }
    //se supone son mi FK
    public string? docNumber { get; set; }
    public string? bankCode { get; set; }
}