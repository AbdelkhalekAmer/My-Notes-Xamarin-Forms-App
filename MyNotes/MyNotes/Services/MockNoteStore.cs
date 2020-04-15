using MyNotes.Models;
using MyNotes.Services.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Services
{
    public class MockNoteStore : INoteStore
    {
        private static readonly List<string> mockCourses;
        private static readonly List<Note> mockNotes;
        private static int nextNoteId;

        static MockNoteStore()
        {
            mockCourses = new List<string>
            {
                "Introduction to Xamarin.Forms",
                "Android Apps with Kotlin: First App",
                "Managing Android App Data with SQLite"
            };

            mockNotes = new List<Note>
            {
                new Note { Id="0", Heading="UI Code",
                    Text="Xamarin.Forms allows UI code to be shared", Course=mockCourses[0] },
                new Note { Id="1", Heading="Sharing Code",
                    Text="With Xamarin.Forms code can be shared across iOS, Android, and UWP", Course=mockCourses[0] },
                new Note { Id="2", Heading="Cross-platform Database",
                    Text="SQLite is a great storage solution for both Android and iOS", Course=mockCourses[2] },
                new Note { Id="3", Heading="Using Kotlin",
                    Text="For those prefering Java-like languages, Kotlin is a great choice for Android", Course=mockCourses[1] },
                new Note { Id="4", Heading="Data Binding",
                    Text="Data binding simplifies connecting app data to the UI", Course=mockCourses[0] },
            };

            nextNoteId = mockNotes.Count;

        }

        public MockNoteStore()
        {

        }

        public async Task<string> AddItemAsync(Note courseNote)
        {
            lock (this)
            {
                courseNote.Id = nextNoteId.ToString();
                mockNotes.Add(courseNote);
                nextNoteId++;
            }
            return await Task.FromResult(courseNote.Id);
        }

        public async Task<bool> UpdateItemAsync(Note courseNote)
        {
            var noteIndex = mockNotes.FindIndex((Note arg) => arg.Id == courseNote.Id);
            var noteFound = noteIndex != -1;
            if (noteFound)
            {
                mockNotes[noteIndex].Heading = courseNote.Heading;
                mockNotes[noteIndex].Text = courseNote.Text;
                mockNotes[noteIndex].Course = courseNote.Course;
            }
            return await Task.FromResult(noteFound);
        }

        public async Task<Note> GetItemAsync(string id)
        {
            var note = mockNotes.FirstOrDefault(courseNote => courseNote.Id == id);

            // Make a copy of the note to simulate reading from an external datastore
            var returnNote = await CopyNote(note);
            return returnNote;
        }

        public async Task<IEnumerable<Note>> GetItemsAsync(bool forceRefresh = false)
        {
            // Make a copy of the notes to simulate reading from an external datastore
            var returnNotes = new List<Note>();
            foreach (var note in mockNotes)
                returnNotes.Add(await CopyNote(note));
            return await Task.FromResult(returnNotes);
        }

        public async Task<IEnumerable<String>> GetCoursesAsync()
        {
            return await Task.FromResult(mockCourses);
        }

        private static async Task<Note> CopyNote(Note note)
        {
            return await Task.Run(() =>
            {
                Task.Delay(250);
                return new Note { Id = note.Id, Heading = note.Heading, Text = note.Text, Course = note.Course };
            });
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
