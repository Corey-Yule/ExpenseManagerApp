using ExpenseManagerApp.Database;  // needed for AppDatabase
using ExpenseManagerApp.ViewModels;


namespace ExpenseManagerApp.Views    // must match x:Class in XAML
{
    public partial class MainPage : ContentPage
    {
        private readonly AppDatabase _database;

        public MainPage(AppDatabase database)
        {
            InitializeComponent();
            _database = database;
        }

        private async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddExpensePage(_database));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is ExpenseViewModel vm)
            {
                _ = vm.LoadExpenses();
            }
        }

        private async void OnDeleteExpenseClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Models.Expense expense)
            {
                bool confirm = await DisplayAlert("Delete", $"Delete '{expense.Title}'?", "Yes", "No");
                if (confirm)
                {
                    await _database.DeleteExpenseAsync(expense);

                    if (BindingContext is ExpenseViewModel vm)
                    {
                        await vm.LoadExpenses();
                    }
                }
            }
        }

        private async void OnEditExpenseClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Models.Expense expense)
            {
                // Navigate to EditExpensePage (you’ll need to create this page)
                await Navigation.PushAsync(new EditExpensePage(_database, expense));
            }
        }

    }
}
