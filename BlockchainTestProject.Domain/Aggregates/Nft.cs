namespace BlockchainTestProject.Domain.Aggregates;

public class Nft
{
    public Nft(string tokenId, string addressId)
    {
        if (string.IsNullOrWhiteSpace(tokenId))
        {
            throw new ArgumentNullException(nameof(tokenId));
        }
        if (string.IsNullOrWhiteSpace(addressId))
        {
            throw new ArgumentNullException(nameof(addressId));
        }
        
        TokenId = tokenId;
        AddressId = addressId;
    }

    public string TokenId { get; private set; }   // TODO create wrapper around string TokenId
    public string AddressId { get; private set; } // TODO create wrapper around string AddressId
}