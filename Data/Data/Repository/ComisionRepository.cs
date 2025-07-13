using Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Dto.Comision;
using Models.Entities.Domain.DBOctopus.OctopusEntities;
using System.Data;

namespace Data.Repository
{
    public class ComisionRepository : IComisionRepository
    {
        private readonly AppDbContext _context;
        public ComisionRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<ComisionDto> BuscarNivelesSuperioesComisiones(int UsuarioID)
        {
            var userID = new SqlParameter("@UsuarioID", SqlDbType.Int);
            userID.Value = UsuarioID;
            userID.IsNullable = false;

            string consulta = "EXEC Usp_BuscarNivelesSuperioesComisiones @UsuarioID";
            var response = _context.Database.SqlQueryRaw<ComisionDto>(consulta, userID).ToList();

            return response;
        }

        public bool IngresarComisiones(List<Comision> listComisiones)
        {
            try
            {
                _context.Comisions.AddRange(listComisiones);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
