using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Util;

namespace TakeawayPizza
{
    public class ReceiptItem
    {
        public string Name = String.Empty;
        public decimal? Price;
        public List<ReceiptItem> SubItems = new();

        public decimal GetTotalPrice()
        {
            decimal result = Price ?? 0;
            foreach (ReceiptItem item in SubItems)
                result += item.GetTotalPrice();
            return result;
        }
    }

    public static class Takeaway
    {
        public const decimal DiscountMinPrice = 20;
        public const decimal DiscountMultiplier = 0.10m;

        public const decimal DeliveryCharge = 2.50m;

        public static Pizza InputPizza(int n)
        {
            Pizza pizza = default;
            string pizzaName = $"pizza {n}";

            // Take input for pizza.Size
            Util.Input.ValidatedInput(
                $"Please input the size of {pizzaName} (S, M, L, or XL): ",
                out pizza.Size,
                (string inputLine) =>
                {
                    inputLine = Regex.Replace(inputLine, @"\s", "").ToUpper();
                    if (Pizza.StringToSizeDict.ContainsKey(inputLine))
                    {
                        return (Pizza.StringToSizeDict[inputLine], "", false);
                    }
                    else
                    {
                        return (null, "That is not a valid size.", true);
                    }
                });

            // Take input for the number of toppings
            Util.Input.ValidatedInput(
                $"Please input the number of toppings for {pizzaName} ({Pizza.MinToppings}-{Pizza.MaxToppings}): ",
                out int numberOfToppings,
                (string inputLine) =>
                {
                    inputLine = inputLine.Trim();
                    if (int.TryParse(inputLine, out int res))
                    {
                        if (res < Pizza.MinToppings)
                        {
                            return (null, "That is below the minimum toppings you can specify.", true);
                        }
                        else if (res > Pizza.MaxToppings)
                        {
                            return (null, "That is above the maximum number of toppings you can specify.", true);
                        }
                        else
                        {
                            return (res, "", false);
                        }
                    }
                    else
                    {
                        return (null, "That is not an integer.", true);
                    }
                });

            // If the user wants toppings, format the string that is displayed to the user to show the topping IDs
            if (numberOfToppings != 0)
            {
                string toppingsListString = "";
                foreach (var topping in Enum.GetValues<Pizza.Topping>())
                {
                    toppingsListString += $"{(int)topping + 1}) {Pizza.ToppingToStringDict[topping]}\n";
                }

                Console.WriteLine($"The following is a list of topping ID numbers followed by the topping they represent:\n{toppingsListString}");
            }

            pizza.Toppings = new();

            // Loop to take input for each topping
            for (int i = 0; i < numberOfToppings; ++i)
            {
                // Take input for the topping ID and validate it as a valid Pizza.Topping
                Util.Input.ValidatedInput(
                    $"Please input the topping ID for the {Util.Formatting.DecorateWithOrdinalIndicator(i + 1)} topping on {pizzaName}: ",
                    out Pizza.Topping currentTopping,
                    (string inputLine) =>
                    {
                        inputLine = inputLine.Trim();
                        if (int.TryParse(inputLine, out int res))
                        {
                            if (Enum.IsDefined<Pizza.Topping>((Pizza.Topping)res))
                            {
                                return ((Pizza.Topping)res, "", false);
                            }
                            else
                            {
                                return (null, "That is not a valid topping ID.", true);
                            }
                        }
                        else
                        {
                            return (null, "That is not a number.", true);
                        }
                    });
                pizza.Toppings.Add(currentTopping);
            }
            return pizza;
        }
        public static Order InputOrder()
        {
            Order order = default;
            Console.Write("Please input your name (forename and surname): ");
            order.Name = Console.ReadLine().Trim();
            Console.Write("Please input your address: ");
            order.Address = Console.ReadLine().Trim();

            // Take input for phone number
            Util.Input.ValidatedInput(
                "Please input your phone number: ",
                out string? phoneNumber,
                (string inputLine) =>
                {
                    inputLine = inputLine.Trim();
                    if (inputLine.All(c => Char.IsDigit(c)))
                    {
                        return (inputLine, "", false);
                    }
                    else
                    {
                        return (null, "That is not a number.", true);
                    }
                });

            order.PhoneNumber = phoneNumber;

            // Take input for number of pizzas
            Util.Input.ValidatedInput(
                $"Please input the number of pizzas that you would like to order ({Order.MinPizzas}-{Order.MaxPizzas}): ",
                out int pizzaAmount,
                (string inputLine) =>
                {
                    inputLine = inputLine.Trim();
                    if (int.TryParse(inputLine, out int res))
                    {
                        if (res < Order.MinPizzas)
                        {
                            return (null, "That is below the minimum pizzas you can specify.", true);
                        }
                        else if (res > Order.MaxPizzas)
                        {
                            return (null, "That is above the maximum number of pizzas you can specify.", true);
                        }
                        else
                        {
                            return (res, "", false);
                        }
                    }
                    else
                    {
                        return (null, "That is not a number.", true);
                    }
                });

            order.Pizzas = new();
            // Loop to take input for each pizza
            for (int i = 1; i <= pizzaAmount; ++i)
            {
                order.Pizzas.Add(InputPizza(i));
            }

            // Take input for whether the order needs to be delivered
            Util.Input.ValidatedInput(
                "Would you like to have your order delivered to you? (Y/N): ",
                out order.NeedsDelivery,
                (string inputLine) =>
                {
                    inputLine = inputLine.Trim().ToUpper();
                    if (inputLine == "Y" || inputLine == "N")
                    {
                        return (inputLine == "Y", "", false);
                    }
                    else
                    {
                        return (null, "That is not Y or N.", true);
                    }
                });

            return order;
        }
        public static List<ReceiptItem> GenerateReceipt(Order order)
        {
            List<ReceiptItem> result = new();
            foreach (Pizza pizza in order.Pizzas)
            {
                result.Add(pizza.GetReceiptItem());
            }
            if (order.NeedsDelivery)
            {
                result.Add(new ReceiptItem()
                {
                    Name = "Delivery",
                    Price = DeliveryCharge
                });
            }
            var totalPrice = result.Aggregate(0m, (x, y) => x + y.GetTotalPrice());
            if (totalPrice > DiscountMinPrice)
            {
                result.Add(new ReceiptItem()
                {
                    Name = $"{DiscountMultiplier * 100}% discount for orders over £{DiscountMinPrice}",
                    Price = -DiscountMultiplier * totalPrice
                });
            }

            return result;
        }

        const int ReceiptAlignmentLeft = 40;
        const int ReceiptAlignmentRight = 6;
        static void PrintReceiptRecursive(ReceiptItem item, int indentPerLevel, int currentIndent = 0)
        {
            var indentedName = new string(' ', currentIndent) + item.Name;
            Console.Write($"{indentedName,-ReceiptAlignmentLeft}");
            if (item.Price != null)
            {
                Console.Write($"{item.Price,ReceiptAlignmentRight:0.00}");
            }
            Console.WriteLine();
            foreach (ReceiptItem subItem in item.SubItems)
            {
                PrintReceiptRecursive(subItem, indentPerLevel, currentIndent + indentPerLevel);
            }
        }
        public static void PrintReceipt(List<ReceiptItem> receipt, int indentPerLevel = 2)
        {
            var totalPrice = receipt.Aggregate(0m, (x, y) => x + y.GetTotalPrice());
            foreach (ReceiptItem item in receipt)
            {
                PrintReceiptRecursive(item, indentPerLevel);
            }
            Console.WriteLine($"{"Total:",-ReceiptAlignmentLeft}{totalPrice,ReceiptAlignmentRight:0.00}");
        }
    }
}
