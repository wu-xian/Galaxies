using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.EntityModel;
using Galaxies.Model.Context;
using Galaxies.Model.EntityModel;

namespace Model.Context
{
    public class LampDbContext : GalaxiesDbContext
    {
        public LampDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Room> Room { set; get; }
        public DbSet<Article> Article { set; get; }
    }
}
