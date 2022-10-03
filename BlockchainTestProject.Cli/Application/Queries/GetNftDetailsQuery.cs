using BlockchainTestProject.Application.Models;
using BlockchainTestProject.Persistence;
using MediatR;
using OneOf;
using OneOf.Types;

public class GetNftDetailsQuery
{
    public record Request(GetNftDetailsQueryModel.Request Query) : IRequest<OneOf<GetNftDetailsQueryModel.Response, None>>;
    
    public class GetNftDetailsQueryHandler : IRequestHandler<Request, OneOf<GetNftDetailsQueryModel.Response, None>>
    {
        private readonly WalletRepository _walletRepository;

        public GetNftDetailsQueryHandler(WalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }
        
        public async Task<OneOf<GetNftDetailsQueryModel.Response, None>> Handle(
            Request request,
            CancellationToken cancellationToken)
        {
            var walletOption = await _walletRepository.GetNftAsync(request.Query.TokenId);
            
            return walletOption.Match<OneOf<GetNftDetailsQueryModel.Response, None>>(
                wallet => new GetNftDetailsQueryModel.Response(wallet.AddressId, request.Query.TokenId),
                () => new None());
        }
    }
}
