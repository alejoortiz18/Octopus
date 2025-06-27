using Models.Entities.Domain.DBOctopus.OctopusEntities;
using Models.Model.Usuario;

namespace Business.Interfaces
{
    public interface IRedBusiness
    {
        public List<RedPorReferidosByIdUsuarioDto> GetTodaLaRedPorUsuarioIdAsync(int usuarioId);
        public RedReferido Insert(RedReferido red);
    }
}
