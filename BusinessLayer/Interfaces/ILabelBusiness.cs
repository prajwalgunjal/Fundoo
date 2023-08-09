using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBusiness
    {
        public LabelEntity AddLabel(LabelModel labelModel, int userID, int noteID);
        public List<LabelEntity> GetLabels(string label, int userID);
        public List<NoteEntity> DisplayByLabel(string label, int userID);
        public LabelEntity Delete_Label_Of_A_Note(int noteID, int userid);
    }
}
