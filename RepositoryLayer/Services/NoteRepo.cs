using CommonLayer.RequestModels;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NoteRepo : INoteRepo
    {
        private Fundoo_Context fundoo_Context_Note;
        private readonly IConfiguration configuration;
        public NoteRepo(Fundoo_Context fundoo_Context, IConfiguration configuration)
        {
            this.fundoo_Context_Note = fundoo_Context;
            this.configuration = configuration;
        }
        public NoteEntity TakeANote(TakeANoteModel takeANoteModel, int userID)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.UserId = userID;
                noteEntity.title = takeANoteModel.title;
                noteEntity.takeANote = takeANoteModel.takeANote;
                takeANoteModel.createdAt = DateTime.Now;
                takeANoteModel.updatedAt = DateTime.Now;
                noteEntity.colour = takeANoteModel.colour;
                noteEntity.pin = takeANoteModel.pin;
                noteEntity.archive = takeANoteModel.archive;
                noteEntity.image = takeANoteModel.image;
                noteEntity.reminder = takeANoteModel.reminder;
                fundoo_Context_Note.Notes.Add(noteEntity);
                fundoo_Context_Note.SaveChanges();
                return noteEntity;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public NoteEntity EditANote(TakeANoteModel takeANoteModel, int userID,int noteId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                bool idExist = fundoo_Context_Note.Notes.Any(x => x.UserId == userID);
                bool noteIDExist = fundoo_Context_Note.Notes.Any(x => x.noteID == noteId);
                noteEntity = fundoo_Context_Note.Notes.Where(x => x.UserId == userID && x.noteID == noteId).FirstOrDefault();
                if (idExist && noteIDExist)
                {
                    /*noteEntity.UserId = userID;
                    noteEntity.title = fundoo_Context_Note.Notes.Where(x => x.UserId == userID).Select(x => x.title).FirstOrDefault();
                    noteEntity.takeANote = fundoo_Context_Note.Notes.Where(x => x.UserId == userID).Select(x => x.takeANote).FirstOrDefault();
                    noteEntity.updatedAt = DateTime.Now;
                    noteEntity.colour = fundoo_Context_Note.Notes.Where(x => x.UserId == userID).Select(x => x.colour).FirstOrDefault();
                    noteEntity.pin = fundoo_Context_Note.Notes.Where(x => x.UserId == userID).Select(x => x.pin).FirstOrDefault();
                    noteEntity.archive = fundoo_Context_Note.Notes.Where(x => x.UserId == userID).Select(x => x.archive).FirstOrDefault();
                    noteEntity.image = fundoo_Context_Note.Notes.Where(x => x.UserId == userID).Select(x => x.image).FirstOrDefault();
                    noteEntity.reminder = fundoo_Context_Note.Notes.Where(x => x.UserId == userID).Select(x => x.reminder).FirstOrDefault();
                    */
                    noteEntity.UserId = userID;
                    noteEntity.title = takeANoteModel.title;
                    noteEntity.takeANote = takeANoteModel.takeANote;
                    takeANoteModel.updatedAt = DateTime.Now;
                    noteEntity.colour = takeANoteModel.colour;
                    noteEntity.pin = takeANoteModel.pin;
                    noteEntity.archive = takeANoteModel.archive;
                    noteEntity.image = takeANoteModel.image;
                    noteEntity.reminder = takeANoteModel.reminder;
                    fundoo_Context_Note.SaveChanges();
                    return noteEntity;
                }
                else { return null; }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public string ChangeColour(int noteId, string colour)
        {
            try
            {
                bool idExist = fundoo_Context_Note.Notes.Any(x => x.noteID == noteId);
                if (idExist)
                {
                    NoteEntity noteEntity = fundoo_Context_Note.Notes.FirstOrDefault(x => x.noteID == noteId);
                    noteEntity.colour = colour;
                    fundoo_Context_Note.SaveChanges();
                    return "colour changed";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public DateTime SetReminder(int noteId,DateTime dateTime)
        {
                bool idExist = fundoo_Context_Note.Notes.Any(x => x.noteID == noteId);
                NoteEntity noteEntity = fundoo_Context_Note.Notes.FirstOrDefault(x => x.noteID == noteId);

                if (idExist)
                {
                    noteEntity.reminder =dateTime;
                    fundoo_Context_Note.SaveChanges();
                    return noteEntity.reminder;
                }
                else
                {
                    noteEntity.reminder = DateTime.Now;
                    fundoo_Context_Note.SaveChanges();
                    return noteEntity.reminder;
                }
            
        }


        public bool isPin(int noteId)
        {
            try
            {
                NoteEntity noteEntity = this.fundoo_Context_Note.Notes.FirstOrDefault(x => x.noteID == noteId);
                if (noteEntity.pin == false)
                {
                    noteEntity.pin = true;
                    noteEntity.unPin = false;
                    fundoo_Context_Note.SaveChanges();
                    return noteEntity.pin;
                }
                else
                {
                    noteEntity.pin = false;
                    noteEntity.unPin = true;
                    fundoo_Context_Note.SaveChanges();
                    return noteEntity.unPin;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public bool TrashNotes (int noteId)
        {
            try
            {
                NoteEntity noteEntity = this.fundoo_Context_Note.Notes.FirstOrDefault(x => x.noteID == noteId);
                if (noteEntity.trash == false)
                {
                    noteEntity.trash = true;
                    fundoo_Context_Note.SaveChanges();
                    return noteEntity.trash;
                }
                else
                {
                    noteEntity.trash = false;
                    fundoo_Context_Note.SaveChanges();
                    return noteEntity.unPin;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool IsArchive(int noteId)
        {
            try
            {
                NoteEntity noteEntity = this.fundoo_Context_Note.Notes.FirstOrDefault(x => x.noteID == noteId);
                if (noteEntity.archive == false)
                {
                    noteEntity.archive = true;
                    fundoo_Context_Note.SaveChanges();
                    return noteEntity.archive;
                }
                else
                {
                    noteEntity.archive = false;
                    fundoo_Context_Note.SaveChanges();
                    return noteEntity.archive;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string Delete_A_note(int userID)
        {
            try
            {
                bool idExist = fundoo_Context_Note.Notes.Any(x => x.UserId == userID);
                if (idExist)
                {
                    var notesToDelete = fundoo_Context_Note.Notes.Where(x => x.UserId == userID).ToList();
                    foreach (var note in notesToDelete)
                    {
                        fundoo_Context_Note.Notes.Remove(note);
                    }
                    fundoo_Context_Note.SaveChanges();
                    return "Note deleted";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public List<NoteEntity> GetAll(string emailid)
        {
            try
            {
                int id = fundoo_Context_Note.Users.Where(x => x.Email == emailid).Select(x => x.UserId).FirstOrDefault();

                //bool idExist = fundoo_Context_Note.Notes.Any(x => x.UserId == id);
                //  string email = idExist.
                // bool EmailExist = fundoo_Context_Note.Users.Any(x => x.Email == idExist.ema);
                bool idExist = fundoo_Context_Note.Notes.Any(x => x.UserId == id);
                if (idExist)
                {
                    List<NoteEntity> noteEntities = fundoo_Context_Note.Notes.Where(x => x.UserId == id).ToList();
                    return noteEntities;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }
    }
}
