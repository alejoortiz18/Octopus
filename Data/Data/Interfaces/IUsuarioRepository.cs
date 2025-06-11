using Models.Entities.Domain.DBOctopus.OctopusEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario ObtenerPorEmail(string email);
        Task ActualizarUsuarioAsync(Usuario usuario);

        /// <summary>
        /// <para>Method usuario x ID(Guid Id).</para>
        /// <br/>Autor:                 RAOG  - Rene Alejandro Ortiz Gaviria
        /// <br/>Create date:           20-02-2025
        /// <br/>Update date: 
        /// <br/>Version:               1.0
        /// <br/>Sistema:               UltraRaceTraining
        /// <br/>Description:           Recibe el Id de un plan de entrenamiento y retorna una lista de Ids de los usuarios asociados a ese plan.
        /// <br/>Assemblies:            Data.Repository.UsuarioRepository    
        /// <br/>
        ///<br/>Historial-------------------------------------------------------------------------------------
        /// <br/>
        /// </summary>
        /// <param name="Id">Obtener usuario por el id</param>
        /// <returns>Una lista de Ids de los usuarios asociados al plan de entrenamiento.</returns>


        Usuario ObtenerPorId(Guid Id);

        bool SaveProfile(Usuario user);
    }
}
