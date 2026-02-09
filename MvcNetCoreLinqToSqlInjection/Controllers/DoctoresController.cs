using Microsoft.AspNetCore.Mvc;
using MvcNetCoreLinqToSqlInjection.Models;
using MvcNetCoreLinqToSqlInjection.Repositories;

namespace MvcNetCoreLinqToSqlInjection.Controllers
{
    public class DoctoresController : Controller
    {
        //private RepositoryDoctoresSQLServer repo;
        private RepositoryDoctoresOracle repo;
        //RECIBIMOS NUESTRO REPOSITORY
        public DoctoresController
            (RepositoryDoctoresOracle repo)
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
    }
}
