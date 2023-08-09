using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRepo
    {
        public NoteEntity TakeANote(TakeANoteModel takeANoteModel, int userID);
        public List<NoteEntity> GetAll(int userID);
        public NoteEntity EditANote(TakeANoteModel takeANoteModel,int userID, int noteId);
        public NoteEntity Delete_A_note(int userID, int noteID);
        public bool isPin(int noteId,int userID);
        public bool TrashNotes(int noteId,int userID);
        public bool IsArchive(int noteId,int userID);
        public string ChangeColour(int noteId, string colour, int userID);

        public DateTime SetReminder(int noteId, DateTime dateTime,int userID);

        public string UploadImage(string filePath, long notesId, long userId);

    }
}
