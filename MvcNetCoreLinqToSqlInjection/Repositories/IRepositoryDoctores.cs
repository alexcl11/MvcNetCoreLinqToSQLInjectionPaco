using MvcNetCoreLinqToSqlInjection.Models;

namespace MvcNetCoreLinqToSqlInjection.Repositories
{
    public interface IRepositoryDoctores
    {
        List<Doctor> GetDoctoresEspecialidad(string especialidad);
        List<Doctor> GetDoctores();

        Task CreateDoctorAsync
            (int idDoctor, string apellido
            , string especialidad, int salario, int idHospital);
        
        Task DeleteDoctorAsync(int idDoctor);

        Task UpdateDoctorAsync(int idDoctor, string apellido
            , string especialidad, int salario, int idHospital);


    }
}
