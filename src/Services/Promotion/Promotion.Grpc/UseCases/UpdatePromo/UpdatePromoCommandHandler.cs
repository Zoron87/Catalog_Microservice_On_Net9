using Common.Kernel.CQRS.Commands;
using Mapster;
using Promotion.Grpc.Domain;
using Promotion.Grpc.Persistance.Interfaces;
using Promotion.Grpc.Protos;

namespace Promotion.Grpc.UseCases.UpdatePromo;

public class UpdatePromoCommandHandler(IPromoRepository promoRepository) : 
    ICommandHandler<UpdatePromoCommand, UpdatePromoResponse>
{
    public async Task<UpdatePromoResponse> Handle(UpdatePromoCommand command, CancellationToken ct)
    {
        var promo = command.Promo.Adapt<Promo>();

        var result = await promoRepository.UpdateAsync(promo, ct);

        return new UpdatePromoResponse()
        {
            Success = result,
            Description = result ? "Промоакция успешно обновлена" : "Ошибка обновления промоакции"
        };

    }
}
