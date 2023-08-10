using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModels
{
    public class TakeANoteModel
    {
        public string title { get; set; }
        public string takeANote { get; set; }
        public string image { get; set; }
        public string colour { get; set; }
        public bool archive { get; set; }
        public bool trash { get; set; }
        public DateTime reminder { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public bool pin { get; set; }
        public bool unPin { get; set; }
        //public int noteID { get; set; }
        //public int userID { get; set; }

    }
}
