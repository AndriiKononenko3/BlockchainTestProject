using BlockchainTestProject.Application.Models;
using BlockchainTestProject.Persistence;
using MediatR;
using OneOf;

namespace BlockchainTestProject.Application.Commands;

public class TransferNftCommand
{
    public record Request(TransferNftCommandModel.Request Command) : IRequest<OneOf<bool, IEnumerable<string>>>;
}

public class TransferNftCommandHandler : IRequestHandler<TransferNftCommand.Request, OneOf<bool, IEnumerable<string>>>
{
    private readonly WalletRepository _walletRepository;

    public TransferNftCommandHandler(WalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task<OneOf<bool, IEnumerable<string>>> Handle(TransferNftCommand.Request request, CancellationToken cancellationToken)
    {
        var fromWallet = await _walletRepository.GetWalletAggregateAsync(request.Command.From);
        var toWallet = await _walletRepository.GetWalletAggregateAsync(request.Command.To);
        var (successFromWallet, errorFromWallet) = fromWallet.RemoveNft(request.Command.TokenId);
        var (successToWallet, errorToWallet) = toWallet.AddNft(request.Command.TokenId);
        
        if (!successFromWallet || !successToWallet)
        {
            return new[] { errorFromWallet, errorToWallet }!; // TODO: return error type not string
        }

        // TODO wrap in transaction
        await _walletRepository.SaveWalletAggregateAsync(fromWallet);
        await _walletRepository.SaveWalletAggregateAsync(toWallet);
        return true;

    }
}