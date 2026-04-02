using System;
using System.Collections.Generic;

// 1. Product Model

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }

    public Product(int id, string name, string category, double price, int stock)
    {
        Id = id;
        Name = name;
        Category = category;
        Price = price;
        Stock = stock;
    }

    public override string ToString() => $"{Name} - {Price:C} (Stock: {Stock})";

    public void Print() => Console.WriteLine(this);
}

class Program
{
    // Task 01 : Smart Product Search
    static List<Product> SearchProducts(List<Product> products, Func<Product, bool> filter)
    {
        List<Product> result = new List<Product>();
        foreach (Product p in products)
            if (filter(p))
                result.Add(p);
        return result;
    }

    // Task 03 : Custom Report Generator
    // 3.1  Print Reports
    static void PrintReport(List<Product> products, Action<Product> printAction)
    {
        foreach (Product p in products)
            printAction(p);
    }

    // 3.2 – Transform Products
    static List<string> TransformProducts(List<Product> products, Func<Product, string> transform)
    {
        List<string> result = new List<string>();
        foreach (Product p in products)
            result.Add(transform(p));
        return result;
    }

    // 3.3 – Filter Products
    static List<Product> FilterProducts(List<Product> products, Predicate<Product> condition)
    {
        List<Product> result = new List<Product>();
        foreach (Product p in products)
            if (condition(p))
                result.Add(p);
        return result;
    }

    static void Main()
    {
        // 2. Product Catalog
        List<Product> catalog = new()
        {
            new Product(1, "Laptop", "Electronics", 1200, 10),
            new Product(2, "Phone", "Electronics", 800, 25),
            new Product(3, "T-Shirt", "Clothing", 30, 100),
            new Product(4, "Jeans", "Clothing", 60, 50),
            new Product(5, "Chocolate", "Food", 5, 200),
            new Product(6, "Coffee Beans", "Food", 15, 80),
            new Product(7, "C# Book", "Books", 45, 30),
            new Product(8, "Novel", "Books", 20, 60),
            new Product(9, "Headphones", "Electronics", 150, 40),
            new Product(10, "Jacket", "Clothing", 120, 15),
        };

        #region Task 1
        Console.WriteLine("--- Electronics ---");
        var electronics = SearchProducts(catalog, p => p.Category == "Electronics");
        foreach (Product p in electronics)
            p.Print();

        Console.WriteLine("\n--- Under $50 ---");
        var cheap = SearchProducts(catalog, p => p.Price < 50);
        foreach (Product p in cheap)
            p.Print();

        Console.WriteLine("\n--- In Stock ---");
        var inStock = SearchProducts(catalog, p => p.Stock > 0);
        foreach (Product p in inStock)
            p.Print();

        Console.WriteLine("\n--- Clothing under $100 ---");
        var clothingUnder100 = SearchProducts(
            catalog,
            p => p.Category == "Clothing" && p.Price < 100
        );
        foreach (Product p in clothingUnder100)
            p.Print();

        #endregion


        #region Task 3.1
        Console.WriteLine("\n--- Short Report ---");
        PrintReport(catalog, p => Console.WriteLine($"{p.Name} - ${p.Price}"));

        Console.WriteLine("\n--- Detailed Report ---");
        PrintReport(
            catalog,
            p =>
                Console.WriteLine($"[{p.Category}] {p.Name} | Price: ${p.Price} | Stock: {p.Stock}")
        );
        #endregion

        #region Task 3.2
        Console.WriteLine("\n--- Summary List ---");
        var summaries = TransformProducts(catalog, p => $"{p.Name} (${p.Price})");
        foreach (var s in summaries)
            Console.WriteLine(s);

        Console.WriteLine("\n--- Price Labels ---");
        var labels = TransformProducts(catalog, p => p.Price > 100 ? "Expensive!" : "Affordable");
        for (int i = 0; i < catalog.Count; i++)
            Console.WriteLine($"{catalog[i].Name}: {labels[i]}");
        #endregion

        #region Task 3.3
        Console.WriteLine("\n--- Low Stock Alert ---");
        var lowStock = FilterProducts(catalog, p => p.Stock < 20);
        foreach (var p in lowStock)
            Console.WriteLine($"[LOW STOCK] {p.Name}: only {p.Stock} left!");
        #endregion
    }
}
