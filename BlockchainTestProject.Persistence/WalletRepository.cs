using BlockchainTestProject.Domain.Aggregates;
using BlockchainTestProject.Domain.DomainEvents;
using LanguageExt;

namespace BlockchainTestProject.Persistence;

public class WalletRepository
{
    private readonly List<WalletEvent> _events;

    public WalletRepository()
    {
        _events = new List<WalletEvent>();
    }

    public async Task<Wallet> GetWalletAggregateAsync(string addressId)
    {
        var events = _events
            .Where(x => x.AddressId.Equals(addressId, StringComparison.OrdinalIgnoreCase))
            .ToArray();

        return new Wallet(addressId, events);
    }
    
    public async Task<Option<Wallet>> GetNftAsync(string tokenId)
    {
        var nft = _events
            .OfType<NftAddedEvent>()
            .Where(x => x.Nft.TokenId.Equals(tokenId, StringComparison.OrdinalIgnoreCase))
            .MaxBy(x => x.CreationDate)
            ?.Nft;

        if (nft is null)
        {
            return Option<Wallet>.None;
        }

        var wallet = await GetWalletAggregateAsync(nft.AddressId);
        return Option<Wallet>.Some(wallet);
    }
    
    public Task SaveWalletAggregateAsync(Wallet aggregate)
    {
        var eventsToSave = aggregate.Events.OfType<WalletEvent>();

        if (!eventsToSave.Any())
        {
            return Task.CompletedTask;
        }

        _events.AddRange(eventsToSave);
        return Task.CompletedTask;
    }
}