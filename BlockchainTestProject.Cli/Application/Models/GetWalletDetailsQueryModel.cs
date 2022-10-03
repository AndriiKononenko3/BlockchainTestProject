namespace BlockchainTestProject.Application.Models;

public record GetWalletDetailsQueryModel
{
    public record Request(string AddressId);
    
    public record Response(string AddressId, IEnumerable<string> Tokens);
}