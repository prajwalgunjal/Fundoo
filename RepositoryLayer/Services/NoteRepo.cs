using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

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

        public string ChangeColour(int noteId, string colour,int userID)
        {
            try
            {
                bool idExist = fundoo_Context_Note.Notes.Any(x => x.noteID == noteId && x.UserId == userID);
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

        public DateTime SetReminder(int noteId,DateTime dateTime, int userID)
        {
                bool idExist = fundoo_Context_Note.Notes.Any(x => x.noteID == noteId && x.UserId == userID);
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


        public bool isPin(int noteId,int userID)
        {
            try
            {
                NoteEntity noteEntity = this.fundoo_Context_Note.Notes.FirstOrDefault(x => x.noteID == noteId && x.UserId == userID);
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
                    return noteEntity.pin;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public bool TrashNotes (int noteId ,int userID)
        {
            try
            {

                NoteEntity noteEntity = this.fundoo_Context_Note.Notes.FirstOrDefault(x => x.noteID == noteId && x.UserId == userID);
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
                    return noteEntity.trash;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool IsArchive(int noteId,int userID)
        {
            try
            {
                NoteEntity noteEntity = this.fundoo_Context_Note.Notes.FirstOrDefault(x => x.noteID == noteId && x.UserId == userID);
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

        public NoteEntity Delete_A_note(int userID , int noteID)
        {
            try
            {
                bool idExist = fundoo_Context_Note.Notes.Any(x => x.UserId == userID && x.noteID == noteID);
                if (idExist)
                {
                    NoteEntity notesToDelete = fundoo_Context_Note.Notes.FirstOrDefault(x => x.noteID == noteID);
                    fundoo_Context_Note.Notes.Remove(notesToDelete);
                    fundoo_Context_Note.SaveChanges();
                    return notesToDelete;
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
        
        public List<NoteEntity> GetAll(int userID)
        {
           
            try
            {
                int id = fundoo_Context_Note.Users.Where(x => x.UserId == userID).Select(x => x.UserId).FirstOrDefault();
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
        public List<NoteEntity> Get_All_Notes_Without_Login()
        {
            try
            {
               
                    List<NoteEntity> noteEntities = fundoo_Context_Note.Notes.ToList();
                    return noteEntities;
           
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public bool SearchNote(string notetitle, int userId)
        {
            try
            {
                bool exists = fundoo_Context_Note.Notes.Any(x => x.title == notetitle && x.UserId == userId);

                return exists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        /*  public ImageUploadResult UploadImage(IFormFile imagePath) 
          {
              Account account = new Account(configuration["Cloudinary: CloudName"], configuration["Cloudinary: ApiKey"], configuration["Cloudinary: ApiSecret"]);
              Cloudinary cloud = new Cloudinary(account);
              var uploadParams = new ImageUploadParams()
              {

                  File = new FileDescription(imagePath.FileName, imagePath.OpenReadStream()),
              };
              var uploadImageRes = cloud.Upload(uploadParams);
              if (uploadImageRes != null) 
              {
                  return uploadImageRes;
              }
              else {
                      return null;
              }

          }*/


        public string UploadImage(string filePath, long notesId, long userId)
        {
            var filterUser = fundoo_Context_Note.Notes.Where(e => e.UserId == userId);
            if (filterUser != null)
            {
                var findNotes = filterUser.FirstOrDefault(e => e.noteID == notesId);
                if(findNotes != null) 
                {
                        Account account = new Account("dzhcgkbwm", "931298622371919", "mmivF3zF1tY-HHyq8HNGaRB5pPs");
                        Cloudinary cloudinary = new Cloudinary(account);
                        ImageUploadParams uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(filePath),
                            PublicId = findNotes.title
                        };

                        ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

                        findNotes.updatedAt = DateTime.Now;
                        findNotes.image = uploadResult.Url.ToString();
                        fundoo_Context_Note.SaveChanges();
                        return "Upload Successfull";
                }
                    return null;
                }
                else
                {
                    return null; 
                }
            
        }
        


        }
}
