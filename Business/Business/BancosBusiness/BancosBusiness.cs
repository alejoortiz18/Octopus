using Business.Interfaces;
using Data.Interfaces;
using Models.Entities.Domain.DBOctopus.OctopusEntities;

namespace Business.BancosBusiness
{
    public class BancosBusiness : IBancosBusiness
    {
        private readonly IBancosRepository _bancosRepository;
        public BancosBusiness(IBancosRepository bancosRepository)
        {
            _bancosRepository = bancosRepository;
        }

        public List<Banco> ObtenerBancos()
        {
            return _bancosRepository.ObtenerBancos();
        }
    }
}
