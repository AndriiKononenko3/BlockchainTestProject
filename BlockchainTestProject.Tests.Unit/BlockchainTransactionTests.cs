using BlockchainTestProject.Application.Commands;
using BlockchainTestProject.Application.Models;
using BlockchainTestProject.Persistence;
using FluentAssertions;
using Xunit;

namespace BlockchainTestProject.Tests.Unit;

public class BlockchainTransactionTests
{
    private readonly WalletRepository _repository = new WalletRepository();
    
    [Fact]
    public async void MintNftCommand_ShouldReturnToken_WhenInputIsValid()
    {
        // Arrange
        var address1 = "0x1000000000000000000000000000000000000000";
        var tokenA = "0xA000000000000000000000000000000000000000";

        var command =  new MintNftCommand.Request(new MintCommandModel.Request(address1, tokenA));
        var handler = new MintNftCommandHandler(_repository);

        // Act
        var result = await handler.Handle(command, new CancellationToken());
        var output = await _repository.GetWalletAggregateAsync(address1);

        //Assert
        result.AsT0.Should().BeTrue();
        output.Nfts.Should()
            .HaveCount(1);
        output.Nfts.First()
            .TokenId.Should()
            .Be(tokenA);
    }
    
    [Fact]
    public async void BurnNftCommand_ShouldRemoveTokenFromTheWallet_WhenInputIsValid()
    {
        // Arrange
        var address1 = "0x1000000000000000000000000000000000000000";
        var tokenA = "0xA000000000000000000000000000000000000000";

        var command =  new MintNftCommand.Request(new MintCommandModel.Request(address1, tokenA));
        var handler = new MintNftCommandHandler(_repository);
        await handler.Handle(command, new CancellationToken());
        
        var burnCommand =  new BurnNftCommand.Request(new BurnNftCommandModel.Request(tokenA));
        var burnHandler = new BurnNftCommandHandler(_repository);

        // Act
        var result = await burnHandler.Handle(burnCommand, new CancellationToken());
        var output = await _repository.GetWalletAggregateAsync(address1);

        //Assert
        result.AsT0.Should().BeTrue();
        output.Nfts.Should()
            .HaveCount(0);
    }
    
    [Fact]
    public async void TransferNftCommand_ShouldMoveTokenBetweenTheWallets_WhenInputIsValid()
    {
        // Arrange
        var address1 = "0x1000000000000000000000000000000000000000";
        var address2 = "0x2000000000000000000000000000000000000000";
        var tokenA = "0xA000000000000000000000000000000000000000";

        var command =  new MintNftCommand.Request(new MintCommandModel.Request(address1, tokenA));
        var handler = new MintNftCommandHandler(_repository);
        await handler.Handle(command, new CancellationToken());
        
        var transferCommand =  new TransferNftCommand.Request(new TransferNftCommandModel.Request(address1, address2, tokenA));
        var transferHandler = new TransferNftCommandHandler(_repository);

        // Act
        var result = await transferHandler.Handle(transferCommand, new CancellationToken());
        var outputWallet1 = await _repository.GetWalletAggregateAsync(address1);
        var outputWallet2 = await _repository.GetWalletAggregateAsync(address2);

        //Assert
        result.AsT0.Should().BeTrue();
        outputWallet1.Nfts.Should()
            .HaveCount(0);
        outputWallet2.Nfts.Should()
            .HaveCount(1);
    }
}