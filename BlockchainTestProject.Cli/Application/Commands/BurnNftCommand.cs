using BlockchainTestProject.Application.Models;
using BlockchainTestProject.Persistence;
using LanguageExt.UnsafeValueAccess;
using MediatR;
using OneOf;

namespace BlockchainTestProject.Application.Commands;

public class BurnNftCommand
{
    public record Request(BurnNftCommandModel.Request Command) : IRequest<OneOf<bool, string>>;
}

public class BurnNftCommandHandler : IRequestHandler<BurnNftCommand.Request, OneOf<bool, string>>
{
    private readonly WalletRepository _walletRepository;

    public BurnNftCommandHandler(WalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task<OneOf<bool, string>> Handle(BurnNftCommand.Request request, CancellationToken cancellationToken)
    {
        var walletOption = await _walletRepository.GetNftAsync(request.Command.TokenId);
        if (walletOption.IsNone)
        {
            return "Wallet not found";
        }
        
        var wallet = walletOption.ValueUnsafe();
        var (success, error) = wallet.RemoveNft(request.Command.TokenId);
        if (!success)
        {
            return error; // TODO: return error type not string
        }

        await _walletRepository.SaveWalletAggregateAsync(wallet);
        return true;
    }
}