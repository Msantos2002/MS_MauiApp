using CommunityToolkit.Mvvm.Input;
using MS_MauiApp.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace MS_MauiApp.ViewModels;

internal class MSNotesViewModel : IQueryAttributable
{
    public ObservableCollection<ViewModels.MSNoteViewModel> AllNotes { get; }
    public ICommand NewCommand { get; }
    public ICommand SelectNoteCommand { get; }
    public MSNotesViewModel()
    {
        AllNotes = new ObservableCollection<ViewModels.MSNoteViewModel>(Models.MS_Note.LoadAll().Select(n => new MSNoteViewModel(n)));
        NewCommand = new AsyncRelayCommand(NewNoteAsync);
        SelectNoteCommand = new AsyncRelayCommand<ViewModels.MSNoteViewModel>(SelectNoteAsync);
    }
    private async Task NewNoteAsync()
    {
        await Shell.Current.GoToAsync(nameof(Views.MS_NotePage));
    }

    private async Task SelectNoteAsync(ViewModels.MSNoteViewModel note)
    {
        if (note != null)
            await Shell.Current.GoToAsync($"{nameof(Views.MS_NotePage)}?load={note.Identifier}");
    }
    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted"))
        {
            string noteId = query["deleted"].ToString();
            MSNoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

            // If note exists, delete it
            if (matchedNote != null)
                AllNotes.Remove(matchedNote);
        }
        else if (query.ContainsKey("saved"))
        {
            string noteId = query["saved"].ToString();
            MSNoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

            // If note is found, update it
            if (matchedNote != null)
            {
                matchedNote.Reload();
                AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
            }

            // If note isn't found, it's new; add it.
            else
                AllNotes.Insert(0, new MSNoteViewModel(Models.MS_Note.Load(noteId)));
        }
    }
}
