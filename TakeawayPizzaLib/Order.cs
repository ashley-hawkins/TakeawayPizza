using System.Drawing;
using System.Text.RegularExpressions;

namespace TakeawayPizza
{
    public struct Order
    {
        // Members
        public string Name;
        public string Address;
        public string PhoneNumber;
        public Pizza[] Pizzas;
        public bool NeedsDelivery;

        // Extra stuff
        public const int MinPizzas = 1;
        public const int MaxPizzas = 6;
    }
}