using AutoMapper;
using RingoMedia.Application.Contract;
using RingoMedia.Application.IServices;
using RingoMedia.Dtos;
using RingoMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingoMedia.Application.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public ReminderService(IReminderRepository reminderRepository, IEmailService emailService, IMapper mapper)
        {
            _reminderRepository = reminderRepository;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task AddReminderAsync(ReminderDTO reminderDto)
        {
            var reminder = _mapper.Map<Reminder>(reminderDto);
            await _reminderRepository.AddReminderAsync(reminder);

            // Send email notification
            await _emailService.SendEmailAsync(
                reminderDto.mail,
                "New Reminder Created",
                $"Reminder '{reminder.Title}' scheduled for {reminder.DateTime} has been created."
            );
        }

        public async Task<IEnumerable<ReminderDTO>> GetRemindersAsync()
        {
            var reminders = await _reminderRepository.GetRemindersAsync();
            return _mapper.Map<List<ReminderDTO>>(reminders);
        }
        public async Task<ReminderDTO> GetReminderByIdAsync(int id)
        {
            var reminder = await _reminderRepository.GetReminderByIdAsync(id);
            return _mapper.Map<ReminderDTO>(reminder);
        }
        public async Task UpdateReminderAsync(ReminderDTO reminderDto)
        {
            var reminder = _mapper.Map<Reminder>(reminderDto);
            await _reminderRepository.UpdateReminderAsync(reminder);
            await _emailService.SendEmailAsync(
                reminderDto.mail,
                "New Reminder updated",
                $"Reminder '{reminder.Title}' scheduled for {reminder.DateTime} has been created."
            );
        }

        public async Task DeleteReminderAsync(int id)
        {
            await _reminderRepository.DeleteReminderAsync(id);
        }

    }
}
