using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using c971_oliver.Models;
using Plugin.LocalNotification;

namespace c971_oliver
{
    public static class DataFunctions
    {
        public static ObservableCollection<Term> Terms = new ObservableCollection<Term>();
        public static ObservableCollection<Course> Courses = new ObservableCollection<Course>();
        public static ObservableCollection<Assessment> Assessments = new ObservableCollection<Assessment>();

        public static void StartupNotifications()
        {
            DateTime today = DateTime.Today;
            var database = new Database();
            database.Initialize();
            List<Course> courses = database.GetAllCourses();
            List<Assessment> assessments = database.GetAllAssessments();

            courses.ForEach(course =>
            {
                if (course.StartDateNotificationEnabled && course.StartDate.Date == today)
                {
                    ShowLocalNotification("Course Start", $"{course.Title} is starting today");
                }
                if (course.EndDateNotificationEnabled && course.EndDate.Date == today)
                {
                    ShowLocalNotification("Course End", $"{course.Title} is ending today");
                }
            });

            assessments.ForEach(assessment =>
            {
                if (assessment.StartDateNotificationEnabled && assessment.StartDate.Date == today)
                {
                    ShowLocalNotification("Assessment Start", $"{assessment.Name} is starting today");
                }
                if (assessment.EndDateNotificationEnabled && assessment.EndDate.Date == today)
                {
                    ShowLocalNotification("Assessment End", $"{assessment.Name} is ending today");
                }

            });
            database.Close();
        }

        private static void ShowLocalNotification(string title, string message)
        {

            MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Launcher.TryOpenAsync("app-notifications://");
            });
        }

        public static void InitializeTermsList()
        {
            var database = new Database();
            database.Initialize();
            List<Term> terms = database.GetAllTerms();
            terms.ForEach(term => Terms.Add(term));
            database.Close();
        }

        public static void AddTermToTermList(Term term)
        {
            var database = new Database();
            database.Initialize();
            database.AddTerm(term);
            Terms.Add(term);
            database.Close();
        }

        public static void UpdateTermInTermList(Term oldTerm, Term newTerm)
        {
            List<Term> termList = Terms.ToList();
            Terms.Clear();

            var database = new Database();
            database.Initialize();
            database.UpdateTerm(newTerm);

            int indexFound = termList.IndexOf(oldTerm);
            termList.RemoveAt(indexFound);
            termList.Insert(indexFound, newTerm);
            termList.ForEach(term => Terms.Add(term));

            database.Close();
        }

        public static void DeleteTermFromTermList(Term term)
        {
            var database = new Database();
            database.Initialize();
            database.DeleteTerm(term);
            Terms.Remove(term);
            database.Close();
        }

        public static void InitializeCoursesList(int termId)
        {
            Courses.Clear();
            var database = new Database();
            database.Initialize();
            List<Course> courses = database.GetCoursesByTermId(termId);
            courses.ForEach(course => Courses.Add(course));
            database.Close();
        }

        public static void AddCourseToCourseList(Course course)
        {
            var database = new Database();
            database.Initialize();
            database.AddCourse(course);
            Courses.Add(course);
            database.Close();
        }

        public static void UpdateCourseInCourseList(Course oldCourse, Course newCourse)
        {
            List<Course> courseList = Courses.ToList();
            Courses.Clear();

            var database = new Database();
            database.Initialize();
            database.UpdateCourse(newCourse);

            int indexFound = courseList.IndexOf(oldCourse);
            courseList.RemoveAt(indexFound);
            courseList.Insert(indexFound, newCourse);
            courseList.ForEach(course => Courses.Add(course));

            database.Close();
        }

        public static void DeleteCourseFromCourseList(Course course)
        {
            var database = new Database();
            database.Initialize();
            database.DeleteCourse(course);
            Courses.Remove(course);
            database.Close();
        }

        public static void InitializeAssessmentList(int courseId)
        {
            Assessments.Clear();
            var database = new Database();
            database.Initialize();
            List<Assessment> assessments = database.GetAssessmentsByCourseId(courseId);
            assessments.ForEach(assessment => Assessments.Add(assessment));
            database.Close();
        }

        public static void AddAssessmentToAssessmentList(Assessment assessment)
        {
            var database = new Database();
            database.Initialize();
            database.AddAssessment(assessment);
            Assessments.Add(assessment);
            database.Close();
        }

        public static void UpdateAssessment(Assessment assessment)
        {
            var database = new Database();
            database.Initialize();
            database.UpdateAssessment(assessment);
            database.Close();

            var existingAssessment = Assessments.FirstOrDefault(a => a.Id == assessment.Id);
            if (existingAssessment != null)
            {
                int index = Assessments.IndexOf(existingAssessment);
                Assessments[index] = assessment;
            }
        }


        public static void DeleteAssessmentFromAssessmentList(Assessment assessment)
        {
            var database = new Database();
            database.Initialize();
            database.DeleteAssessment(assessment);
            Assessments.Remove(assessment);
            database.Close();
        }


    }

}