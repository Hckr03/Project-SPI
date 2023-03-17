using System.Text.Json.Serialization;

namespace BankAPI.Models.Dtos;

public class BankDtoIn
{   
    [JsonIgnore]
    public Guid Id { get ; set;}
    public string BankCode {get; set;}
    public string Fullname {get; set;}
    public string Adress { get; set; }
}