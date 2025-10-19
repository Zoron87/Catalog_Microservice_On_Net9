using Basket.API.Models;
using Common.Kernel.CQRS.Commands;

namespace Basket.API.ShoppingBasket.Save;

public record SaveCartCommand(ShoppingCart Cart) : ICommand<SaveCartResult>;