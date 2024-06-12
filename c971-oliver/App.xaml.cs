using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Extensions.DependencyInjection;
using c971_oliver.Models;
using System.IO;
using SQLite;

namespace c971_oliver
{
    public partial class App : Application
    {
        public static string CurrentUser { get; private set; }
        public static SQLiteAsyncConnection UserDbConnection { get; private set; }
        private readonly MauiAppBuilder appBuilder;

        public App()
        {
            appBuilder = MauiApp.CreateBuilder();
            ConfigureServices(appBuilder.Services);

            InitializeComponent();

            // Initialize and setup the C971 database
            var database = new Database();
            database.Initialize();

            // Set the initial page to LoginPage
            MainPage = new NavigationPage(new LoginPage(database));

            LocalNotificationCenter.Current.NotificationActionTapped += OnNotificationActionTapped;
        }

        protected override void OnStart()
        {
            base.OnStart();

            // Call the StartupNotifications method when the application starts
            DataFunctions.StartupNotifications();
        }

        private void OnNotificationActionTapped(NotificationActionEventArgs e)
        {
            if (e.IsDismissed)
            {
                // Handle notification dismissed action
                return;
            }
            if (e.IsTapped)
            {
                // Handle notification tapped action
                return;
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Database>();
        }

        public static void SetCurrentUser(string username)
        {
            CurrentUser = username;
        }

        public static void InitializeUserDatabaseService(string username)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, $"{username}_data.db3");
            UserDbConnection = new SQLiteAsyncConnection(dbPath);
            UserDbConnection.CreateTableAsync<Course>().Wait();
            UserDbConnection.CreateTableAsync<Assessment>().Wait();
            UserDbConnection.CreateTableAsync<Note>().Wait();
        }
    }
}
