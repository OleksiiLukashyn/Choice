using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Choice.Data;
using Microsoft.AspNetCore.Mvc;

namespace Choice.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _db;

        public StudentsController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            int n = _db.Students.Count();
            return Content(n.ToString());
        }
    }
}