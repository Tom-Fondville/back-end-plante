using back_end_plante.Common.Models;

namespace back_end_plante.Common.Requests;

public class CreateQuestionRequest
{
    public string RequestorId { get; set; }
    public string QuestionType { get; set; }
    public string Question { get; set; }


    public Forum ToForum()
    {
        return new Forum
        {
            RequestorId = RequestorId,
            QuestionType = QuestionType,
            Question = Question
        };
    }
}