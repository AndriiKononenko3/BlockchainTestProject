using BlockchainTestProject.Domain.Aggregates;

namespace BlockchainTestProject.Domain.DomainEvents;

public class NftRemovedEvent : WalletEvent
{
    public NftRemovedEvent(
        string addressId,
        string tokenId)
        : base(addressId)
    {
        TokenId = tokenId;
    }

    public string TokenId { get; private set; }
}