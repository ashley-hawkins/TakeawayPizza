using System.Drawing;
using System.Text.RegularExpressions;

namespace TakeawayPizza
{
    public struct Order
    {
        // Properties
        public string Name;
        public string Address;
        public string PhoneNumber;
        public List<Pizza> Pizzas;
        public bool NeedsDelivery;

        // Constants
        public const int MinPizzas = 1;
        public const int MaxPizzas = 6;
    }
}