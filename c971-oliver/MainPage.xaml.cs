using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using c971_oliver.Models;

namespace c971_oliver
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Term> Terms { get; set; }
        private Database _database;
        public ObservableCollection<Course> SearchResults { get; set; }
        public ObservableCollection<string> TypeOptions { get; } = new ObservableCollection<string> { "Performance Assessment", "Objective Assessment" };
        public MainPage(Database database)
        {
            InitializeComponent();
            _database = database;
            ExampleData();
            Terms = new ObservableCollection<Term>(_database.GetAllTerms());
            BindingContext = this;
            SearchResults = new ObservableCollection<Course>();
            SearchResultsListView.ItemsSource = SearchResults;


        }
        private void OnSearchClicked(object sender, EventArgs e)
        {
            // Get the selected date from the DatePicker
            DateTime selectedDate = SearchDatePicker.Date;

            // Filter the list of courses to find those that fall within the selected date range
            IEnumerable<Course> coursesOnDate = _database.GetAllCourses().Where(course =>
                course.StartDate <= selectedDate && selectedDate <= course.EndDate);

            // Update the SearchResults collection with the filtered courses
            SearchResults.Clear();
            foreach (var course in coursesOnDate)
            {
                SearchResults.Add(course);
            }
        }



        private void ExampleData()
        {
            _database.Initialize(); // Initialize the database

            // Clear existing data 
            _database.ClearDatabase();

            // Create and add terms
            Term term1 = new Term { Title = "Term 1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(6) };
            Term term2 = new Term { Title = "Term 2", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(6) };
            _database.AddTerm(term1);
            _database.AddTerm(term2);

            // Retrieve the inserted terms to get their IDs
            term1 = _database.GetAllTerms().First(t => t.Title == "Term 1");
            term2 = _database.GetAllTerms().First(t => t.Title == "Term 2");

            // Create courses for term 1
            Course course1 = new Course
            {
                TermId = term1.Id,
                Title = "Math 101",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                Status = "In Progress",
                InstructorName = "Anika Patel",
                InstructorPhone = "555-123-4567",
                InstructorEmail = "anika.patel@strimeuniversity.edu",
                StartDateNotificationEnabled = true,
                EndDateNotificationEnabled = true,
                Notes = "Test note for math 101 course"
            };
            Course course2 = new Course
            {
                TermId = term1.Id,
                Title = "English 101",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                Status = "Completed",
                InstructorName = "Taylor Swift",
                InstructorPhone = "131-123-1313",
                InstructorEmail = "taylor.swift@strimeuniversity.edu",
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Notes = "Test note for english 101 course"
            };
            Course course3 = new Course
            {
                TermId = term1.Id,
                Title = "History 101",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                Status = "In Progress",
                InstructorName = "Jalen Hurts",
                InstructorPhone = "555-123-4567",
                InstructorEmail = "jalen.hurts@strimeuniversity.edu",
                StartDateNotificationEnabled = true,
                EndDateNotificationEnabled = true,
                Notes = "Test note for math 101 course"
            };
            Course course4 = new Course
            {
                TermId = term1.Id,
                Title = "Science 101",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                Status = "Completed",
                InstructorName = "Michael Jordan",
                InstructorPhone = "131-123-1313",
                InstructorEmail = "michael.jordan@strimeuniversity.edu",
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Notes = "Test note for english 101 course"
            };
            Course course5 = new Course
            {
                TermId = term1.Id,
                Title = "Spanish 101",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                Status = "In Progress",
                InstructorName = "Anika Patel",
                InstructorPhone = "555-123-4567",
                InstructorEmail = "anika.patel@strimeuniversity.edu",
                StartDateNotificationEnabled = true,
                EndDateNotificationEnabled = true,
                Notes = "Test note for math 101 course"
            };
            Course course6 = new Course
            {
                TermId = term1.Id,
                Title = "Writing 101",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                Status = "Completed",
                InstructorName = "Serena Williams",
                InstructorPhone = "131-123-1313",
                InstructorEmail = "serena.williams@strimeuniversity.edu",
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Notes = "Test note for english 101 course"
            };
            _database.AddCourse(course1);
            _database.AddCourse(course2);
            _database.AddCourse(course3);
            _database.AddCourse(course4);
            _database.AddCourse(course5);
            _database.AddCourse(course6);

            // Create courses for term 2
            Course course7 = new Course
            {
                TermId = term2.Id,
                Title = "Math 202",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                Status = "Planned",
                InstructorName = "Travis Kelce",
                InstructorPhone = "555-123-4569",
                InstructorEmail = "travis.kelce@strimeuniversity.edu",
                StartDateNotificationEnabled = true,
                EndDateNotificationEnabled = true,
                Notes = "Test note for science 202 course"
            };
            Course course8 = new Course
            {
                TermId = term2.Id,
                Title = "Englsih 202",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                Status = "Planned",
                InstructorName = "Harry Styles",
                InstructorPhone = "555-123-4570",
                InstructorEmail = "harry.styles@strimeuniversity.edu",
                StartDateNotificationEnabled = true,
                EndDateNotificationEnabled = true,
                Notes = "Test note for history 202 course"
            };
            Course course9 = new Course
            {
                TermId = term2.Id,
                Title = "History 202",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                Status = "Planned",
                InstructorName = "Jason Kelce",
                InstructorPhone = "555-123-4569",
                InstructorEmail = "jason.kelce@strimeuniversity.edu",
                StartDateNotificationEnabled = true,
                EndDateNotificationEnabled = true,
                Notes = "Test note for science 202 course"
            };
            Course course10 = new Course
            {
                TermId = term2.Id,
                Title = "Science 202",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                Status = "Planned",
                InstructorName = "Lebron James",
                InstructorPhone = "555-123-4570",
                InstructorEmail = "lebron.james@strimeuniversity.edu",
                StartDateNotificationEnabled = true,
                EndDateNotificationEnabled = true,
                Notes = "Test note for history 202 course"
            };
            Course course11 = new Course
            {
                TermId = term2.Id,
                Title = "Spanish 202",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                Status = "Planned",
                InstructorName = "Britney Spears",
                InstructorPhone = "555-123-4569",
                InstructorEmail = "britney.spears@strimeuniversity.edu",
                StartDateNotificationEnabled = true,
                EndDateNotificationEnabled = true,
                Notes = "Test note for science 202 course"
            };
            Course course12 = new Course
            {
                TermId = term2.Id,
                Title = "Writing 202",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                Status = "Planned",
                InstructorName = "Dolly Parton",
                InstructorPhone = "555-123-4570",
                InstructorEmail = "dolly.parton@strimeuniversity.edu",
                StartDateNotificationEnabled = true,
                EndDateNotificationEnabled = true,
                Notes = "Test note for history 202 course"
            };
            _database.AddCourse(course7);
            _database.AddCourse(course8);
            _database.AddCourse(course9);
            _database.AddCourse(course10);
            _database.AddCourse(course11);
            _database.AddCourse(course12);

            // Create assessments for course1
            Assessment pa1 = new Assessment
            {
                CourseId = course1.Id,
                Name = "Performance Assessment 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[0]
            };
            Assessment oa1 = new Assessment
            {
                CourseId = course1.Id,
                Name = "Objective Assessment 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[1]
            };
            _database.AddAssessment(pa1);
            _database.AddAssessment(oa1);

            // Create assessments for course2
            Assessment pa2 = new Assessment
            {
                CourseId = course2.Id,
                Name = "Performance Assessment 2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[0]
            };
            Assessment oa2 = new Assessment
            {
                CourseId = course2.Id,
                Name = "Objective Assessment 2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[1]
            };
            _database.AddAssessment(pa2);
            _database.AddAssessment(oa2);

            // Create assessments for course3
            Assessment pa3 = new Assessment
            {
                CourseId = course3.Id,
                Name = "Performance Assessment 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[0]
            };
            Assessment oa3 = new Assessment
            {
                CourseId = course3.Id,
                Name = "Objective Assessment 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[1]
            };
            _database.AddAssessment(pa3);
            _database.AddAssessment(oa3);

            // Create assessments for course4
            Assessment pa4 = new Assessment
            {
                CourseId = course4.Id,
                Name = "Performance Assessment 2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[0]
            };
            Assessment oa4 = new Assessment
            {
                CourseId = course4.Id,
                Name = "Objective Assessment 2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[1]
            };
            _database.AddAssessment(pa4);
            _database.AddAssessment(oa4);

            // Create assessments for course5
            Assessment pa5 = new Assessment
            {
                CourseId = course5.Id,
                Name = "Performance Assessment 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[0]
            };
            Assessment oa5 = new Assessment
            {
                CourseId = course5.Id,
                Name = "Objective Assessment 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[1]
            };
            _database.AddAssessment(pa5);
            _database.AddAssessment(oa5);

            // Create assessments for course6
            Assessment pa6 = new Assessment
            {
                CourseId = course6.Id,
                Name = "Performance Assessment 2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[0]
            };
            Assessment oa6 = new Assessment
            {
                CourseId = course6.Id,
                Name = "Objective Assessment 2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[1]
            };
            _database.AddAssessment(pa6);
            _database.AddAssessment(oa6);

            // Create assessments for course7
            Assessment pa7 = new Assessment
            {
                CourseId = course7.Id,
                Name = "Performance Assessment 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[0]
            };
            Assessment oa7 = new Assessment
            {
                CourseId = course6.Id,
                Name = "Objective Assessment 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[1]
            };
            _database.AddAssessment(pa7);
            _database.AddAssessment(oa7);

            // Create assessments for course8
            Assessment pa8 = new Assessment
            {
                CourseId = course8.Id,
                Name = "Performance Assessment 2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[0]
            };
            Assessment oa8 = new Assessment
            {
                CourseId = course8.Id,
                Name = "Objective Assessment 2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[1]
            };
            _database.AddAssessment(pa8);
            _database.AddAssessment(oa8);

            // Create assessments for course9
            Assessment pa9 = new Assessment
            {
                CourseId = course9.Id,
                Name = "Performance Assessment 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[0]
            };
            Assessment oa9 = new Assessment
            {
                CourseId = course9.Id,
                Name = "Objective Assessment 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[1]
            };
            _database.AddAssessment(pa9);
            _database.AddAssessment(oa9);

            // Create assessments for course10
            Assessment pa10 = new Assessment
            {
                CourseId = course10.Id,
                Name = "Performance Assessment 2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[0]
            };
            Assessment oa10 = new Assessment
            {
                CourseId = course10.Id,
                Name = "Objective Assessment 2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[1]
            };
            _database.AddAssessment(pa10);
            _database.AddAssessment(oa10);

            // Create assessments for course11
            Assessment pa11 = new Assessment
            {
                CourseId = course11.Id,
                Name = "Performance Assessment 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[0]
            };
            Assessment oa11 = new Assessment
            {
                CourseId = course11.Id,
                Name = "Objective Assessment 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[1]
            };
            _database.AddAssessment(pa11);
            _database.AddAssessment(oa11);

            // Create assessments for course12
            Assessment pa12 = new Assessment
            {
                CourseId = course12.Id,
                Name = "Performance Assessment 2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[0]
            };
            Assessment oa12 = new Assessment
            {
                CourseId = course12.Id,
                Name = "Objective Assessment 2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                StartDateNotificationEnabled = false,
                EndDateNotificationEnabled = true,
                Type = TypeOptions[1]
            };
            _database.AddAssessment(pa12);
            _database.AddAssessment(oa12);

        }


        private void OnAddTermClicked(object sender, EventArgs e)
        {
            // Create a new term
            var newTerm = new Term { Title = "New Term", StartDate = DateTime.Today, EndDate = DateTime.Today.AddMonths(1) };
            _database.AddTerm(newTerm);
            Terms.Add(newTerm);
        }

        private void ViewTermClicked(object sender, EventArgs e)
        {
            var term = (sender as Button)?.CommandParameter as Term;
            if (term != null)
            {
                // Navigate to term details 
                Navigation.PushAsync(new TermDetailsPage(term, _database));
            }
        }

        private void DeleteTermClicked(object sender, EventArgs e)
        {
            var term = (sender as Button)?.CommandParameter as Term;
            if (term != null)
            {
                _database.DeleteTerm(term);
                Terms.Remove(term);
            }
        }

        private void TermTitleChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            var term = entry.BindingContext as Term;

            if (term != null && e.NewTextValue != null)
            {
                term.Title = e.NewTextValue;
                _database.UpdateTerm(term);
            }
        }

        private void TermStartDateChanged(object sender, DateChangedEventArgs e)
        {
            var datePicker = sender as DatePicker;
            var term = datePicker?.BindingContext as Term;

            if (term != null)
            {
                term.StartDate = e.NewDate;
                _database.UpdateTerm(term);
            }
        }

        private void TermEndDateChanged(object sender, DateChangedEventArgs e)
        {
            var datePicker = sender as DatePicker;
            var term = datePicker?.BindingContext as Term;

            if (term != null)
            {
                term.EndDate = e.NewDate;
                _database.UpdateTerm(term);
            }
        }
        private async void OnGenerateReportClicked(object sender, EventArgs e)
        {
            // Create a StringBuilder to build the report content
            var reportContent = new System.Text.StringBuilder();

            // Add report title
            reportContent.AppendLine("Term and Course Report");
            reportContent.AppendLine();

            // Add each term and its associated courses to the report
            foreach (var term in Terms)
            {
                reportContent.AppendLine($"Term: {term.Title}");
                reportContent.AppendLine($"Start Date: {term.StartDate.ToShortDateString()} End Date: {term.EndDate.ToShortDateString()}");
                reportContent.AppendLine("Courses:");
                var courses = _database.GetCoursesByTermId(term.Id);
                foreach (var course in courses)
                {
                    reportContent.AppendLine($"- {course.Title} - {course.StartDate.ToShortDateString()} to {course.EndDate.ToShortDateString()}");
                }
                reportContent.AppendLine();
            }

            // Display the report using a DisplayAlert
            await DisplayAlert("Term and Course Report", reportContent.ToString(), "OK");
        }
    }

}
