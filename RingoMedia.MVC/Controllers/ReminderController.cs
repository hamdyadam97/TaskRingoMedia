using Microsoft.AspNetCore.Mvc;
using RingoMedia.Application.IServices;
using RingoMedia.Application.Services;
using RingoMedia.Dtos;
using RingoMedia.Models;

namespace RingoMedia.MVC.Controllers
{
    public class ReminderController : Controller
    {
        private readonly IReminderService _reminderService;

        public ReminderController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }

        // GET: Reminder/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reminder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReminderDTO reminderDto)
        {
            if (ModelState.IsValid)
            {
                await _reminderService.AddReminderAsync(reminderDto);
                return RedirectToAction(nameof(Index));
            }

            // Debug validation errors
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View(reminderDto);
        }

        // GET: Reminder/Index
        public async Task<IActionResult> Index()
        {
            var reminders = await _reminderService.GetRemindersAsync();
            return View(reminders);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var remendirDto = await _reminderService.GetReminderByIdAsync(id);
            if (remendirDto == null)
            {
                return NotFound();
            }

            // Prepare ViewBag or ViewData if necessary (e.g., for dropdown lists)


            return View(remendirDto);
        }

            [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ReminderDTO reminderDto)
        {
            if (id != reminderDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _reminderService.UpdateReminderAsync(reminderDto);
                return RedirectToAction(nameof(Index));
            }

            return View(reminderDto);
        }

        // GET: Reminder/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var reminder = await _reminderService.GetReminderByIdAsync(id);
            if (reminder == null)
            {
                return NotFound();
            }
            return View(reminder);
        }

        // POST: Reminder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _reminderService.DeleteReminderAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
