using Grpc.Core;

namespace GrpcMantenimiento.Services;


public class PersonService(ILogger<PersonService> logger) : PersonSvc.PersonSvcBase
{
    public override Task<PersonResponse> InsertPerson(PersonRequest request, ServerCallContext context)
    {
        logger.LogInformation("The message is received from {Nombre}", request.Person.Name);

        return Task.FromResult(new PersonResponse
        {
            Code = "200",
            Message = $"La persona {request.Person.Name} se ha registrado correctamente"
        });
    }
}
