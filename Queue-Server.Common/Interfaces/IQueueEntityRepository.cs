using Queue_Server.Common.Enums;
using Queue_Server.Common.Models;
using System.Collections.Generic;

namespace Queue_Server.Common.Interfaces
{
    public interface IQueueEntityRepository
    {
        public QueueEntity GetById(int id);
        public IEnumerable<QueueEntity> GetAllEntities();
        public QueueEntity AddEntity(string name);
        public QueueEntity PullEntityFromQueue(QueueEntity queueEntity);
        public QueueEntity GetEntityByStatus(Status status);
    }
}
