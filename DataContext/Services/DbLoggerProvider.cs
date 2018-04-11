using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Services {

	public class DbLoggerProvider : ILoggerProvider {

		private readonly Func<string, LogLevel, bool> _filter;
		private string _connectionString;

		public DbLoggerProvider(Func<string, LogLevel, bool> filter, string connectionString) {
			_filter = filter;
			_connectionString = connectionString;
		}

		public ILogger CreateLogger(string categoryName) {
			return new DbLogger(categoryName, _filter, _connectionString);
		}

		public void Dispose() {
			//Dispose(true);
			GC.SuppressFinalize(this);
		}

	}
}
