namespace BlockchainTestProject.Application.Models;

public record TransferNftCommandModel
{
    public record Request(
        string From,
        string To,
        string TokenId);
}