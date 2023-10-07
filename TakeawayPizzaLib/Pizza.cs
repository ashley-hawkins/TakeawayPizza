namespace TakeawayPizza
{
    public struct Pizza
    {
        // Properties
        public PizzaSize Size;
        public List<Topping> Toppings;

        // Constants
        public const int MinToppings = 0;
        public const int MaxToppings = 6;

        // Implementation
        public enum PizzaSize
        {
            Small,
            Medium,
            Large,
            ExtraLarge
        }
        public static readonly Dictionary<PizzaSize, string> SizeToStringDict = new() {
            { PizzaSize.Small, "Small" },
            { PizzaSize.Medium, "Medium" },
            { PizzaSize.Large, "Large" },
            { PizzaSize.ExtraLarge, "Extra Large" },
        };
        public static readonly Dictionary<string, Pizza.PizzaSize> StringToSizeDict = new() {
            { "S", PizzaSize.Small },
            { "M", PizzaSize.Medium },
            { "L", PizzaSize.Large },
            { "XL", PizzaSize.ExtraLarge },
        };

        public enum Topping
        {
            Anchovies,
            Bacon,
            BarbecueDrizzle,
            Chicken,
            GreenPeppers,
            Ham,
            Onions,
            Pepperoni,
            Pineapple,
            PremiumCheeseBlend,
            VeganCheese,
            VeganSausage
        }
        public static readonly Dictionary<Topping, string> ToppingToStringDict = new() {
            { Topping.Anchovies, "Anchovies" },
            { Topping.Bacon, "Bacon" },
            { Topping.BarbecueDrizzle, "Barbecue Drizzle" },
            { Topping.Chicken, "Chicken" },
            { Topping.GreenPeppers, "Green Peppers" },
            { Topping.Ham, "Ham" },
            { Topping.Onions, "Onions" },
            { Topping.Pepperoni, "Pepperoni" },
            { Topping.Pineapple, "Pineapple" },
            { Topping.PremiumCheeseBlend, "Premium Cheese Blend" },
            { Topping.VeganCheese, "Vegan Cheese" },
            { Topping.VeganSausage, "Vegan Sausage" }
        };

        public static decimal GetPizzaPrice(PizzaSize size)
        {
            switch (size)
            {
                case PizzaSize.Small:
                    return 5.95m;
                case PizzaSize.Medium:
                    return 7.50m;
                case PizzaSize.Large:
                    return 10.95m;
                case PizzaSize.ExtraLarge:
                    return 14.95m;
            }
            throw new ArgumentOutOfRangeException("Invalid size specified.");
        }
        public static decimal GetToppingsPrice(int amount)
        {
            if (!(MinToppings <= amount && amount <= MaxToppings)) throw new ArgumentOutOfRangeException($"Argument 'amount' must be in the range [{MinToppings}, {MaxToppings}]");
            switch (amount)
            {
                case 0:
                    return 0;
                case 1:
                    return 0.75m;
                case 2:
                    return 1.35m;
                case 3:
                    return 2.00m;
                default:
                    return 2.50m;
            }
        }
        public readonly ReceiptItem GetReceiptItem()
        {
            ReceiptItem result = new()
            {
                Name = $"{SizeToStringDict[Size]} Pizza",
                Price = GetPizzaPrice(Size)
            };
            if (Toppings.Count != 0)
            {
                result.SubItems = new() {
                    new()
                    {
                        Name = $"Toppings x{6}",
                        Price = GetToppingsPrice(Toppings.Count),
                        SubItems = Toppings.Select(x => new ReceiptItem() { Name = ToppingToStringDict[x] } ).ToList()
                    }
                };
            }
            return result;
        }
    }
}
