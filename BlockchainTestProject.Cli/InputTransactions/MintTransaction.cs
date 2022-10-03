using System.Text.Json.Serialization;

namespace BlockchainTestProject.InputTransactions;

public class MintTransaction : BaseTransaction
{
    [JsonPropertyName("Address")]
    public string Address { get; set; }
}