namespace BlockchainTestProject.Application.Models;

public record BurnNftCommandModel
{
    public record Request(string TokenId);
}