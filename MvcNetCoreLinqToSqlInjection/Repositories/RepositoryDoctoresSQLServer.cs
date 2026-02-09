using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using MvcNetCoreLinqToSqlInjection.Models;
using System.Data;
using System.Numerics;

namespace MvcNetCoreLinqToSqlInjection.Repositories
{

    #region STORED PROCEDURES
    //CREATE PROCEDURE SP_DELETE_DOCTOR
    //(@id int)
    //AS
    //    DELETE FROM DOCTOR WHERE DOCTOR_NO = @id
    //GO

    //CREATE PROCEDURE SP_MODIFICAR_DOCTOR
    //(@id int, @apellido nvarchar(50), @especialidad nvarchar(50), @salario int, @idHospital int)
    //AS
    //    UPDATE DOCTOR SET APELLIDO=@apellido, ESPECIALIDAD=@especialidad, SALARIO=@salario, HOSPITAL_COD=@idHospital WHERE DOCTOR_NO=@id
    //GO
    #endregion
    public class RepositoryDoctoresSQLServer: IRepositoryDoctores
    {
        private SqlConnection cn;
        private SqlCommand com;
        private DataTable tablaDoctor;

        public RepositoryDoctoresSQLServer()
        {
            string connectionString = @"Data Source=LOCALHOST\DEVELOPER;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            string sql = "select * from DOCTOR";
            SqlDataAdapter ad = 
                new SqlDataAdapter(sql, this.cn);
            this.tablaDoctor = new DataTable();
            ad.Fill(this.tablaDoctor);
        }

        public List<Doctor> GetDoctores()
        {
            var consulta = from datos in
                               this.tablaDoctor.AsEnumerable()
                           select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
            {
                Doctor doc = new Doctor
                {
                    IdDoctor = row.Field<int>("DOCTOR_NO"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Especialidad = row.Field<string>("ESPECIALIDAD"),
                    Salario = row.Field<int>("SALARIO"),
                    IdHospital = row.Field<int>("HOSPITAL_COD")
                };
                doctores.Add(doc);
            }
            return doctores;
        }

        public async Task CreateDoctorAsync
            (int idDoctor, string apellido
            , string especialidad, int salario, int idHospital)
        {
            string sql = "insert into DOCTOR values " 
                + " (@idhospital, @id, @apellido "
                + ", @especialidad, @salario)";
            this.com.Parameters.AddWithValue("@id", idDoctor);
            this.com.Parameters.AddWithValue("@apellido", apellido);
            this.com.Parameters.AddWithValue("@especialidad", especialidad);
            this.com.Parameters.AddWithValue("@salario", salario);
            this.com.Parameters.AddWithValue("@idhospital", idHospital);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task DeleteDoctorAsync(int idDoctor)
        {
            string sql = "SP_DELETE_DOCTOR";
            this.com.Parameters.AddWithValue("@id", idDoctor);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }


        public async Task UpdateDoctorAsync(int idDoctor, string apellido, string especialidad, int salario, int idHospital)
        {
            string sql = "SP_MODIFICAR_DOCTOR";
            this.com.Parameters.AddWithValue("@id", idDoctor);
            this.com.Parameters.AddWithValue("@apellido", apellido);
            this.com.Parameters.AddWithValue("@especialidad", especialidad);
            this.com.Parameters.AddWithValue("@salario", salario);
            this.com.Parameters.AddWithValue("@idHospital", idHospital);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }
    }
}
