using MyNotes.Models;

using System.Linq;

namespace MyNotes.ViewModels
{
    public class NoteDetailViewModel : BaseViewModel
    {
        public string[] Courses { get; set; }
        public Note Note { get; set; }

        public bool EditMode { get; }

        private readonly Note oldNote;

        public string NoteCourse
        {
            get
            {
                return Note.Course;
            }
            set
            {
                Note.Course = value;
                OnPropertyChanged();
            }
        }

        public string NoteHeading
        {
            get
            {
                return Note.Heading;
            }
            set
            {
                Note.Heading = value;
                OnPropertyChanged();
            }
        }

        public string NoteText
        {
            get
            {
                return Note.Text;
            }
            set
            {
                Note.Text = value;
                OnPropertyChanged();
            }
        }

        public NoteDetailViewModel(Note note = null)
        {
            Title = note == null ? Constants.Pages.ADD_NOTE : Constants.Pages.EDIT_NOTE;

            EditMode = note != null;

            InitializeCourses();

            Note = note ?? new Note();

            oldNote = new Note()
            {
                Id = Note.Id,
                Course = Note.Course,
                Heading = Note.Heading,
                Text = Note.Text
            };
        }

        async void InitializeCourses()
        {
            Courses = (await DataStore.GetCoursesAsync()).ToArray();
        }

        public bool HasNotChanged()
        {
            return Note.Equals(oldNote);
        }


    }
}
