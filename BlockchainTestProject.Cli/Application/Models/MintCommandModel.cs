namespace BlockchainTestProject.Application.Models;

public record MintCommandModel
{
    public record Request(
        string AddressId,
        string TokenId);
}