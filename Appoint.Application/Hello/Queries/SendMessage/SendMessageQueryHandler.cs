using Appoint.Application.Hello.Common;
using ErrorOr;
using MediatR;

namespace Appoint.Application.Hello.Queries.SendMessage;

public class SendMessageQueryHandler : IRequestHandler<SendMessageQuery, ErrorOr<HelloMessage>>
{
    public async Task<ErrorOr<HelloMessage>> Handle(SendMessageQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return new HelloMessage(Message: $"Hey {request.Name} I didn't know you were {request.Age} years old");
    }
}