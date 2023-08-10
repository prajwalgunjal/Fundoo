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
        public CollabEntity RemoveCollab(int userID, int collabID, int noteID)
        {
            CollabEntity collabEntity = new CollabEntity();
            collabEntity = Fundoo_Context.Collabs.Where(x => x.UserId == userID && x.noteID== noteID && x.ColabId == collabID ).FirstOrDefault();
            if(collabEntity != null)
            {
                Fundoo_Context.Collabs.Remove(collabEntity);
                Fundoo_Context.SaveChanges() ;
                return collabEntity;

            }
            else
            {
                return null;
            }

        }

        public List<CollabEntity> Get_All_Collabs(int userID) 
        {
            List<CollabEntity> collabEntities = new List<CollabEntity>();
            collabEntities = Fundoo_Context.Collabs.Where(x => x.UserId == userID).ToList();
            if (collabEntities != null)
            {
                return collabEntities;
            }
            else
            {
                return null;
            }
        }

        public List<NoteEntity> Get_All_Note_For_One_Collab(int userID, int CollabID)
        {
            List<NoteEntity> noteEntities = new List<NoteEntity>();
            noteEntities = Fundoo_Context.Collabs.Where(x => x.ColabId == CollabID && x.UserId == userID).Join(Fundoo_Context.Notes, x => x.noteID, y => y.noteID, (x, y) => y).ToList();
            if (noteEntities != null)
            {
                return noteEntities;
            }
            else { return null; }
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
