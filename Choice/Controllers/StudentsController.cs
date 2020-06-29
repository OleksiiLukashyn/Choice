using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Choice.Data;
using Choice.Models;
using System.Collections.Generic;

namespace Choice.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }


        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Group")] StudentViewModel studentModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentModel);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            var disciplines = _context.Disciplines;
            var studentModel = new StudentViewModel();
            studentModel.Student = student;
            studentModel.Disciplines = disciplines.Select(discipline => new DisciplineViewItem()
            {
                Discipline = discipline,
                IsStudied = discipline.StudDiscs.Any(sd => sd.StudentId == student.Id)
            }
            ).ToList();

            return View(studentModel);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Group")] Student student, List<DisciplineViewItem> disciplines)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            var addedStudDisc = disciplines.Where(discipline => discipline.IsStudied)
                .Select(x => new StudDisc()
                {
                    StudentId = student.Id,
                    DisciplineId = x.Discipline.Id
                });

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    _context.RemoveRange(_context.StudDiscs.Where(sd => sd.StudentId == student.Id));
                    _context.StudDiscs.AddRange(addedStudDisc);
                    await _context.SaveChangesAsync();
    }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
}
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var student = await _context.Students
        .FirstOrDefaultAsync(m => m.Id == id);
    if (student == null)
    {
        return NotFound();
    }

    return View(student);
}

// POST: Students/Delete/5
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    var student = await _context.Students.FindAsync(id);
    _context.Students.Remove(student);
    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
}

private bool StudentExists(int id)
{
    return _context.Students.Any(e => e.Id == id);
}
    }
}
