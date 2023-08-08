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
    }
}
