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

    }
}
