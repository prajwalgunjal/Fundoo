using CommonLayer.RequestModels;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CollabRepo : ICollabRepo
    {
        Fundoo_Context Fundoo_Context;
        public CollabRepo(Fundoo_Context fundoo_Context)
        {
            Fundoo_Context = fundoo_Context;
        }
        public CollabEntity AddCollab(int userID, int NoteId, CollabModel collabModel)
        {
            CollabEntity collabEntity = new CollabEntity();
            collabEntity.noteID = NoteId;
            collabEntity.UserId = userID;
            collabEntity.Email = collabModel.Email;
            collabEntity.createdAt = DateTime.Now;
            collabEntity.updatedAt = DateTime.Now;
            Fundoo_Context.Collabs.Add(collabEntity);
            Fundoo_Context.SaveChanges();
            return collabEntity;
        }
        public List<CollabEntity> GetCollabEntities(int userID,int noteid)
        {
            List<CollabEntity> resultList = new List<CollabEntity> ();

            var result = Fundoo_Context.Collabs.Where(x => x.noteID == noteid && x.UserId == userID).FirstOrDefault();
            if(result != null)
            {
                resultList = Fundoo_Context.Set<CollabEntity>().ToList();
            }
            return resultList;
        }



    }
}
