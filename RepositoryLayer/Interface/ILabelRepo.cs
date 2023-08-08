using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface ILabelRepo
    {
        public LabelEntity AddLabel(LabelModel labelModel, int userID, int noteID);
        public List<LabelEntity> GetLabels(string label);
    }
}