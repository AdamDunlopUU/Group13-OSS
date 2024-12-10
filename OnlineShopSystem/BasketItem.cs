using System;
using OnlineShopSystem;


public class BasketItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal SubTotal => Product.Price * Quantity;

    public BasketItem(Product product, int quantity)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
        Quantity = quantity > 0 ? quantity : throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
    }

    public override string ToString()
    {
        return $"{Product.Name} - Quantity: {Quantity}, Subtotal: {SubTotal:C}";
    }
}
