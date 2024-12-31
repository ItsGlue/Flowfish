using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteSaver : MonoBehaviour
{
    [System.Serializable]
    public class Note
    {
        public DateTime date;
        public string content;
    }

    [System.Serializable]
    public class NotesWrapper
    {
        public List<Note> notes = new List<Note>();
    }

    public TMP_InputField inputField; // Use TMP_InputField instead of InputField
    public DateTime selectedDate;
    private NotesWrapper notesWrapper;
    private string filePath;

    void Start()
    {
        selectedDate = DateManager.Instance.getSelectedDate();
        filePath = Path.Combine(Application.persistentDataPath, "notes.json");
        LoadNotes();
        LoadNoteForSelectedDate();
    }

    // Function to save the note and change to "Calendar"
    public void SaveNoteAndChangeScene()
    {
        string noteContent = inputField.text;

        if (!string.IsNullOrEmpty(noteContent))
        {
            Note existingNote = notesWrapper.notes.Find(note => note.date.Date == selectedDate.Date);

            if (existingNote != null)
            {
                existingNote.content = noteContent; // Update existing note
            }
            else
            {
                Note newNote = new Note { date = selectedDate, content = noteContent };
                notesWrapper.notes.Add(newNote); // Add new note
            }

            SaveNotes();
        }

        SceneManager.LoadScene("Calendar"); // Change to "Calendar" scene
    }
    // Fricky cheek poking
    // Function to save notes to JSON
    private void SaveNotes()
    {
        Debug.Log("Saving...");
        string json = JsonUtility.ToJson(notesWrapper);
        File.WriteAllText(filePath, json);
        Debug.Log("Saved: " + json);
    }

    // Function to load notes from JSON
    private void LoadNotes()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            notesWrapper = JsonUtility.FromJson<NotesWrapper>(json);
            Debug.Log("Loading: " + json);
        }
        else
        {
            notesWrapper = new NotesWrapper();
        }
    }

    // Function to load the note for the selected date into the input field
    private void LoadNoteForSelectedDate()
    {
        Debug.Log("Attempting to load note for date: " + selectedDate);
        Note existingNote = notesWrapper.notes.Find(note => note.date.Date == selectedDate.Date);

        if (existingNote != null)
        {
            inputField.text = existingNote.content; // Set input field text to the note content
            Debug.Log("Loaded note for selected date: " + existingNote.content);
        }
        else
        {
            inputField.text = string.Empty; // Clear input field if no note exists
            Debug.Log("No note found for selected date.");
        }
    }
}
