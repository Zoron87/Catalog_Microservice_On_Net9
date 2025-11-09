using Common.Kernel.CQRS.Commands;
using Mapster;
using Promotion.Grpc.Domain;
using Promotion.Grpc.Persistance.Interfaces;
using Promotion.Grpc.Protos;

namespace Promotion.Grpc.UseCases.CreatePromo;

public class CreatePromoCommandHandler(IPromoRepository repository) : 
    ICommandHandler<CreatePromoCommand, CreatePromoResponse>
{
    public async Task<CreatePromoResponse> Handle(CreatePromoCommand command, CancellationToken ct)
    {
        var promo = command.Promo.Adapt<Promo>();

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
