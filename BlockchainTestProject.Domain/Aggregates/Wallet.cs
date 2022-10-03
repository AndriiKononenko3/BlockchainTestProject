using BlockchainTestProject.Domain.DomainEvents;
using BlockchainTestProject.Domain.Infrastructure.EventSourcing;

namespace BlockchainTestProject.Domain.Aggregates;

public class Wallet : EventSourced
{
    public Wallet(string addressId, IEnumerable<IEvent> history)
        : this()
    {
        AddressId = addressId;
        LoadFrom(history);
    }

    protected Wallet()
        : base(Guid.NewGuid())
    {
        Handles<NftAddedEvent>(OnNftItemAdded);
        Handles<NftRemovedEvent>(OnNftItemRemoved);
    }
    
    public string AddressId { get; private set; } // TODO create wrapper around string AddressId
    public List<Nft> Nfts { get; private set; } = new();

    public (bool success, string? error) AddNft(string tokenId)
    {
        if (string.IsNullOrEmpty(tokenId))
        {
            return (false, "TokenId is empty");
        }
        
        if (Nfts.Any(nft => nft.TokenId == tokenId))
        {
            return (false, "Nft already exists");
        }
        
        Update(new NftAddedEvent(AddressId, new Nft(tokenId, AddressId)));
        return (true, null);
    }
    
    public (bool success, string? error) RemoveNft(string tokenId)
    {
        if (string.IsNullOrEmpty(tokenId))
        {
            return (false, "TokenId is empty");
        }
        
        if (Nfts.Any(nft => nft.TokenId == tokenId) == false)
        {
            return (true, null);
        }
        
        Update(new NftRemovedEvent(AddressId, tokenId));
        return (true, null);
    }
    
    private void OnNftItemAdded(NftAddedEvent itemAdded)
    {
        Nfts.Add(itemAdded.Nft);
    }
    
    private void OnNftItemRemoved(NftRemovedEvent itemRemoved)
    {
        Nfts.RemoveAll(x => x.TokenId == itemRemoved.TokenId);
    }
}