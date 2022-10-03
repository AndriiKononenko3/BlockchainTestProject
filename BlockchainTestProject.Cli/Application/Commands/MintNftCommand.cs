using BlockchainTestProject.Application.Models;
using BlockchainTestProject.Persistence;
using MediatR;
using OneOf;

namespace BlockchainTestProject.Application.Commands;

public class MintNftCommand
{
    public record Request(MintCommandModel.Request Command) : IRequest<OneOf<bool, string>>;
}

public class MintNftCommandHandler : IRequestHandler<MintNftCommand.Request, OneOf<bool, string>>
{
    private readonly WalletRepository _walletRepository;

    public MintNftCommandHandler(WalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task<OneOf<bool, string>> Handle(MintNftCommand.Request request, CancellationToken cancellationToken)
    {
        var wallet = await _walletRepository.GetWalletAggregateAsync(request.Command.AddressId);
        var (success, error) = wallet.AddNft(request.Command.TokenId);
        if (success)
        {
            await _walletRepository.SaveWalletAggregateAsync(wallet);
            return true;
        }

        return error; // TODO: return error type not string
    }
}