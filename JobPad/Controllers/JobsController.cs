using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobPad.Data;
using JobPad.Models;
using Microsoft.AspNetCore.Identity;
using JobPad.Models.ViewModels;

namespace JobPad.Controllers
{
    public class JobsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public JobsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }


        // Private method to get current user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            ViewData["currentUser"] = currentUser;

            var applicationDbContext = _context.Jobs
                .Include(j => j.Customer)
                .Where(j => j.Customer.UserId == currentUser.Id)
                .OrderBy(j => j.Customer.LastName);
            return View(await applicationDbContext.ToListAsync());
        }



        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .Include(j => j.Customer)
                .Include(j => j.Materials)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        public async Task<IActionResult> Create()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            List<Customer> customers = await _context.Customers.Where(c => c.UserId == currentUser.Id).ToListAsync();

            var viewModel = new JobCreateViewModel()
            {
                CustomerOptions = customers.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.FirstName + " " + c.LastName
                    
                }).ToList()
            };

            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "EmailAddress");
            return View(viewModel);
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EstimatedJobCost,FinalCost,IsPaid,Address,CustomerId")] Job job)
        {
            if (ModelState.IsValid)
            {
                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "EmailAddress", job.CustomerId);
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            List<Customer> customers = await _context.Customers.Where(c => c.UserId == currentUser.Id).ToListAsync();

            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            var viewModel = new JobCreateViewModel()
            {
                Job=job,
                CustomerOptions = customers.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.FirstName + " " + c.LastName

                }).ToList()
            };

            ViewData["CustomerId,"] = new SelectList(_context.Customers, "Id", "EmailAddress");
            return View(viewModel);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,JobCreateViewModel viewModel)
        {

            var job = viewModel.Job;

            if (id != job.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "EmailAddress", viewModel.Job.CustomerId);
            return View(viewModel.Job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .Include(j => j.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
}
