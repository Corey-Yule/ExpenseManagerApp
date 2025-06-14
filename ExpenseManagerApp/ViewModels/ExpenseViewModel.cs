using System.Collections.ObjectModel;
using System.ComponentModel;
using ExpenseManagerApp.Database;
using ExpenseManagerApp.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseManagerApp.ViewModels
{
    public class ExpenseViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Expense> Expenses { get; set; } = new();
        private readonly AppDatabase _database;

        public event PropertyChangedEventHandler PropertyChanged;

        public ExpenseViewModel()
        {
            _database = App.Services.GetRequiredService<AppDatabase>();
            _ = LoadExpenses();  // fire-and-forget, or await if you can
        }

        public async Task LoadExpenses()
        {
            var items = await _database.GetExpensesAsync();
            Expenses.Clear();
            foreach (var item in items)
                Expenses.Add(item);
        }
    }
}
