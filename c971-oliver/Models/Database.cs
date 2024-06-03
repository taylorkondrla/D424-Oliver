using System;
using System.Collections.Generic;
using System.IO;
using c971_oliver.Models;
using SQLite;
using static Android.Provider.ContactsContract.CommonDataKinds;

namespace c971_oliver.Models
{
    public class Database
    {
        private SQLiteConnection database;

        public bool Initialize()
        {
            bool dbTablesCreated = false;
            if (database == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "C971db.db");
                database = new SQLiteConnection(dbPath);
            }

            CreateTableResult createTerm = database.CreateTable<Term>();
            CreateTableResult createCourse = database.CreateTable<Course>();
            CreateTableResult createAssessment = database.CreateTable<Assessment>();
            CreateTableResult createNote = database.CreateTable<Note>();

            dbTablesCreated = createTerm == CreateTableResult.Created || createCourse == CreateTableResult.Created || createAssessment == CreateTableResult.Created || createNote == CreateTableResult.Created;

            return dbTablesCreated;
        }

        public void AddTerm(Term term)
        {
            database.Insert(term);
        }

        public List<Term> GetAllTerms()
        {
            return database.Table<Term>().ToList();
        }

        public List<Course> GetAllCourses()
        {
            return database.Table<Course>().ToList();
        }

        public List<Assessment> GetAllAssessments()
        {
            return database.Table<Assessment>().ToList();
        }

        public int UpdateTerm(Term term)
        {
            return database.Update(term);
        }

        public int DeleteTerm(Term term)
        {
            return database.Delete(term);
        }

        public void AddCourse(Course course)
        {
            database.Insert(course);
        }

        public List<Course> GetCoursesByTermId(int termId)
        {
            string query = $"SELECT * FROM Course WHERE TermId={termId}";
            return database.Query<Course>(query);
        }

        public int UpdateCourse(Course course)
        {
            return database.Update(course);
        }

        public int DeleteCourse(Course course)
        {
            return database.Delete(course);
        }

        public void AddAssessment(Assessment assessment)
        {
            database.Insert(assessment);
        }

        public List<Assessment> GetAssessmentsByCourseId(int courseId)
        {
            string query = $"SELECT * FROM Assessment WHERE CourseId={courseId}";
            return database.Query<Assessment>(query);
        }

        public int UpdateAssessment(Assessment assessment)
        {
            return database.Update(assessment);
        }

        public int DeleteAssessment(Assessment assessment)
        {
            return database.Delete(assessment);
        }

        public void AddNote(Note note)
        {
            database.Insert(note);
        }

        public List<Note> GetNotesByCourseId(int courseId)
        {
            string query = $"SELECT * FROM Note WHERE CourseId={courseId}";
            return database.Query<Note>(query);
        }

        public int UpdateNote(Note note)
        {
            return database.Update(note);
        }

        public int DeleteNote(Note note)
        {
            return database.Delete(note);
        }

        public void ClearDatabase()
        {
            database.DeleteAll<Term>();
            database.DeleteAll<Course>();
            database.DeleteAll<Assessment>();
            database.DeleteAll<Note>();
        }

        public void Close()
        {
            database.Close();
        }
    }
}