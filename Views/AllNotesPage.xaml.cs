namespace MS_MauiApp.Views;

public partial class AllNotesPage : ContentPage
{
	public AllNotesPage()
	{
		InitializeComponent();
        BindingContext = new Models.MS_AllNotes();
    }

    protected override void OnAppearing()
    {
        ((Models.MS_AllNotes)BindingContext).LoadNotes();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MS_NotePage));
    }

    private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the note model
            var note = (Models.MS_Note)e.CurrentSelection[0];

            // Should navigate to "NotePage?ItemId=path\on\device\XYZ.notes.txt"
            await Shell.Current.GoToAsync($"{nameof(MS_NotePage)}?{nameof(MS_NotePage.ItemId)}={note.Filename}");

            // Unselect the UI
            notesCollection.SelectedItem = null;
        }
    }
}
	