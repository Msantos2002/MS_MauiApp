namespace MS_MauiApp.Views;
[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class MS_NotePage : ContentPage
{

    string _fileName = Path.Combine(FileSystem.AppDataDirectory, "MS_MauiApp.txt");
    public MS_NotePage()
	{

    InitializeComponent();
        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

        LoadNote(Path.Combine(appDataPath, randomFileName));

        if (File.Exists(_fileName))
            TextEditor.Text = File.ReadAllText(_fileName);
    }
    private async void SaveButton_Clicked(object sender, EventArgs e)
{
    if (BindingContext is Models.MS_Note note)
        File.WriteAllText(note.Filename, TextEditor.Text);

    await Shell.Current.GoToAsync("..");
}

private async void DeleteButton_Clicked(object sender, EventArgs e)
{
    if (BindingContext is Models.MS_Note note)
    {
        // Delete the file.
        if (File.Exists(note.Filename))
            File.Delete(note.Filename);
    }

    await Shell.Current.GoToAsync("..");
}
private void LoadNote(string fileName)
    {
        Models.MS_Note noteModel = new Models.MS_Note();
        noteModel.Filename = fileName;

        if (File.Exists(fileName))
        {
            noteModel.Date = File.GetCreationTime(fileName);
            noteModel.Text = File.ReadAllText(fileName);
        }

        BindingContext = noteModel;
    }
    public string ItemId
    {
        set { LoadNote(value); }
    }
}