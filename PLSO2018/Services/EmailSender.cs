using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace PLSO2018.Website.Services {

	// This class is used by the application to send email for account confirmation and password reset.
	// For more details see https://go.microsoft.com/fwlink/?LinkID=532713
	public class EmailSender : IEmailSender {

		public AuthMessageSenderOptions Options { get; }

		public EmailSender(IConfiguration configuration) {
			Options = new AuthMessageSenderOptions {
				SendGridKey = configuration.GetSection("SendGrid")["Key"],
				SendGridUser = configuration.GetSection("SendGrid")["User"],
			};
		}

		public Task Execute(string apiKey, string subject, string message, string email) {
			var client = new SendGridClient(apiKey);
			var msg = new SendGridMessage() {
				From = new EmailAddress("KevinGrigsby@hotmail.com", "Kevin Grigsby"),
				Subject = subject,
				PlainTextContent = message,
				HtmlContent = message,
			};
			msg.AddTo(new EmailAddress(email));

			// Disable click tracking
			msg.TrackingSettings = new TrackingSettings {
				ClickTracking = new ClickTracking { Enable = false }
			};

			return client.SendEmailAsync(msg);
		}

		public Task SendEmailAsync(string email, string subject, string message) {
			return Execute(Options.SendGridKey, subject, message, email);
		}

	}
}
