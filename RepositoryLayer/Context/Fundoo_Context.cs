using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class Fundoo_Context : DbContext 
    {
        public Fundoo_Context(DbContextOptions dbContextOptions ): base(dbContextOptions) {

        }    

        public DbSet<UserEntity> Users { get; set; } // users name of the table 
        public DbSet<NoteEntity> Notes { get; set; }
    }
}
