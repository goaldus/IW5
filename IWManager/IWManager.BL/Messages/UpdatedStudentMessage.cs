using IWManager.BL.Models;

namespace IWManager.BL.Messages
{
    public class UpdatedStudentMessage
    {
        public StudentDetailModel Model { get; set; }
        public UpdatedStudentMessage(StudentDetailModel model)
        {
            Model = model;
        }
    }
}