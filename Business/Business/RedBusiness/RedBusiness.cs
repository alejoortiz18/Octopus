using Business.Interfaces;
using Data.Interfaces;
using Models.Entities.Domain.DBOctopus.OctopusEntities;
using Models.Model.Usuario;

namespace Business.RedBusiness
{
    public class RedBusiness : IRedBusiness
    {
        private readonly IRedRepository _redRepository;
        public RedBusiness(IRedRepository redRepository)
        {
            _redRepository = redRepository;
        }

        public List<RedPorReferidosByIdUsuarioDto> GetTodaLaRedPorUsuarioIdAsync(int usuarioId)
        {
            return _redRepository.GetTodaLaRedPorUsuarioIdAsync(usuarioId);
        }

    }
}
