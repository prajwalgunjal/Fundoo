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
        public List<NoteEntity> GetAll(int userID)
        {
            return noteRepo.GetAll(userID);
        }
        public NoteEntity EditANote(TakeANoteModel takeANoteModel, int userID, int noteId)
        {
            return noteRepo.EditANote(takeANoteModel,userID,noteId);
        }
        public NoteEntity Delete_A_note(int userID, int noteID)
        {
            return noteRepo.Delete_A_note(userID,noteID);
        }
        public bool isPin(int noteId, int userID)
        {
            return noteRepo.isPin(noteId,userID);
        }
        public bool TrashNotes(int noteId, int userID)
        {
            return noteRepo.TrashNotes(noteId,userID);
        }
        public bool IsArchive(int noteId , int userID)
        {
            return noteRepo.IsArchive(noteId ,userID);
        }
        public string ChangeColour(int noteId, string colour, int userID)
        {
            return noteRepo.ChangeColour(noteId, colour, userID);
        }
        public DateTime SetReminder(int noteId, DateTime dateTime, int userID)
        {
            return noteRepo.SetReminder(noteId,dateTime, userID);
        }
        public string UploadImage(string filePath, long notesId, long userId)
        {
            return noteRepo.UploadImage(filePath, notesId, userId);

        }
        public List<NoteEntity> Get_All_Notes_Without_Login()
        {
            return noteRepo.Get_All_Notes_Without_Login();
        }

        public bool SearchNote(string notetitle, int userId)
        {
            return noteRepo.SearchNote(notetitle, userId);
        }
    }
}
