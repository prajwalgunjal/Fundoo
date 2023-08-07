using BusinessLayer.Interfaces;
using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class NoteBusiness : INoteBusiness
    {
        public INoteRepo noteRepo;
        public NoteBusiness(INoteRepo noteRepo)
        {
            this.noteRepo = noteRepo;
        }
        public NoteEntity TakeANote(TakeANoteModel takeANoteModel, int userID)
        {
            return noteRepo.TakeANote(takeANoteModel, userID);
        }
        /* public List<NoteEntity> DisplayNote(int userID)
         {
             return noteRepo.DisplayNote(userID);
         }*/
        public List<NoteEntity> GetAll(string emailid)
        {
            return noteRepo.GetAll(emailid);
        }
        public NoteEntity EditANote(TakeANoteModel takeANoteModel, int userID, int noteId)
        {
            return noteRepo.EditANote(takeANoteModel,userID,noteId);
        }
        public string Delete_A_note(int userID)
        {
            return noteRepo.Delete_A_note(userID);
        }
        public bool isPin(int noteId)
        {
            return noteRepo.isPin(noteId);
        }
        public bool TrashNotes(int noteId)
        {
            return noteRepo.TrashNotes(noteId);
        }
        public bool IsArchive(int noteId)
        {
            return noteRepo.IsArchive(noteId);
        }
    }
}
