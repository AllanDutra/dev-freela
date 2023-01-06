using MediatR;

namespace DevFreela.Application.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<Unit> // ? Unit representa o void.
    {
        public string Content { get; set; }
        public int IdProject { get; set; }
        public int IdUser { get; set; }
    }
}