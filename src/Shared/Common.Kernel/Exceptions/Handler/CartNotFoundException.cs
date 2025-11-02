namespace Common.Kernel.Exceptions.Handler;

public  class CartNotFoundException : NotFoundException
{
    public CartNotFoundException(string accountName) : base("ShoppingCart", accountName)
    {
         
    }
}
