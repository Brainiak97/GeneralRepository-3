using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.BLL.Dto
{
    /// <summary>
    /// DTO для отправки произвольного электронного письма.
    /// </summary>
    public class SendEmailDto
    {
        /// <summary>
        /// Адрес электронной почты получателя.
        /// </summary>
        public required string To { get; set; }

        /// <summary>
        /// Тема письма.
        /// </summary>
        public required string Subject { get; set; }

        /// <summary>
        /// Тело письма в формате HTML.
        /// </summary>
        public required string Body { get; set; }
    }
}
