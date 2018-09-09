using DataContext.Services;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataContext.Support {

	public static class ExtensionMethods {

		public static ILoggerFactory AddContext(this ILoggerFactory factory, Func<string, LogLevel, bool> filter = null, string connectionString = null) {
			factory.AddProvider(new DbLoggerProvider(filter, connectionString));
			return factory;
		}

		public static ILoggerFactory AddContext(this ILoggerFactory factory, LogLevel minimumLevel, string connectionString) {
			return AddContext(factory, (_, logLevel) => logLevel >= minimumLevel, connectionString);
		}

		public static void LogForce(this ILogger logger, EventId eventId, string message) {
			// By default (in Startup) we are only logging Warning, Error and Critical messages.
			// If something NEEDS to be sent to the database (log.EventLog) then use this extension method
			// Could manually do it without this extension via:
			// logger.Log(LogLevel.Information, 70, "Some message", new Exception(""), (msg, exc) => ("Forced:" + msg));
			// Note: "Forced:" is required and will get stripped off before saving to the database
			logger.Log(LogLevel.Information, eventId, message, new Exception(""), (msg, exe) => ("Forced: " + message));
		}

		// http://stackoverflow.com/a/42932812/373438
		public static RelationalDataReader ExecuteSqlQuery(this DatabaseFacade databaseFacade, string sql, params object[] parameters) {
			var concurrencyDetector = databaseFacade.GetService<IConcurrencyDetector>();

			using (concurrencyDetector.EnterCriticalSection()) {
				var rawSqlCommand = databaseFacade
						.GetService<IRawSqlCommandBuilder>()
						.Build(sql, parameters);

				return rawSqlCommand
						.RelationalCommand
						.ExecuteReader(
								databaseFacade.GetService<IRelationalConnection>(),
								parameterValues: rawSqlCommand.ParameterValues);
			}
		}

		public static async Task<RelationalDataReader> ExecuteSqlQueryAsync(this DatabaseFacade databaseFacade, string sql, CancellationToken cancellationToken = default(CancellationToken), params object[] parameters) {
			var concurrencyDetector = databaseFacade.GetService<IConcurrencyDetector>();

			using (concurrencyDetector.EnterCriticalSection()) {
				var rawSqlCommand = databaseFacade
						.GetService<IRawSqlCommandBuilder>()
						.Build(sql, parameters);

				return await rawSqlCommand
						.RelationalCommand
						.ExecuteReaderAsync(
							databaseFacade.GetService<IRelationalConnection>(),
							parameterValues: rawSqlCommand.ParameterValues,
							cancellationToken: cancellationToken
						);
			}
		}

	}
}
