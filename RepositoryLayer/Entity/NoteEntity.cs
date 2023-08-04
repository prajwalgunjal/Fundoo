using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class NoteEntity
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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int noteID { get; set; }
        
        [ForeignKey("User")]
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual UserEntity User { get; set; }
    }
}
