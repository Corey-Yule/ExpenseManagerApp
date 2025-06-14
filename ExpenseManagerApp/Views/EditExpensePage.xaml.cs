using ExpenseManagerApp.Database;
using ExpenseManagerApp.Models;
using System;

namespace ExpenseManagerApp.Views
{
    public partial class EditExpensePage : ContentPage
    {
        private readonly AppDatabase _database;
        private readonly Expense _expense;

        public EditExpensePage(AppDatabase database, Expense expense)
        {
            InitializeComponent();
            _database = database;
            _expense = expense;

            // Pre-fill form fields with existing data
            TitleEntry.Text = expense.Title;
            AmountEntry.Text = expense.Amount.ToString("F2");
            CategoryEntry.Text = expense.Category;
            DatePicker.Date = expense.Date;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            _expense.Title = TitleEntry.Text;
            _expense.Category = CategoryEntry.Text;
            _expense.Date = DatePicker.Date;
            _expense.Amount = decimal.TryParse(AmountEntry.Text, out var amt) ? amt : 0;

            await _database.UpdateExpenseAsync(_expense);
            await Navigation.PopAsync();
        }
    }
}
