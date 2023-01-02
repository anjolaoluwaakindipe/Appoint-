using Appoint.Application.Hello.Common;
using ErrorOr;
using MediatR;

namespace Appoint.Application.Hello.Queries.SendMessage;

public record SendMessageQuery(string Name, int Age):IRequest<ErrorOr<HelloMessage>>;