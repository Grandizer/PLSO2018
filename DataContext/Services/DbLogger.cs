using Microsoft.Extensions.Logging;
using PLSO2018.Entities;
using System;

namespace DataContext.Services {

	public class DbLogger : ILogger {

		private string _categoryName;
		private Func<string, LogLevel, bool> _filter;
		private LogSQLHelper _helper;

		public DbLogger(string categoryName, Func<string, LogLevel, bool> filter, string connectionString) {
			_categoryName = categoryName;
			_filter = filter;
			_helper = new LogSQLHelper(connectionString);
		}

		public IDisposable BeginScope<TState>(TState state) {
			return null;
		}

		public bool IsEnabled(LogLevel logLevel) {
			return (_filter == null || _filter(_categoryName, logLevel));
		}

		public void Log<TState>(LogLevel logLevel, EventId eventID, TState state, Exception exception, Func<TState, Exception, string> formatter) {
			bool WasForced = false;

			if (formatter(state, exception).StartsWith("Forced:"))
				WasForced = true;

			if (!IsEnabled(logLevel) && (WasForced == false))
				return;

			if (formatter == null)
				throw new ArgumentNullException(nameof(formatter));

			var message = formatter(state, exception);
			var stackTrace = "";

			if (string.IsNullOrEmpty(message))
				return;

			if (WasForced)
				message = state.ToString();

			if ((exception != null) && (exception.ToString() != "System.Exception")) {
				message += "\n" + exception.Message;
				stackTrace = exception.StackTrace;
			}

			EventLog eventLog = new EventLog {
				Message = message,
				EventID = eventID.Id,
				LogLevel = logLevel.ToString(),
				Occurred = DateTime.UtcNow,
				SurveyorID = null,
				//SurveyorID = 0 // TODO: See if we can get the Surveyor currently logged in from the Context
			};

			_helper.InsertLog(eventLog);
		}

	}
}
