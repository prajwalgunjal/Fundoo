using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface INoteBusiness
    {
        public NoteEntity TakeANote(TakeANoteModel takeANoteModel, int userID);
        public List<NoteEntity> GetAll(string emailid);
        public NoteEntity EditANote(TakeANoteModel takeANoteModel,int userID, int noteId);
        public string Delete_A_note(int userID);
        public bool isPin(int noteId);
        public bool TrashNotes(int noteId);
        public bool IsArchive(int noteId);
    }
}