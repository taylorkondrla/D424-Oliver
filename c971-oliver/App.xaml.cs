using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Extensions.DependencyInjection;
using C971_oliver.Models;
using c971_oliver;

namespace c971_oliver
{
    public partial class App : Application
    {
        private readonly MauiAppBuilder appBuilder;

        public App()
        {
            appBuilder = MauiApp.CreateBuilder();
            ConfigureServices(appBuilder.Services);

            InitializeComponent();

            // Initialize and setup the database
            var database = new Database();
            database.Initialize();

            // Create the main page with the database instance
            var mainPage = new MainPage(database);

            MainPage = new NavigationPage(mainPage);

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

                return;
            }
            if (e.IsTapped)
            {

                return;
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<Database>();
        }
    }
}