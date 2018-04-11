using PLSO2018.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataContext.Services {

	public class LogSQLHelper {

		private string ConnectionString { get; set; }

		public LogSQLHelper(string connectionString) {
			ConnectionString = connectionString;
		}

		private bool ExecuteNonQuery(string commandString, List<SqlParameter> parameters) {
			bool Result = false;

			using (SqlConnection conn = new SqlConnection(ConnectionString)) {
				if (conn.State != ConnectionState.Open)
					conn.Open();

				using (SqlCommand command = new SqlCommand(commandString, conn)) {
					command.Parameters.AddRange(parameters.ToArray());
					int count = command.ExecuteNonQuery();
					Result = count > 0;
				}
			}

			return Result;
		}

		public bool InsertLog(EventLog log) {
			string command = @"
				INSERT INTO [log].[EventLog] (
					[EventID], [LogLevel], [Message], [StackTrace], [Occurred]
				) VALUES (
					@EventID, @LogLevel, @Message, @StackTrace, @Occurred
				)";

			var SID = new SqlParameter("SurveyorID", log.SurveyorID ?? 1) {
				DbType = DbType.Int32
			};


			List<SqlParameter> parameters = new List<SqlParameter> {
				new SqlParameter("@EventID", log.EventID ?? null),
				//SID,
				new SqlParameter("LogLevel", log.LogLevel),
				new SqlParameter("Message", log.Message ?? ""),
				///new SqlParameter("StackTrace", log.StackTrace ?? DBNull.Value),
				new SqlParameter("Occurred", log.Occurred)
			};

			if (string.IsNullOrWhiteSpace(log.StackTrace))
				parameters.Add(new SqlParameter("StackTrace", DBNull.Value));
			else
				parameters.Add(new SqlParameter("StackTrace", log.StackTrace));

			return ExecuteNonQuery(command, parameters);
		}

	}
}
