using System.Text.Json.Serialization;

namespace BlockchainTestProject.InputTransactions;

public class TransferTransaction : BaseTransaction
{
    [JsonPropertyName("From")]
    public string From { get; set; }
    
    [JsonPropertyName("To")]
    public string To { get; set; }
}