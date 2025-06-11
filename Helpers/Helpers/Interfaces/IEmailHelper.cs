using Models.Entities.Domain.DBOctopus.OctopusEntities;

namespace Helpers.Interfaces
{
    public interface IEmailHelper
    {
        Task EnviarCorreoAsync(string destinatario, string codigo, string subject, string body);
        Task EnviarCorreoCrearUsuarioAsync(Usuario usuario, string subject, string body);
    }
}
