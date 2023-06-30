using back_end_plante.Common.Models.Messaging;

namespace back_end_plante.Common.Requests;

public class SendMessageRequest
{
    public Discussion Discussion { get; set; }
    public Message Message { get; set; }
}