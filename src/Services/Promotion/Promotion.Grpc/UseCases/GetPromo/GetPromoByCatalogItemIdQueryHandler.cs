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
            return new PromoModel()
            {
                CatalogItemId = query.CatalogItemId,
                Value = 0
            };
        }

        return promo.Adapt<PromoModel>();
    }
}
