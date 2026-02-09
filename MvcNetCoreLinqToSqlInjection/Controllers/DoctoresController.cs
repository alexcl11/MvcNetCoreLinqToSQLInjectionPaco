using Microsoft.AspNetCore.Mvc;
using MvcNetCoreLinqToSqlInjection.Models;
using MvcNetCoreLinqToSqlInjection.Repositories;

namespace MvcNetCoreLinqToSqlInjection.Controllers
{
    public class DoctoresController : Controller
    {
        //private RepositoryDoctoresSQLServer repo;
        //private RepositoryDoctoresOracle repo;
        IRepositoryDoctores repo;
        //RECIBIMOS NUESTRO REPOSITORY
        public DoctoresController
            (IRepositoryDoctores repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Doctor> doctores =
                this.repo.GetDoctores();
            return View(doctores);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Doctor doc)
        {
            await this.repo.CreateDoctorAsync
                (doc.IdDoctor, doc.Apellido, doc.Especialidad
                , doc.Salario, doc.IdHospital);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int iddoctor)
        {
            await this.repo.DeleteDoctorAsync(iddoctor);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(id);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, string apellido, string especialidad, int salario, int idHospital )
        {
            await this.repo.UpdateDoctorAsync(id, apellido, especialidad, salario, idHospital);
            return RedirectToAction("Index");
        }

        public IActionResult DoctoresEspecialidad()
        {
            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);
        }

        [HttpPost]
        public IActionResult DoctoresEspecialidad(string especialidad) 
        {
            List<Doctor> doctores = this.repo.GetDoctoresEspecialidad(especialidad);
            return View(doctores);
        }

    }
}
