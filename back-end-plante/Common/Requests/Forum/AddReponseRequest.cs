using back_end_plante.Common.Models;

namespace back_end_plante.Common.Requests;

public class AddReponseRequest
{
    public string ForumId { get; set; }
    public string BotanistId { get; set; }
    public string Response { get; set; }

    public Forum ToForum()
    {
        return new Forum
        {
            Id = ForumId,
            BotanistId = BotanistId,
            Response = Response
        };
    }
}