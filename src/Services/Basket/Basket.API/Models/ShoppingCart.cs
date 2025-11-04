namespace Basket.API.Models;

public class ShoppingCart
{
    public string AccountName { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(u => u.Quantity * u.UnitPrice);

    public ShoppingCart() { }

    public ShoppingCart(string accountName)
    {
        AccountName = accountName;
    }
}
