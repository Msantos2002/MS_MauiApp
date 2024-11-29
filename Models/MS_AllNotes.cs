using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MS_MauiApp.Models
{
    internal class MS_AllNotes
    {
        public ObservableCollection<MS_Note> Notes { get; set; } = new ObservableCollection<MS_Note>();

        public MS_AllNotes() =>
            LoadNotes();

        public void LoadNotes()
        {
            Notes.Clear();

            // Get the folder where the notes are stored.
            string appDataPath = FileSystem.AppDataDirectory;

            // Use Linq extensions to load the *.notes.txt files.
            IEnumerable<MS_Note> notes = Directory

                                        // Select the file names from the directory
                                        .EnumerateFiles(appDataPath, "*.notes.txt")

                                        // Each file name is used to create a new Note
                                        .Select(filename => new MS_Note()
                                        {
                                            Filename = filename,
                                            Text = File.ReadAllText(filename),
                                            Date = File.GetLastWriteTime(filename)
                                        })

                                        // With the final collection of notes, order them by date
                                        .OrderBy(note => note.Date);

            // Add each note into the ObservableCollection
            foreach (MS_Note note in notes)
                Notes.Add(note);
        }
    } 
}
