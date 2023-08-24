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
    public class LabelRepo : ILabelRepo
    {
        private Fundoo_Context fundoo_Context_Note;
        private readonly IConfiguration configuration;
        public LabelRepo(Fundoo_Context fundoo_Context, IConfiguration configuration)
        {
            this.fundoo_Context_Note = fundoo_Context;
            this.configuration = configuration;
        }
        public LabelEntity AddLabel(LabelModel labelModel, int userID, int noteID)
        {
            LabelEntity labelEntity = new LabelEntity();
            labelEntity.noteID = noteID;
            labelEntity.UserId = userID;
            labelEntity.Label = labelModel.label;
            labelEntity.createdAt = DateTime.Now;
            labelEntity.updatedAt = DateTime.Now;
            fundoo_Context_Note.Add(labelEntity);
            fundoo_Context_Note.SaveChanges();
            return labelEntity;
        }
        public List<LabelEntity> GetLabels(string label, int userID)
        {   
            List<LabelEntity> labelEntities = fundoo_Context_Note.Labels.Where(x => x.Label == label && x.UserId == userID).ToList();
            if (labelEntities != null)
            {

                return labelEntities;
            }
            else
                return null;
        }

        public List<NoteEntity> DisplayByLabel(string label, int userID)
        {
            List<int> noteId;
            List<NoteEntity> noteEntity;
            bool checkLabel = fundoo_Context_Note.Labels.Any(x => x.Label == label && x.UserId == userID);
            noteId = fundoo_Context_Note.Labels.Where(x => x.Label == label).Select(x => x.noteID).ToList();

            if (checkLabel)
            {
                //noteEntity = fundoo_Context_Note.Notes.Where(x => x.noteID == noteId).ToList();
                noteEntity = fundoo_Context_Note.Labels.Where(x => x.Label == label && x.UserId == userID).Join(fundoo_Context_Note.Notes, x => x.noteID, y => y.noteID, (x, y) => y).ToList();
                return noteEntity;
            }
            else
            {
                return null;
            }
        }

        public LabelEntity Delete_Label_Of_A_Note(int noteID,int userid)
        {
            var checkNoteID = fundoo_Context_Note.Labels.Any(x=> x.noteID == noteID && x.UserId == userid);
            if (checkNoteID)
            {
                LabelEntity labelEntity = fundoo_Context_Note.Labels.FirstOrDefault(x => x.noteID == noteID);
                fundoo_Context_Note.Remove(labelEntity);
                fundoo_Context_Note.SaveChanges();
                return labelEntity;
            }
            else { return null; }   
        }

        public List<LabelEntity> DisplayAllLabel(int userId)
        {
            List<LabelEntity> labelEntities = new List<LabelEntity>();
            labelEntities = fundoo_Context_Note.Labels.Where(x=> x.UserId == userId).ToList();
            if(labelEntities!= null)
            {
                return labelEntities;
            }
            else
            {
                return null;
            }
        }

    }
}
