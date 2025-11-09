using Common.Kernel.CQRS.Commands;
using Promotion.Grpc.Domain;
using Promotion.Grpc.Persistance.Interfaces;
using Promotion.Grpc.Protos;

namespace Promotion.Grpc.UseCases.CreatePromo;

public class CreatePromoCommandHandler(IPromoRepository repository) : 
    ICommandHandler<CreatePromoCommand, CreatePromoResponse>
{
    public async Task<CreatePromoResponse> Handle(CreatePromoCommand command, CancellationToken ct)
    {
        var promo = new Promo
        {
            Id = Guid.NewGuid(),
            CatalogItemId = command.Promo.CatalogItemId,
            Title = command.Promo.Title,
            Value = (decimal)command.Promo.Value,
        };

        var success = await repository.CreateAsync(promo, ct);

        var result = new CreatePromoResponse
        {
            Id = promo.Id.ToString(),
            Success = success,
            Description = success ? "Промо акция успешно создана" : "Ошибка при создании промо-акции"
        };
        return result;
    }
}
