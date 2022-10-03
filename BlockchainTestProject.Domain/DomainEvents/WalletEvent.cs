using System.Text.Json.Serialization;
using BlockchainTestProject.Domain.Infrastructure.EventSourcing;

namespace BlockchainTestProject.Domain.DomainEvents;

public class WalletEvent : Event
{
    public WalletEvent(string addressId)
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow; // TODO : Use a clock
        AddressId = addressId;
    }

    [JsonConstructor]
    public WalletEvent(
        Guid id,
        string addressId,
        DateTime createDate)
    {
        Id = id;
        AddressId = addressId;
        CreationDate = createDate;
    }
    
    public Guid Id { get; private set; }
    
    public string AddressId { get; private set; }
    
    public DateTime CreationDate { get; private set; }
}