using ExpenseManagerApp.Database;
using ExpenseManagerApp.Views;   // <-- THIS IS IMPORTANT

namespace ExpenseManagerApp
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            Services = serviceProvider;

            var db = Services.GetRequiredService<AppDatabase>();
            MainPage = new NavigationPage(new MainPage(db));   // <--- now works
        }
    }
}
