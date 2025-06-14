using Models.Dto.Auth;
using Models.Entities.Domain.DBOctopus.OctopusEntities;

namespace Business.Interfaces
{
    public interface IAuthBusiness
    {
        Task<int> InicioSesionAsync(LoginDto user);
        Task<bool> GenerarCodigoRestablecimientoAsync(string email);
        Task<(bool Success, string ErrorMessage)> SignUpAsync(SignUpDto signUpDto);
    }
}
//dotnet ef dbcontext scaffold "Server=DESKALEJO\\SQLEXPRESS;Database=Octopus;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Entities/Domain/DBTraining/TraininEntities --context AppDbContext --force --project "E:\empresas\Personales\Red de mercadeo\Proyecto\Octopus\Octopus\Models\Models\Models.csproj"
//Scaffold - DbContext "Server=DESKALEJO\SQLEXPRESS;Database=Octopus;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer - OutputDir Model / DBEntities - Force