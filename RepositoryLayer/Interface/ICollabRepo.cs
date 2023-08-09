using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface ICollabRepo
    {
        public CollabEntity AddCollab(int userID, int NoteId, CollabModel collabModel);
        public List<CollabEntity> GetCollabEntities(int userID, int noteid);

    }
}