using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using c971_oliver.Models;
using Microsoft.Maui.Controls.Compatibility;

namespace c971_oliver
{
    public partial class TermDetailsPage : ContentPage
    {
        public Term Term { get; set; }
        public ObservableCollection<Course> Courses { get; set; }
        private Database _database;

        public TermDetailsPage(Term term, Database database)
        {
            InitializeComponent();
            _database = database;
            Term = term;
            Courses = new ObservableCollection<Course>(_database.GetCoursesByTermId(term.Id)); // Initialize courses from database
            BindingContext = this;
        }

        private void OnAddCourseClicked(object sender, EventArgs e)
        {
            // Create a new course and add it to the collection
            var newCourse = new Course { TermId = Term.Id };
            Courses.Add(newCourse);
            DataFunctions.AddCourseToCourseList(newCourse);

            // Refresh the UI
            InitializeComponent();
        }

        private void ViewCourseClicked(object sender, EventArgs e)
        {
            var course = (sender as Button)?.CommandParameter as Course;
            // Navigate to course details 
            Navigation.PushAsync(new CourseDetailsPage(course, _database));
        }

        private void DeleteCourseClicked(object sender, EventArgs e)
        {
            var course = (sender as Button)?.CommandParameter as Course;
            if (course != null)
            {
                _database.DeleteCourse(course);
                Courses.Remove(course);
            }
        }
    }
}