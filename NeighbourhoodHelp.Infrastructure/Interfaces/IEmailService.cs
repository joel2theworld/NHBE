using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeighbourhoodHelp.Model.DTOs;

namespace NeighbourhoodHelp.Infrastructure.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDto emailDto);
        Task SendForgotPasswordEmailAsync(EmailDto request);
        Task SendEmailToAgentForErrandCreated(EmailDto createErrandDto);
    }
}
