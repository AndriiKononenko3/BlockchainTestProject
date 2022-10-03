using BlockchainTestProject.Domain.Aggregates;

namespace BlockchainTestProject.Domain.DomainEvents;

public class NftAddedEvent : WalletEvent
{
    public NftAddedEvent(
        string addressId,
        Nft nft)
        : base(addressId)
    {
        Nft = nft;
    }

    public Nft Nft { get; private set; }
}