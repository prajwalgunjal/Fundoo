using CommonLayer.RequestModels;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
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
        public List<LabelEntity> GetLabels(string label)
        {
            List<LabelEntity> labelEntities = fundoo_Context_Note.Labels.Where(x => x.Label == label).ToList();
            if (labelEntities != null)
            {

                return labelEntities;
            }
            else
                return null;
        }



    }
}
