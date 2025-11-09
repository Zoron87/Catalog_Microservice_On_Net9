using Grpc.Core;
using MediatR;
using Promotion.Grpc.Protos;
using Promotion.Grpc.UseCases.GetPromo;

namespace Promotion.Grpc.Services;

public class PromoGrpcService (IMediator mediator) : PromoService.PromoServiceBase
{
    public override async Task<PromoModel> GetPromo (GetPromoRequest request, ServerCallContext context)
    {
        var query = new GetPromoByCatalogItemIdQuery(request.CatalogItemId);
        return await mediator.Send(query);
    }
}
