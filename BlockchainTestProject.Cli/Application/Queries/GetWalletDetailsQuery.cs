using BlockchainTestProject.Application.Models;
using BlockchainTestProject.Persistence;
using MediatR;
using OneOf;
using OneOf.Types;

namespace BlockchainTestProject.Application.Queries;

public class GetWalletDetailsQuery
{
    public record Request(GetWalletDetailsQueryModel.Request Query) : IRequest<OneOf<GetWalletDetailsQueryModel.Response, None>>;
    
    public class GetWalletDetailsQueryHandler : IRequestHandler<Request, OneOf<GetWalletDetailsQueryModel.Response, None>>
    {
        private readonly WalletRepository _walletRepository;

        public GetWalletDetailsQueryHandler(WalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }
        
        public async Task<OneOf<GetWalletDetailsQueryModel.Response, None>> Handle(
            Request request,
            CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetWalletAggregateAsync(request.Query.AddressId);
            return new GetWalletDetailsQueryModel.Response(wallet.AddressId, wallet.Nfts.Select(x => x.TokenId));
        }
    }
}