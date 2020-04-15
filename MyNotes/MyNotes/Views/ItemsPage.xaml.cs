using MyNotes.Models;
using MyNotes.ViewModels;

using System;
using System.ComponentModel;

using Xamarin.Forms;

namespace MyNotes.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ItemsPage(ItemsViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;

            BindingContext = _viewModel;
        }

        public ItemsPage()
        {
            InitializeComponent();

            _viewModel = new ItemsViewModel();

            BindingContext = _viewModel;
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var note = (Note)layout.BindingContext;
            await Navigation.PushModalAsync(new NavigationPage(new ItemDetailPage(new NoteDetailViewModel(note))));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ItemDetailPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Notes.Count == 0)
                _viewModel.IsBusy = true;
        }
    }
}