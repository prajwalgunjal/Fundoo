using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface ICollabRepo
    {
        public CollabEntity AddCollab(int userID, int NoteId, CollabModel collabModel);
        public List<CollabEntity> GetCollabEntities(int userID, int noteid);
        public CollabEntity RemoveCollab(int userID, int collabID, int noteID);
        public List<CollabEntity> Get_All_Collabs(int userID);
        public List<NoteEntity> Get_All_Note_For_One_Collab(int userID, int CollabID);
    }
}