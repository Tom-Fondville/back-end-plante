using back_end_plante.Common.Models.Messaging;

namespace back_end_plante.Common.Requests;

public class SendMessageRequest
{
    public DiscussionId DiscussionId { get; set; }
    public Message Message { get; set; }
}