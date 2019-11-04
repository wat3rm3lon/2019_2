using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityApi.Helpers;
using EntityApi.Entities;

namespace EntityApi.Services
{
    public interface IEntityService
    {
        void Insert(Entity entity);
        Entity Get(Guid id);
    }
    public class EntityService : IEntityService
    {
        public EntityContext context_;
        public EntityService(EntityContext context)
        {
            context_ = context;
        }
        public void Insert(Entity entity)
        {
            context_.Entitys.Add(entity);
            var entity_ = context_.Entitys.SingleOrDefault(e => e.Id == entity.Id);
            if(entity_ != null)
            {
                entity_.Amount = entity.Amount;
                entity_.OperationDate = entity.OperationDate;
            }
            else
            {
                context_.Entitys.Add(entity);
            }
            
            context_.SaveChanges();

        }
        public Entity Get(Guid id)
        {
            return context_.Entitys.Find(id);
        }
    }
}
