using ExpenseManagerApp.Database;
using ExpenseManagerApp.Models;

namespace ExpenseManagerApp.Views;

public partial class AddExpensePage : ContentPage
{
    private readonly AppDatabase _database;

    public AddExpensePage(AppDatabase database)
    {
        InitializeComponent();
        _database = database;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var expense = new Expense
        {
            Title = TitleEntry.Text,
            Amount = decimal.TryParse(AmountEntry.Text, out decimal amt) ? amt : 0,
            Category = CategoryEntry.Text,
            Date = DatePicker.Date
        };

        await _database.SaveExpenseAsync(expense);
        await Navigation.PopAsync(); // Go back to MainPage
    }
}
