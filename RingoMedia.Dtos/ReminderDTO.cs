using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RingoMedia.Dtos
{
    public class ReminderDTO
    {
        public int? Id { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
        public string? mail { get; set; }
    }
}
