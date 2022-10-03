using BlockchainTestProject.Application.Models;
using BlockchainTestProject.Persistence;
using LanguageExt.UnsafeValueAccess;
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

            if (walletOption.IsNone)
            {
                return new None();
            }
            
            return new GetNftDetailsQueryModel.Response(walletOption.ValueUnsafe().AddressId, request.Query.TokenId);
        }
    }
}