using MyNotes.Services;
using MyNotes.Services.Contracts;
using MyNotes.ViewModels;
using MyNotes.Views;

using Xamarin.Forms;

namespace MyNotes
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<INoteStore, MockNoteStore>();

            MainPage = new MainPage();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
