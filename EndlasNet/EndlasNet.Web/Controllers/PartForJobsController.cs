﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndlasNet.Data;

namespace EndlasNet.Web.Controllers
{
    public class PartForJobsController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public PartForJobsController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: PartForJobs
        public async Task<IActionResult> Index()
        {
            var endlasNetDbContext = _context.PartsForJobs.Include(p => p.PartInfo).Include(p => p.User).Include(p => p.Work);
            return View(await endlasNetDbContext.ToListAsync());
        }

        // GET: PartForJobs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForJob = await _context.PartsForJobs
                .Include(p => p.PartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .FirstOrDefaultAsync(m => m.PartId == id);
            if (partForJob == null)
            {
                return NotFound();
            }

            return View(partForJob);
        }

        // GET: PartForJobs/Create
        public IActionResult Create()
        {
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "AuthString");
            ViewData["WorkId"] = new SelectList(_context.Work, "WorkId", "WorkDescription");
            return View();
        }

        // POST: PartForJobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartId,WorkId,StaticPartInfoId,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,UserId")] PartForJob partForJob)
        {
            if (ModelState.IsValid)
            {
                partForJob.PartId = Guid.NewGuid();
                _context.Add(partForJob);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber", partForJob.StaticPartInfoId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "AuthString", partForJob.UserId);
            ViewData["WorkId"] = new SelectList(_context.Work, "WorkId", "WorkDescription", partForJob.WorkId);
            return View(partForJob);
        }

        // GET: PartForJobs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForJob = await _context.PartsForJobs.FindAsync(id);
            if (partForJob == null)
            {
                return NotFound();
            }
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber", partForJob.StaticPartInfoId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "AuthString", partForJob.UserId);
            ViewData["WorkId"] = new SelectList(_context.Work, "WorkId", "WorkDescription", partForJob.WorkId);
            return View(partForJob);
        }

        // POST: PartForJobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PartId,WorkId,StaticPartInfoId,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,UserId")] PartForJob partForJob)
        {
            if (id != partForJob.PartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partForJob);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartForJobExists(partForJob.PartId))
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
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber", partForJob.StaticPartInfoId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "AuthString", partForJob.UserId);
            ViewData["WorkId"] = new SelectList(_context.Work, "WorkId", "WorkDescription", partForJob.WorkId);
            return View(partForJob);
        }

        // GET: PartForJobs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForJob = await _context.PartsForJobs
                .Include(p => p.PartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .FirstOrDefaultAsync(m => m.PartId == id);
            if (partForJob == null)
            {
                return NotFound();
            }

            return View(partForJob);
        }

        // POST: PartForJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var partForJob = await _context.PartsForJobs.FindAsync(id);
            _context.PartsForJobs.Remove(partForJob);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartForJobExists(Guid id)
        {
            return _context.PartsForJobs.Any(e => e.PartId == id);
        }
    }
}