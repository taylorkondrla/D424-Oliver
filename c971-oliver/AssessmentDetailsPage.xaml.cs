using Microsoft.Maui.Controls;
using SQLite;
using c971_oliver.Models;
using Plugin.LocalNotification;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls.Compatibility;
using Android.Telecom;

namespace c971_oliver
{
    public partial class AssessmentDetailsPage : ContentPage
    {
        public ObservableCollection<Assessment> Assessments { get; set; }
        private Database _database;
        public ObservableCollection<string> TypeOptions { get; set; }

        // Constructor

        public AssessmentDetailsPage(int courseId, Database database)
        {
            InitializeComponent();
            _database = database;
            TypeOptions = new ObservableCollection<string> { "Performance Assessment", "Objective Assessment" };
            Assessments = new ObservableCollection<Assessment>(_database.GetAssessmentsByCourseId(courseId)); // Fetch assessments by course ID
            BindingContext = this;
            InitializeUI();
        }


        // Called when the page is about to appear
        protected override void OnAppearing()
        {
            base.OnAppearing();

            //  UI elements are initialized before accessing them
            InitializeUI();
        }

        // Method to initialize UI elements
        private void InitializeUI()
        {

            var assessmentNameEntry = this.FindByName<Entry>("assessmentNameEntry");
            var typePicker = this.FindByName<Picker>("typePicker");
            var startDatePicker = this.FindByName<DatePicker>("startDatePicker");
            var endDatePicker = this.FindByName<DatePicker>("endDatePicker");
            var startDateNotificationSwitch = this.FindByName<Switch>("startDateNotificationSwitch");
            var endDateNotificationSwitch = this.FindByName<Switch>("endDateNotificationSwitch");

            // Check if any UI elements are null
            if (assessmentNameEntry != null && typePicker != null && startDatePicker != null &&
                endDatePicker != null && startDateNotificationSwitch != null && endDateNotificationSwitch != null)
            {

            }
        }

        private void AddAssessmentClicked(object sender, EventArgs e)
        {
            // Create a new assessment and add it to the collection
            var newAssessment = new Assessment
            {
                Name = "",
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = false,
                Type = TypeOptions[0],
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(1)
            };

            _database.AddAssessment(newAssessment);
            Assessments.Add(newAssessment);
        }

        private void DeleteAssessmentClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var assessment = button.BindingContext as Assessment;
                if (assessment != null)
                {
                    _database.DeleteAssessment(assessment);
                    Assessments.Remove(assessment);
                }
            }
        }


        private void OnSaveClicked(object sender, EventArgs e)
        {

        }
        private void AssessmentNameChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            var assessment = entry?.BindingContext as Assessment;

            if (assessment != null && e.NewTextValue != null)
            {
                assessment.Name = e.NewTextValue;
                _database.UpdateAssessment(assessment);
            }
        }

        private void AssessmentStartDateChanged(object sender, DateChangedEventArgs e)
        {
            var datePicker = sender as DatePicker;
            var assessment = datePicker?.BindingContext as Assessment;

            if (assessment != null)
            {
                assessment.StartDate = e.NewDate;
                _database.UpdateAssessment(assessment);
            }
        }

        private void AssessmentEndDateChanged(object sender, DateChangedEventArgs e)
        {
            var datePicker = sender as DatePicker;
            var assessment = datePicker?.BindingContext as Assessment;

            if (assessment != null)
            {
                assessment.EndDate = e.NewDate;
                _database.UpdateAssessment(assessment);
            }
        }

        private async void StartDateNotificationToggled(object sender, ToggledEventArgs e)
        {
            var toggleSwitch = sender as Switch;
            var assessment = toggleSwitch?.BindingContext as Assessment;

            if (assessment != null)
            {
                assessment.StartDateNotificationEnabled = e.Value;
                _database.UpdateAssessment(assessment);

                if (assessment.StartDateNotificationEnabled)
                {
                    var startNotification = new NotificationRequest
                    {
                        Title = "Assessment Start Notification",
                        Description = $"Reminder for start date of assessment: {assessment.Name}",
                        Schedule = new NotificationRequestSchedule
                        {
                            NotifyTime = assessment.StartDate.AddDays(-1) // Notify a day before the start date
                        }
                    };
                    await LocalNotificationCenter.Current.Show(startNotification);
                }
            }
        }

        private async void EndDateNotificationToggled(object sender, ToggledEventArgs e)
        {
            var toggleSwitch = sender as Switch;
            var assessment = toggleSwitch?.BindingContext as Assessment;

            if (assessment != null)
            {
                assessment.EndDateNotificationEnabled = e.Value;
                _database.UpdateAssessment(assessment);

                if (assessment.EndDateNotificationEnabled)
                {
                    var endNotification = new NotificationRequest
                    {
                        Title = "Assessment End Notification",
                        Description = $"Reminder for end date of assessment: {assessment.Name}",
                        Schedule = new NotificationRequestSchedule
                        {
                            NotifyTime = assessment.EndDate.AddDays(-1) // Notify a day before the end date
                        }
                    };
                    await LocalNotificationCenter.Current.Show(endNotification);
                }
            }
        }


        private void TypePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            var assessment = picker?.BindingContext as Assessment;

            if (assessment != null && picker != null)
            {
                // Update the assessment's type based on the selected item in the picker
                assessment.Type = (string)picker.SelectedItem;
                _database.UpdateAssessment(assessment);
            }
        }
    }

}