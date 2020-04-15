using MyNotes.ViewModels;

using System;
using System.ComponentModel;

using Xamarin.Forms;

namespace MyNotes.Views
{
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        NoteDetailViewModel _viewModel;
        public ItemDetailPage(NoteDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new NoteDetailViewModel();
        }

        public void OnSave(object sender, EventArgs eventArgs)
        {
            var message = _viewModel.EditMode ? Constants.EDIT_NOTE_MESSAGE : Constants.SAVE_NOTE_MESSAGE;
            MessagingCenter.Send(this, message, _viewModel.Note);
            Navigation.PopModalAsync();
        }

        public async void OnCancel(object sender, EventArgs eventArgs)
        {
            if (_viewModel.HasNotChanged())
            {
                await Navigation.PopModalAsync();
            }

            if (await DisplayAlert(Constants.CANCEL_POPUP_TITLE, Constants.CANCEL_POPUP_DESCRIPTION, Constants.YES_MESSAGE, Constants.NO_MESSAGE))
            {
                await Navigation.PopModalAsync();
            }

        }

    }
}