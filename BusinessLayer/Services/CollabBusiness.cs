using BusinessLayer.Interfaces;
using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CollabBusiness:ICollabBusiness
    {
        ICollabRepo icollabRepo;
        public CollabBusiness(ICollabRepo icollabRepo)
        {
            this.icollabRepo = icollabRepo;
        }

        public CollabEntity AddCollab(int userID, int NoteId, CollabModel collabModel)
        {
            return icollabRepo.AddCollab(userID, NoteId, collabModel);
        }
        public List<CollabEntity> GetCollabEntities(int userID, int noteid)
        {
            return icollabRepo.GetCollabEntities(userID,noteid);
        }

        public CollabEntity RemoveCollab(int userID, int collabID, int noteID)
        {
            return icollabRepo.RemoveCollab(userID, collabID, noteID);
        }
        public List<CollabEntity> Get_All_Collabs(int userID)
        {
            return icollabRepo.Get_All_Collabs(userID);
        }
        public List<NoteEntity> Get_All_Note_For_One_Collab(int userID, int CollabID)
        {
            return icollabRepo.Get_All_Note_For_One_Collab(userID, CollabID);
        }
    }
}
