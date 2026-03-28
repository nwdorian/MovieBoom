namespace Application.Contracts.Emails;

public record class EmailRequest(string EmailTo, string Subject, string Body);
