using TakeawayPizza;

namespace TakeawayPizzaTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Pizza_GetToppingsPrice()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Pizza.GetToppingsPrice(0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Pizza.GetToppingsPrice(-1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Pizza.GetToppingsPrice(7));
            Assert.AreEqual(0.75m, Pizza.GetToppingsPrice(1));
            Assert.AreEqual(1.35m, Pizza.GetToppingsPrice(2));
            Assert.AreEqual(2.00m, Pizza.GetToppingsPrice(3));
            Assert.AreEqual(2.50m, Pizza.GetToppingsPrice(4));
            Assert.AreEqual(2.50m, Pizza.GetToppingsPrice(5));
            Assert.AreEqual(2.50m, Pizza.GetToppingsPrice(6));
        }
        [TestMethod]
        public void Pizza_GetPizzaPrice()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Pizza.GetPizzaPrice((Pizza.PizzaSize)(-1)));
        }
        [TestMethod]
        public void Pizza_ToppingToStringDict()
        {
            Assert.IsTrue(Enum.GetValues<Pizza.Topping>().All((x) => Pizza.ToppingToStringDict.ContainsKey(x)));
        }
    }
}