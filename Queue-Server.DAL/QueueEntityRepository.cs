using Microsoft.EntityFrameworkCore;
using Queue_Server.Common.Enums;
using Queue_Server.Common.Interfaces;
using Queue_Server.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Queue_Server.DAL
{
    public class QueueEntityRepository : IQueueEntityRepository
    {
        private QueueDbContext _db;

        public QueueEntityRepository(QueueDbContext dbContext)
        {
            _db = dbContext;
        }

        public QueueEntity GetById(int id)
        {
            return _db.QueueEntities.Find(id);
        }

        public QueueEntity GetEntityByStatus(Status status)
        {
            return _db.QueueEntities.Find((int)status);
        }
        public IEnumerable<QueueEntity> GetEntitiesByStatus(Status status)
        {
            return _db.QueueEntities.Where(qe => qe.Status == status).ToList();
        }

        public IEnumerable<QueueEntity> GetAllEntities()
        {
            return _db.QueueEntities.ToList();
        }

        public QueueEntity AddEntity(string name)
        {
            var entity = new QueueEntity
            {
                Name = name
            };
            _db.Add(entity);
            _db.SaveChanges();
            return entity;
        }

        public QueueEntity PullEntityFromQueue()
        {
            var completedEntity = _db.QueueEntities.Find((int)Status.InProgress);
            completedEntity.Status = Status.Completed;
            var completedEntityEntry = _db.QueueEntities.Attach(completedEntity);
            completedEntityEntry.State = EntityState.Modified;
            var inProgressEntity = _db.QueueEntities.Find((int)Status.Awaiting);
            inProgressEntity.Status = Status.InProgress;
            var inProgressEntityEntry = _db.QueueEntities.Attach(inProgressEntity);
            inProgressEntityEntry.State = EntityState.Modified;
            _db.SaveChanges();
            return inProgressEntity;
        }

    }
}
