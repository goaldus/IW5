using System;

namespace IWManager.BL.Messages
{
    public class DeletedStudentMessage
    {
        public DeletedStudentMessage(Guid objectId)
        {
            ObjectId = objectId;
        }

        public Guid ObjectId { get; set; }
    }
}