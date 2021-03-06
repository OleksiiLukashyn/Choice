﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChoiceA.Models;
using System.Threading.Tasks;
using ChoiceA.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Options;

namespace ChoiceA.Controllers
{
    //[ForStudent]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Students
        public IActionResult Index()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == Startup.StudentIdPropertyName);
            if (claim == null)
                return View();
            return RedirectToAction("Edit", new { id = Convert.ToInt32(claim.Value) });
        }


        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Students/Edit/5
        [HttpGet]
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
                Id = discipline.Id,
                Name = discipline.Title,
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
                    DisciplineId = x.Id,

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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAjax([FromBody] StudentViewModel studentViewModel)
        {
            var addedStudDisc = studentViewModel.Disciplines.Where(discipline => discipline.IsStudied)
                .Select(x => new StudDisc()
                {
                    StudentId = studentViewModel.Student.Id,
                    DisciplineId = x.Id,

                });


            if (ModelState.IsValid)
            {
                try
                {
                    _context.RemoveRange(_context.StudDiscs.Where(sd => sd.StudentId == studentViewModel.Student.Id));
                    _context.StudDiscs.AddRange(addedStudDisc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(studentViewModel.Student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return new JsonResult(new { name = studentViewModel.Student.Name });
            }

            var errMes = ModelState["text"]?.Errors[0]?.ErrorMessage ?? "Unknown error";
            return BadRequest(errMes);
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
