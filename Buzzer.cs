namespace GuiEventsLab;

// Buzzer: a subscriber that can also stop listening. It records the
// messages a customer's phone would show, like
// "Buzz: Ada, your latte is ready."
//
// Part B. Write Connect, Disconnect, and OnOrderReady along with the
// Session 1 demo.
public class Buzzer
{
    private readonly List<string> messages = new List<string>();

    // Given.
    public List<string> Messages()
    {
        return new List<string>(messages);
    }

    public void Connect(CoffeeBar bar)
    {
        // TODO: Part B, demo
        bar.OrderReady += OnOrderReady;
    }

    // Unsubscribe with the same method that subscribed.
    public void Disconnect(CoffeeBar bar)
    {
        // TODO: Part B, demo
        bar.OrderReady -= OnOrderReady;
    }

    private void OnOrderReady(object? sender, OrderEventArgs e)
    {
        // TODO: Part B, demo
        messages.Add($"Buzz: {e.Order.Customer}, your {e.Order.Drink} is ready.");
    }
}
