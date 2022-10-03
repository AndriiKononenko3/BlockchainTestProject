using System.Text.Json.Serialization;

namespace BlockchainTestProject.InputTransactions;

public enum TransactionType
{
    Mint,
    Burn,
    Transfer
}

public class BaseTransaction
{
    [JsonPropertyName("Type")]
    public TransactionType Type { get; set; }
    
    [JsonPropertyName("TokenId")]
    public string TokenId { get; set; }
}