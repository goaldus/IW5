using System;

namespace IWManager.BL.Messages
{
    public class DeletedCourseMessage
    {
        public DeletedCourseMessage(Guid objectId)
        {
            ObjectId = objectId;
        }

        public Guid ObjectId { get; set; }
    }
}
