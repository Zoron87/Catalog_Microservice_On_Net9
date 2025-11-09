using Common.Kernel.CQRS.Commands;
using Promotion.Grpc.Domain;
using Promotion.Grpc.Persistance.Interfaces;
using Promotion.Grpc.Protos;

namespace Promotion.Grpc.UseCases.UpdatePromo;

public class UpdatePromoCommandHandler(IPromoRepository promoRepository) : 
    ICommandHandler<UpdatePromoCommand, UpdatePromoResponse>
{
    public async Task<UpdatePromoResponse> Handle(UpdatePromoCommand command, CancellationToken ct)
    {
        var promo = new Promo()
        {
            Id = Guid.Parse(command.Promo.Id),
            Title = command.Promo.Title,
            Value = (decimal)command.Promo.Value
        };

        var result = await promoRepository.UpdateAsync(promo, ct);

        return new UpdatePromoResponse()
        {
            Success = result,
            Description = result ? "Промоакция успешно обновлена" : "Ошибка обновления промоакции"
        };

    }
}
