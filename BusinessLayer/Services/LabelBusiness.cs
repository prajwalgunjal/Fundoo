﻿using BusinessLayer.Interfaces;
using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class LabelBusiness : ILabelBusiness
    {
        public ILabelRepo LabelRepo;
        public LabelBusiness(ILabelRepo LabelRepo)
        {
            this.LabelRepo = LabelRepo;
        }

        public LabelEntity AddLabel(LabelModel labelModel, int userID, int noteID)
        {
            return LabelRepo.AddLabel(labelModel,userID,noteID);
        }
        public List<LabelEntity> GetLabels(string label, int userID)
        {
            return LabelRepo.GetLabels(label,userID);
        }

        public List<NoteEntity> DisplayByLabel(string label, int userID)
        {
            return LabelRepo.DisplayByLabel(label,userID);
        }

        public LabelEntity Delete_Label_Of_A_Note(int noteID, int userid)
        {
            return LabelRepo.Delete_Label_Of_A_Note(noteID, userid);
        }
        public List<LabelEntity> DisplayAllLabel(int userId)
        {
            return LabelRepo.DisplayAllLabel(userId);
        }
    }
}
