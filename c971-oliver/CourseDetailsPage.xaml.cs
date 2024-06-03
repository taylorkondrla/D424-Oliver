using Microsoft.Maui.Controls;
using SQLite;
using c971_oliver.Models;
using Plugin.LocalNotification;
using System.Collections.ObjectModel;

namespace c971_oliver
{
    public partial class CourseDetailsPage : ContentPage
    {
        public Course SelectedCourse { get; set; }
        public Course Course { get; set; }
        public ObservableCollection<string> StatusOptions { get; set; }
        private Database _database;

        public CourseDetailsPage(Course course, Database database)
        {
            InitializeComponent();
            _database = database;
            Course = course;
            SelectedCourse = course;
            StatusOptions = new ObservableCollection<string> { "Planned", "In Progress", "Completed", "Dropped" };
            BindingContext = this;
            InitData();
        }

        private void InitData()
        {
            // Populate UI elements with course data
            courseTitle.Text = SelectedCourse.Title;
            instructorName.Text = Course.InstructorName;
            instructorEmail.Text = Course.InstructorEmail;
            instructorPhone.Text = Course.InstructorPhone;
            notes.Text = Course.Notes;
            courseStartDate.Date = SelectedCourse.StartDate;
            courseEndDate.Date = SelectedCourse.EndDate;
            startDateNotifications.IsToggled = Course.StartDateNotificationEnabled;
            endDateNotifications.IsToggled = Course.EndDateNotificationEnabled;

            // Set the selected status in the picker
            if (StatusOptions.Contains(Course.Status))
            {
                statusSelection.SelectedItem = Course.Status;
            }
        }

        private async void OnAssessmentClicked(object sender, EventArgs e)
        {
            // Pass the selected course's ID to the AssessmentDetailsPage
            await Navigation.PushAsync(new AssessmentDetailsPage(SelectedCourse.Id, _database));
        }


        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Update course details
            Course.Title = courseTitle.Text;
            Course.InstructorName = instructorName.Text;
            Course.InstructorEmail = instructorEmail.Text;
            Course.InstructorPhone = instructorPhone.Text;
            Course.Notes = notes.Text;
            Course.StartDate = courseStartDate.Date;
            Course.EndDate = courseEndDate.Date;
            Course.Status = statusSelection.SelectedItem as string;
            Course.StartDateNotificationEnabled = startDateNotifications.IsToggled;
            Course.EndDateNotificationEnabled = endDateNotifications.IsToggled;

            _database.UpdateCourse(SelectedCourse);
            await DisplayAlert("Success", "Course details saved successfully", "OK");

            // Manage notifications
            if (Course.StartDateNotificationEnabled)
            {
                var startNotification = new NotificationRequest
                {
                    Title = "Course Start Notification",
                    Description = $"Reminder for start date of course: {SelectedCourse.Title}",
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = SelectedCourse.StartDate.AddDays(-1) // Notify a day before the start date
                    }
                };
                await LocalNotificationCenter.Current.Show(startNotification);
            }

            if (Course.EndDateNotificationEnabled)
            {
                var endNotification = new NotificationRequest
                {
                    Title = "Course End Notification",
                    Description = $"Reminder for end date of course: {SelectedCourse.Title}",
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = SelectedCourse.EndDate.AddDays(-1) // Notify a day before the end date
                    }
                };
                await LocalNotificationCenter.Current.Show(endNotification);
            }
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            // Delete the course from the database
            var confirmDelete = await DisplayAlert("Confirmation", "Are you sure you want to delete this course?", "Yes", "No");
            if (confirmDelete)
            {
                _database.DeleteCourse(Course);
                await Navigation.PopAsync();
            }
        }

        private async Task ShareText(string text)
        {
            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Note"
            });
        }

        private async void OnShareNotesClicked(object sender, EventArgs e)
        {
            await ShareText(notes.Text);
        }


        private void StartDateNotificationsToggled(object sender, ToggledEventArgs e)
        {
            Course.StartDateNotificationEnabled = e.Value;
            _database.UpdateCourse(Course);
        }
        private void EndDateNotificationsToggled(object sender, ToggledEventArgs e)
        {
            Course.EndDateNotificationEnabled = e.Value;
            _database.UpdateCourse(Course);
        }


        private void StatusSelectionChanged(object sender, EventArgs e)
        {
            if (statusSelection.SelectedItem != null)
            {
                Course.Status = statusSelection.SelectedItem.ToString();
                _database.UpdateCourse(Course);
            }
        }
    }
}