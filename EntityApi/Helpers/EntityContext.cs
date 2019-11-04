using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EntityApi.Entities;

namespace EntityApi.Helpers
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options) : base(options) { }

        public DbSet<Entity> Entitys { get; set; }
    }
}
