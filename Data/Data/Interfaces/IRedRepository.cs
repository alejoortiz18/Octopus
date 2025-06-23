using Models.Entities.Domain.DBOctopus.OctopusEntities;
using Models.Model.Usuario;

namespace Data.Interfaces
{
    public interface IRedRepository
    {
        public List<RedPorReferidosByIdUsuarioDto> GetTodaLaRedPorUsuarioIdAsync(int usuarioId);

    }
}
