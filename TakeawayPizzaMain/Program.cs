using TakeawayPizza;
var order = Takeaway.InputOrder();
var receipt = Takeaway.GenerateReceipt(order);
Takeaway.PrintReceipt(receipt);