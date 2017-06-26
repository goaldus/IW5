using IWManager.BL.Models;

namespace IWManager.BL.Messages
{
    public class UpdatedGeneralRatingMessage
    {
        public GeneralRatingDetailModel Model { get; set; }
        public UpdatedGeneralRatingMessage(GeneralRatingDetailModel model)
        {
            Model = model;
        }
    }
}