using RingoMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingoMedia.Application.Contract
{
    public interface IReminderRepository
    {
        Task AddReminderAsync(Reminder reminder);
        Task<IEnumerable<Reminder>> GetRemindersAsync();
        Task<Reminder> GetReminderByIdAsync(int id);
        Task UpdateReminderAsync(Reminder reminder);
        Task DeleteReminderAsync(int id);
    }
}
