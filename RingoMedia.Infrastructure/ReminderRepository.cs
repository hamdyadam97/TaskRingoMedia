﻿using Microsoft.EntityFrameworkCore;
using RingoMedia.Application.Contract;
using RingoMedia.Context;
using RingoMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingoMedia.Infrastructure
{
    public class ReminderRepository: IReminderRepository
    {
        private readonly RingoMediaDbContext _context;

        public ReminderRepository(RingoMediaDbContext context)
        {
            _context = context;
        }

        public async Task AddReminderAsync(Reminder reminder)
        {
            _context.Reminders.Add(reminder);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reminder>> GetRemindersAsync()
        {
            return await _context.Reminders.ToListAsync();
        }

        public async Task<Reminder> GetReminderByIdAsync(int id)
        {
            return await _context.Reminders.FindAsync(id);
        }

        public async Task UpdateReminderAsync(Reminder reminder)
        {
            _context.Entry(reminder).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReminderAsync(int id)
        {
            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder != null)
            {
                _context.Reminders.Remove(reminder);
                await _context.SaveChangesAsync();
            }
        }
    }

}