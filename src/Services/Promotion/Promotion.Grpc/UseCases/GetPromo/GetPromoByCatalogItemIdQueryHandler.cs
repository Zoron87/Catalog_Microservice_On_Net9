using Common.Kernel.CQRS.Queries;
using Grpc.Core;
using Mapster;
using Promotion.Grpc.Persistance.Interfaces;
using Promotion.Grpc.Protos;

namespace Promotion.Grpc.UseCases.GetPromo;

public class GetPromoByCatalogItemIdQueryHandler(IPromoRepository repository)
    : IQueryHandler<GetPromoByCatalogItemIdQuery, PromoModel>
{
    public async Task<PromoModel> Handle(GetPromoByCatalogItemIdQuery query, CancellationToken ct)
    {
        var promo = await repository.GetByCatalogItemIdAsync(query.CatalogItemId, ct);

        if (promo is null)
        {
            throw new RpcException(
                new Status(StatusCode.NotFound,
                $"Ничего для {query.CatalogItemId} не найдено")
                );
        }

        return promo.Adapt<PromoModel>();
    }
}
