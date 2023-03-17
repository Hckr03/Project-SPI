using System.Text.Json.Serialization;

namespace BankAPI.Models.Dtos;

public class AccountDtoIn
{
    [JsonIgnore]
    public Guid Id {get; set;}
    public string AccountNum {get; set;}
    public string Currency { get; set; }
    public decimal Balance { get; set; }
    //se supone son mi FK
    public string ClientDocNumber { get; set; }
    public Guid BankId { get; set; }
}
