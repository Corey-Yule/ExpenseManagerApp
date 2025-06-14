namespace ExpenseManagerApp.Views;

using Microsoft.Extensions.DependencyInjection;
using ExpenseManagerApp.Database;
using ExpenseManagerApp.ViewModels;
using ExpenseManagerApp;          // for App.Services
using ExpenseManagerApp.Models;   // for Expense
using ExpenseManagerApp.Views;    // for MainPage

public partial class Dashboard : ContentPage
{
    private readonly DashboardViewModel _viewModel;

    public Dashboard()
    {
        InitializeComponent();

        var database = App.Services.GetRequiredService<AppDatabase>();
        _viewModel = new DashboardViewModel(database);
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadRecentTransactions();
    }

    private void OnTransactionSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Expense selectedExpense)
        {
            DisplayAlert("Transaction Selected", $"You selected: {selectedExpense.Title} (${selectedExpense.Amount:F2})", "OK");
            ((CollectionView)sender).SelectedItem = null;
        }
    }

    private async void OnExpensesClicked(object sender, EventArgs e)
    {
        var database = App.Services.GetRequiredService<AppDatabase>();
        await Navigation.PushAsync(new MainPage(database));
    }

    private async void OnTotalExpensesTapped(object sender, EventArgs e)
    {
        var database = App.Services.GetRequiredService<AppDatabase>();
        await Navigation.PushAsync(new MainPage(database)); // Or the correct page class name for your expenses
    }

    private async void OnTotalIncomeTapped(object sender, EventArgs e)
    {
        var database = App.Services.GetRequiredService<AppDatabase>();
        await Navigation.PushAsync(new MainPage(database)); // Or the correct page class name for your income
    }
}
