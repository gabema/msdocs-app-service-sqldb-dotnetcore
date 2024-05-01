using DotNetCoreSqlDb.Data;
using DotNetCoreSqlDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace DotNetCoreSqlDb.Controllers
{
    [ActionTimerFilter]
    public class TodosController : Controller
    {
        private readonly MyDatabaseContext _context;
        private readonly IMemoryCache _cache;
        private readonly string _TodoItemsCacheKey = "TodoItemsList";

        public TodosController(MyDatabaseContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // GET: Todos
        public async Task<IActionResult> Index()
        {
            var todos = TodoListCache;
            if (TodoListCache.Count == 0)
            { 
                todos = await _context.Todo.ToListAsync();
                _cache.Set(_TodoItemsCacheKey, todos);
            }

            return View(todos);
        }

        // GET: Todos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = TodoListCache.Find(t => t.ID == id);

            if (todo is null)
            {
                todo = await _context.Todo.FirstOrDefaultAsync(m => m.ID == id);

                if (todo is null)
                {
                    return NotFound();
                }

                TodoListCache.Add(todo);
            }

            return View(todo);
        }

        // GET: Todos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Description,CreatedDate")] Todo todo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todo);
                await _context.SaveChangesAsync();
                _cache.Remove(_TodoItemsCacheKey);
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        // GET: Todos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = TodoListCache.Find(t => t.ID == id);

            if (todo is null)
            {
                todo = await _context.Todo.FindAsync(id);
            }

            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        // POST: Todos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Description,CreatedDate")] Todo todo)
        {
            if (id != todo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todo);
                    await _context.SaveChangesAsync();
                    TodoListCache.Clear();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoExists(todo.ID))
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
            return View(todo);
        }

        // GET: Todos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _context.Todo
                .FirstOrDefaultAsync(m => m.ID == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        // POST: Todos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todo = await _context.Todo.FindAsync(id);
            if (todo != null)
            {
                _context.Todo.Remove(todo);
                await _context.SaveChangesAsync();
                TodoListCache.Clear();
            }
            return RedirectToAction(nameof(Index));
        }

        private List<Todo> TodoListCache
        {
            get => ((List<Todo>?)_cache.Get(_TodoItemsCacheKey)) ?? [];
        }

        private bool TodoExists(int id)
        {
            return _context.Todo.Any(e => e.ID == id);
        }
    }
}
