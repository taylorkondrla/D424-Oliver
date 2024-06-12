using System;
using Microsoft.Maui.Controls;
using SQLite;
using System.IO;
using System.Threading.Tasks;

using c971_oliver.Models;

namespace c971_oliver
{
    public partial class LoginPage : ContentPage
    {
        private SQLiteAsyncConnection _userDb;
        private Database _c971db;

        public LoginPage(Database c971Database)
        {
            InitializeComponent();
            _c971db = c971Database;
            _userDb = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "users.db3"));
            _userDb.CreateTableAsync<User>().Wait();
        }


        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text.Trim();
            var user = await _userDb.Table<User>().Where(u => u.Username == username).FirstOrDefaultAsync();
            if (user != null)
            {
                if (user.Password == PasswordEntry.Text)
                {
                    // Set the current user in the session
                    App.SetCurrentUser(user.Username);
                    App.InitializeUserDatabaseService(user.Username);

                    // Navigate to MainPage
                    Application.Current.MainPage = new NavigationPage(new MainPage(_c971db));
                }
                else
                {
                    // Show invalid password message
                    await DisplayAlert("Error", "Invalid password.", "OK");
                }
            }
            else
            {
                // Show invalid username message
                await DisplayAlert("Error", "Invalid username.", "OK");
                Console.WriteLine($"No user found with username: {username}");
            }
        }






        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            try
            {
                var user = new User
                {
                    Username = UsernameEntry.Text,
                    Password = PasswordEntry.Text
                };
                await _userDb.InsertAsync(user);
                // Show registration successful message
                await DisplayAlert("Success", "Registration successful. Please log in.", "OK");
            }
            catch (Exception ex)
            {
                // Show error message if registration fails
                await DisplayAlert("Error", $"Error registering user: {ex.Message}", "OK");
            }
        }


    }
}
