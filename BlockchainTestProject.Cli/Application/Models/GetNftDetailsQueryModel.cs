namespace BlockchainTestProject.Application.Models;

public class GetNftDetailsQueryModel
{
    public record Request(string TokenId);
    
    public record Response(string AddressId, string TokenId);
}