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
    }
}
