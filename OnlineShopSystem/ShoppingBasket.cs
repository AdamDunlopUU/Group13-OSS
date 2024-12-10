using System;
using System.Collections.Generic;
using System.Linq;
using OnlineShopSystem;


public class ShoppingBasket
{
    public int BasketID { get; }
    public Customer Customer { get; }
    public List<BasketItem> Items { get; } = new List<BasketItem>();

    public ShoppingBasket(int basketId, Customer customer)
    {
        BasketID = basketId;
        Customer = customer ?? throw new ArgumentNullException(nameof(customer));
    }

    public ShoppingBasket()
    {
    }

    public void AddProduct(Product product, int quantity)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        // Check if product already exists in basket
        var existingItem = Items.FirstOrDefault(item => item.Product.ProductID == product.ProductID);

        if (existingItem != null)
        {
            // Update quantity if product already in basket
            existingItem.Quantity += quantity;
        }
        else
        {
            // Add new basket item
            Items.Add(new BasketItem(product, quantity));
        }

        Console.WriteLine($"{quantity} x {product.Name} added to basket.");
    }

    public void RemoveProduct(int productId, int quantity = -1)
    {
        var basketItem = Items.FirstOrDefault(item => item.Product.ProductID == productId);

        if (basketItem == null)
        {
            Console.WriteLine("Product not found in basket.");
            return;
        }

        if (quantity == -1 || quantity >= basketItem.Quantity)
        {
            // Remove entire item
            Items.Remove(basketItem);
            Console.WriteLine($"{basketItem.Product.Name} removed from basket.");
        }
        else
        {
            // Reduce quantity
            basketItem.Quantity -= quantity;
            Console.WriteLine($"{quantity} x {basketItem.Product.Name} removed from basket.");
        }
    }

    public void ViewBasket()
    {
        if (Items.Count == 0)
        {
            Console.WriteLine("Your basket is empty.");
            return;
        }

        Console.WriteLine("Shopping Basket:");
        foreach (var item in Items)
        {
            Console.WriteLine(item.ToString());
        }
        Console.WriteLine($"Total Cost: {GetTotalCost():C}");
    }

    public decimal GetTotalCost()
    {
        return Items.Sum(item => item.SubTotal);
    }

    public void ClearBasket()
    {
        Items.Clear();
        Console.WriteLine("Basket has been cleared.");
    }

    public bool CheckoutBasket()
    {
        if (Items.Count == 0)
        {
            Console.WriteLine("Cannot checkout an empty basket.");
            return false;
        }

        // Validate stock availability
        foreach (var item in Items)
        {
            if (item.Quantity > item.Product.StockQuantity)
            {
                Console.WriteLine($"Insufficient stock for {item.Product.Name}. Available: {item.Product.StockQuantity}, Requested: {item.Quantity}");
                return false;
            }
        }

        // Reduce stock quantities
        foreach (var item in Items)
        {
            item.Product.StockQuantity -= item.Quantity;
        }

        Console.WriteLine("Checkout successful!");
        ClearBasket();
        return true;
    }

    internal void AddItem(Product selectedProduct, int quantity)
    {
        throw new NotImplementedException();
    }

    internal object GetTotalPrice()
    {
        throw new NotImplementedException();
    }

    internal void DisplayBasket()
    {
        throw new NotImplementedException();
    }
}
