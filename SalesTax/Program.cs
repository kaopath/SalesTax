using System;
using System.Collections.Generic;

namespace SalesTax
{
    class Program
    {
        static Dictionary<short, List<Product>> scenarios = new Dictionary<short, List<Product>>() {

            {1,new List<Product> {
                new Product
                {
                     Name="Book",
                     Quantity=1,
                     Amount=12.49,
                     Type=Type.Book
                },
                   new Product
                {
                     Name="Music CD",
                     Quantity=1,
                     Amount=14.99,
                     Type=Type.Other
                }
                   ,  new Product
                {
                     Name="Chocolate Bar",
                     Quantity=1,
                     Amount=0.85,
                     Type=Type.Food
                }
            }},


            {2,new List<Product> {
                new Product
                {
                     Name="imported box of chocolates",
                     Quantity=1,
                     Amount=10.00,
                     Imported=true,
                     Type=Type.Food
                },
                   new Product
                {
                     Name="imported bottle of perfume",
                     Quantity=1,
                     Amount=47.50,
                       Imported=true,
                     Type=Type.Other
                }

            }},

            {3,new List<Product> {
                new Product
                {
                     Name="imported bottle of perfume",
                     Quantity=1,
                     Amount=27.99,
                     Imported=true,
                     Type=Type.Other
                },
                   new Product
                {
                     Name="bottle of perfume",
                     Quantity=1,
                     Amount=18.99,
                     Type=Type.Other
                }
                   ,
                   new Product
                {
                     Name="packet of headache pills",
                     Quantity=1,
                     Amount=9.75,
                     Type=Type.Medical
                }
                   ,
                   new Product
                {
                     Name="box of imported chocolates",
                     Quantity=1,
                     Amount=11.25,
                     Imported=true,
                     Type=Type.Food
                }

            }},

         };



        static void Main(string[] args)
        {

            CalculateTax();

            PrintReceipt();

            Console.Read();
        }


        static void CalculateTax()
        {
            foreach (var scenario in scenarios)
            {
                foreach (var product in scenario.Value)
                {
                    if (product.Imported)
                    {
                        product.Tax += Math.Ceiling((product.Amount * 0.05) * 20) / 20;
                    }

                    if (!(product.Type == Type.Book || product.Type == Type.Food || product.Type == Type.Medical))
                    {
                        product.Tax += Math.Ceiling((product.Amount * 0.1) * 20) / 20;
                    }
                }
            }
        }


        static void PrintReceipt()
        {
            double totalAmount = 0;
            double totalTax = 0;
            foreach (var scenario in scenarios)
            {
                totalAmount = 0;
                totalTax = 0;
                Console.WriteLine($"Scenario {scenario.Key} – Output");
                foreach (var product in scenario.Value)
                {
                    totalTax += product.AmountTaxIncluded - product.Amount;
                    totalAmount += product.AmountTaxIncluded;
                    Console.WriteLine($"{product.Quantity} {product.Name} : {product.AmountTaxIncluded:0.##}");
                }

                Console.WriteLine($"Sales Tax: {totalTax:0.##}");
                Console.WriteLine($"Total: {totalAmount:0.##}\n");
            }
        }
    }

    class Product
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Amount { get; set; }

        public double Tax { get; set; }


        public double AmountTaxIncluded
        {
            get
            {
                return Amount + Tax;
            }
        }

        public Type Type { get; set; }

        public bool Imported { get; set; }
    }

    enum Type
    {
        Book = 1,
        Food = 2,
        Medical = 3,
        Other = 4,
    }
}
