using ExpenseManagerApp.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManagerApp.Database
{
    public class AppDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public AppDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Expense>().Wait();
        }

        // Get all expenses
        public Task<List<Expense>> GetExpensesAsync() =>
            _database.Table<Expense>().ToListAsync();

        // Insert or update expense
        public Task<int> SaveExpenseAsync(Expense expense)
        {
            if (expense.Id != 0)
                return _database.UpdateAsync(expense);
            else
                return _database.InsertAsync(expense);
        }

        // Delete an expense
        public Task<int> DeleteExpenseAsync(Expense expense) =>
            _database.DeleteAsync(expense);

        public Task<int> UpdateExpenseAsync(Expense expense) =>
            _database.UpdateAsync(expense);
    }
}
