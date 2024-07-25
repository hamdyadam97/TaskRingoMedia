using RingoMedia.Dtos;
using RingoMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingoMedia.Application.IServices
{
    public interface IReminderService
    {
        Task AddReminderAsync(ReminderDTO reminderDto);
        Task<IEnumerable<ReminderDTO>> GetRemindersAsync();
        Task<ReminderDTO> GetReminderByIdAsync(int id);
        Task UpdateReminderAsync(ReminderDTO reminderDto);
        Task DeleteReminderAsync(int id);
    }
}
