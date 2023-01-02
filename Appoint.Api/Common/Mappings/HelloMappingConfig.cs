using Appoint.Application.Hello.Common;
using Appoint.Contracts.Hello.Requests;
using Appoint.Contracts.Hello.Responses;
using Mapster;

namespace Appoint.Api.Common.Mappings;

public class HelloMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<HelloMessage, HelloResponse>().Map(dest=> dest.Message, src => src.Message);
    }
}