using Lab07.Data;
using Lab07.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab07.Controllers;

public class StudentsController : Controller
{
    private readonly SchoolContext _context;

    public StudentsController(SchoolContext context)
    {
        _context = context;
    }

    // GET: Students
    // GET: Students (SHOW ALL - NO PAGING)
    public async Task<IActionResult> Index(string? sortOrder, string? searchString)
    {
        ViewData["CurrentSort"] = sortOrder;
        ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
        ViewData["CurrentFilter"] = searchString;

        IQueryable<Student> students = _context.Students;

        if (!string.IsNullOrEmpty(searchString))
        {
            students = students.Where(s => s.LastName.Contains(searchString)
                                        || s.FirstMidName.Contains(searchString));
        }

        students = sortOrder switch
        {
            "name_desc" => students.OrderByDescending(s => s.LastName),
            "Date" => students.OrderBy(s => s.EnrollmentDate),
            "date_desc" => students.OrderByDescending(s => s.EnrollmentDate),
            _ => students.OrderBy(s => s.LastName)
        };

        // AsNoTracking: tối ưu cho truy vấn chỉ đọc (không tracking entity) :contentReference[oaicite:1]{index=1}
        return View(await students.AsNoTracking().ToListAsync());
    }

    // GET: Students/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Students == null)
        {
            return NotFound();
        }

        var student = await _context.Students
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

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
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("LastName,FirstMidName,EnrollmentDate")] Student student)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();

                // Đưa user về Index và tự Search theo LastName vừa tạo
                // (Index của bạn đã có logic: nếu searchString != null => pageNumber = 1)
                TempData["Msg"] = "Created successfully!";
                return RedirectToAction(nameof(Index));
            }
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError("", "Unable to save changes. Try again.");
        }

        return View(student);
    }

    // GET: Students/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Students == null)
        {
            return NotFound();
        }

        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    // POST: Students/Edit/5
    [HttpPost, ActionName("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditPost(int? id)
    {
        if (id == null) return NotFound();

        var studentToUpdate = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);
        if (studentToUpdate == null) return NotFound();

        if (await TryUpdateModelAsync(
            studentToUpdate,
            "",
            s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
        {
            try
            {
                await _context.SaveChangesAsync();

                // ĐÚNG biến: studentToUpdate (không phải student)
                return RedirectToAction(nameof(Details), new { id = studentToUpdate.ID });
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again.");
            }
        }

        return View(studentToUpdate);
    }


    // GET: Students/Delete/5
    public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
    {
        if (id == null || _context.Students == null)
        {
            return NotFound();
        }

        var student = await _context.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

        if (student == null)
        {
            return NotFound();
        }

        if (saveChangesError.GetValueOrDefault())
        {
            ViewData["ErrorMessage"] = "Delete failed. Try again, and if the problem persists see your system administrator.";
        }
        TempData["Msg"] = "Delete successfully!";
        return View(student);
    }

    // POST: Students/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            return RedirectToAction(nameof(Delete), new { id, saveChangesError = true });
        }
    }
}
