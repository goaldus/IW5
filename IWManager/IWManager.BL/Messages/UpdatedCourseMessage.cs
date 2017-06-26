using IWManager.BL.Models;

namespace IWManager.BL.Messages
{
    public class UpdatedCourseMessage
    {
        public CourseDetailModel Model { get; set; }
        public UpdatedCourseMessage(CourseDetailModel model)
        {
            Model = model;
        }
    }
}
