using Common.Kernel.CQRS.Commands;
using Mapster;
using Promotion.Grpc.Domain;
using Promotion.Grpc.Persistance.Interfaces;
using Promotion.Grpc.Protos;

namespace Promotion.Grpc.UseCases.DeletePromo;

public class DeletePromoCommandHandler(IPromoRepository promoRepository)
    : ICommandHandler<DeletePromoCommand, DeletePromoResponse>
{
    public async Task<DeletePromoResponse> Handle(DeletePromoCommand command, CancellationToken ct)
    {
        var promo = command.Promo.Adapt<Promo>();
        var result = await promoRepository.DeleteByCatalogItemIdAsync(promo, ct);

        return new DeletePromoResponse()
        {
            Success = result,
            Description = result ? "Промоакция успешно удалена" : "Ошибка удаления промоакции"
        };
    }
}
