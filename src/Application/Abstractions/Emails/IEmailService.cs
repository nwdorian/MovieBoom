using Application.Contracts.Emails;

namespace Application.Abstractions.Emails;

public interface IEmailService
{
    Task SendEmail(EmailRequest request);
}
