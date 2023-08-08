using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ICollabBusiness
    {
        public CollabEntity AddCollab(int userID, int NoteId, CollabModel collabModel);
        public List<CollabEntity> GetCollabEntities(int userID, int noteid);

    }
}
