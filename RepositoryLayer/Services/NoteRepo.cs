using CommonLayer.RequestModels;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
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
    }
}
