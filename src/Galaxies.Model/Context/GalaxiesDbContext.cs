using Galaxies.Model.EntityModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Model.Context
{
    public class GalaxiesDbContext : DbContext
    {
        public GalaxiesDbContext(DbContextOptions options) : base(options)
        { }

        public GalaxiesDbContext() : base()
        { }

        public virtual DbSet<User> User { set; get; }
        public virtual DbSet<Role> Role { set; get; }

        public virtual DbSet<Permission> Permission { set; get; }
        public virtual DbSet<UserRoleClaim> UserRoleClaim { set; get; }
        public virtual DbSet<ProgramRoleClaim> ProgramRoleClaim { set; get; }
        public virtual DbSet<ProgramForWeb> ProgramForWeb { set; get; }
        public virtual DbSet<MenuForWeb> MenuForWeb { set; get; }
        public virtual DbSet<MenuForWebInRoles> MenuForWebInRoles { set; get; }
    }
}
