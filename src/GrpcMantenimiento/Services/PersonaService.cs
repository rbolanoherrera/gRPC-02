using Grpc.Core;

namespace GrpcMantenimiento.Services;


public class PersonaService(ILogger<PersonaService> logger) : PersonaSvc.PersonaSvcBase
{
    public override Task<PersonaResponse> RegistrarPersona(PersonaRequest request, ServerCallContext context)
    {
        logger.LogInformation("The message is received from {Nombre}", request.Persona.Nombre);

        return Task.FromResult(new PersonaResponse
        {
            Code = "200",
            Message = $"La persona {request.Persona.Nombre} se ha registrado correctamente"
        });
    }
}
