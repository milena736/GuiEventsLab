using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GuiEventsLab;

// MainWindow: the code behind the window. It shows the coffee bar and asks
// it to do things. Every decision about orders stays in CoffeeBar.
//
// Part C and Part D are written along with the Session 2 demos.
// Exercise 3 and Exercise 4 are yours.
public partial class MainWindow : Window
{
    private readonly CoffeeBar bar = new CoffeeBar();
    private readonly Rewards rewards = new Rewards();

    public MainWindow()
    {
        InitializeComponent();

        // TODO: Part C, demo. Subscribe to bar.OrderPlaced.
        bar.OrderPlaced += Bar_OrderPlaced;

        // TODO: Exercise 3. Subscribe to bar.OrderReady and bar.AllCaughtUp.
        bar.OrderReady += Bar_OrderReady;
        bar.AllCaughtUp += Bar_AllCaughtUp;

        // TODO: Part D, demo. Subscribe to TextChanged on both boxes,
        // then set the buttons for an empty window.
        CustomerBox.TextChanged += Boxes_TextChanged;
        DrinkBox.TextChanged += Boxes_TextChanged;

        // TODO: Exercise 4. Connect a Rewards object to the bar and
        // subscribe to its FreeDrinkEarned event.
        rewards.Connect(bar);
        rewards.FreeDrinkEarned += Rewards_FreeDrinkEarned;

        UpdateButtons();
    }
    //Part C: read the boxes, ask the bar, and show any refusal to the person.
    private void PlaceButton_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            bar.Place(CustomerBox.Text ?? "", DrinkBox.Text ?? "");
            CustomerBox.Text = "";
            DrinkBox.Text = "";
            StatusText.Text = "";
        }
        catch (ArgumentException ex)
        {
            StatusText.Text = ex.Message;
        }
    }
    //Part C: show new order
    private void Bar_OrderPlaced(object? sender, OrderEventArgs e)
    {
        ShowWaiting();
        UpdateButtons();
    }
    //EX 3: ask bar to finish oldest order
    private void ReadyButton_Click(object? sender, RoutedEventArgs e)
    {
        bar.FinishNext();
    }
    //EX 3: moved order to pickup list
    private void Bar_OrderReady(object? sender, OrderEventArgs e)
    {
        PickupList.Items.Add($"#{e.Order.Number} {e.Order.Customer}");
        ShowWaiting();
        UpdateButtons();
    }
    //EX 3: bar is all caught up
    private void Bar_AllCaughtUp(object? sender, EventArgs e)
    {
        StatusText.Text = "All caught up!";
        
    }
    //Part D: used every time either box changes
    private void Boxes_TextChanged(object? sender, TextChangedEventArgs e)
    {
        UpdateButtons();
    }
    //EX: 4: a subscriber of a subscriber
    private void Rewards_FreeDrinkEarned(object? sender, OrderEventArgs e)
    {
        StatusText.Text = $"Free drink earned: {e.Order.Customer}";
    }
    // Part C: show the waiting list in the window.
    private void ShowWaiting()
    {
        WaitingList.Items.Clear();
        foreach (Order order in bar.Waiting())
        {
            WaitingList.Items.Add(order.ToString());
        }
    }
    // Part D: enable or disable the buttons
    private void UpdateButtons()
    {
        PlaceButton.IsEnabled = !string.IsNullOrWhiteSpace(CustomerBox.Text) && !string.IsNullOrWhiteSpace(DrinkBox.Text);
        ReadyButton.IsEnabled = bar.WaitingCount > 0;
    }




    // Write the click handlers, the event handlers, and the helper
    // methods below this line.
}
