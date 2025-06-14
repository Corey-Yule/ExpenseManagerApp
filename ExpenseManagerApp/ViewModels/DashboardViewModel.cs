using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using ExpenseManagerApp.Database;
using ExpenseManagerApp.Models;
using Microsoft.Extensions.DependencyInjection;
using ExpenseManagerApp;
using System.Linq;

public class DashboardViewModel : INotifyPropertyChanged
{
    private readonly AppDatabase _database;

    public ObservableCollection<Expense> RecentTransactions { get; set; } = new();

    private decimal _totalIncome;
    public decimal TotalIncome
    {
        get => _totalIncome;
        set
        {
            _totalIncome = value;
            OnPropertyChanged(nameof(TotalIncome));
        }
    }

    private decimal _totalExpenses;
    public decimal TotalExpenses
    {
        get => _totalExpenses;
        set
        {
            _totalExpenses = value;
            OnPropertyChanged(nameof(TotalExpenses));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public DashboardViewModel(AppDatabase database)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
    }

    public async Task LoadRecentTransactions()
    {
        var items = await _database.GetExpensesAsync() ?? new List<Expense>();

        RecentTransactions.Clear();
        foreach (var item in items.OrderByDescending(e => e.Date).Take(5))
            RecentTransactions.Add(item);

        TotalIncome = items.Where(x => x.Amount < 0).Sum(x => Math.Abs(x.Amount));
        TotalExpenses = items.Where(x => x.Amount > 0).Sum(x => x.Amount);
    }
}
