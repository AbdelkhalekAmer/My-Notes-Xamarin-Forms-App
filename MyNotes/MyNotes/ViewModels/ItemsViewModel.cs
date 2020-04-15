using MyNotes.Models;
using MyNotes.Views;

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MyNotes.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Note> Notes { get; set; }
        public Command LoadNotesCommand { get; set; }

        public ItemsViewModel()
        {
            Title = Constants.Pages.NOTES;
            Notes = new ObservableCollection<Note>();
            LoadNotesCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<ItemDetailPage, Note>(this, Constants.SAVE_NOTE_MESSAGE, async (sender, note) =>
            {
                Notes.Add(note);
                await DataStore.AddItemAsync(note);
            });


            MessagingCenter.Subscribe<ItemDetailPage, Note>(this, Constants.EDIT_NOTE_MESSAGE, async (sender, note) =>
            {
                await DataStore.UpdateItemAsync(note);
            });

        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Notes.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Notes.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}