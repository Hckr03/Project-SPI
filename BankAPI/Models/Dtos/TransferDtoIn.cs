using System.Text.Json.Serialization;

namespace BankAPI.Models.Dtos;

public class TransferDtoIn
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string FromAccountNum { get; set; }
    public string FromClientDocNumber { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string ToAccountNum { get; set;}
    public string ToClientDocNumber { get; set; }
    public string State { get; set; }
}