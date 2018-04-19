using System.Collections.Generic;

namespace PLSO2018.Entities.Common {

#pragma warning disable CA1815 // Override equals and operator equals on value types
#pragma warning disable CA1034 // Nested types should not be visible

	public struct Constants {

		public struct Applicaton {

			public static readonly string LocalEnvironment = "Local";

		}

		public struct AuditTrail {

			/// <summary>
			/// This property wraps ALL changes (each entry in the .Data field)
			/// </summary>
			public static readonly string DataPrefix = "<audits>";
			public static readonly string DataSuffix = "</audits>";

			/// <summary>
			/// 0 = The property that changed, 1 = The original value of the property, 2 = The new value of the property
			/// </summary>
			public static readonly string PropertyChangedFormat = "<audit><prop>{0}</prop> from <old>'{1}'</old> to <new>'{2}'</new></audit>";
			public static readonly string PropertyFormat = "<audit><prop>{0}</prop> set to <new>'{1}'</new></audit>";
			public static readonly string EntityCreationFormat = "<audit>Initial creation of {0}</audit>";

			public static partial class CommonLists {

				public static readonly List<string> AuditablePropertyTypes = new List<string>() {
					"System.Boolean", "System.Int16", "System.Int32", "System.Int64", "System.DateTime", "System.DateTimeOffset", "System.String", "System.Decimal"
				};

				public static readonly List<string> UnAuditedPropertyNames = new List<string>() {

				};

			} // CommonLists - Class

		} // AuditTrail

		public struct Claims {

			public struct User {

				public static readonly string ID = "User.ID";
				public static readonly string Number = "User.Number";
				public static readonly string FirstName = "User.FirstName";
				public static readonly string LastName = "User.LastName";
				public static readonly string EMail = "User.EMail";
				public static readonly string TimeZoneID = "User.TimeZoneID"; // From the ref.TimeZone table
				public static readonly string TimeZoneLookupKey = "User.TimeZoneLookupKey";

			} // User

			public struct Application {

				public static readonly string User = "Application.User"; // Everyone
				public static readonly string IsAdministrator = "Application.Administrator"; // Can do everything - Should NEVER be given out to anyone outside of Avantia

			} // Application

		} // Claims

		public struct LoggingEvents {

			public const int GENERATE_ITEMS = 1000;
			public const int LIST_ITEMS = 1001;
			public const int GET_ITEM = 1002;
			public const int INSERT_ITEM = 1003;
			public const int UPDATE_ITEM = 1004;
			public const int DELETE_ITEM = 1005;

			public const int GET_ITEM_NOTFOUND = 4000;
			public const int UPDATE_ITEM_NOTFOUND = 4001;

		} // LoggingEvents

		public struct TemporalTables {

			/// <summary>
			/// Use argument 0 as the Schema and argument 1 as the Real Table Name (not a History named version) and 2 is the
			/// alternate schema name for the history table (like log).
			/// https://msdn.microsoft.com/en-us/magazine/mt795184.aspx?f=255&MSPPError=-2147217396
			/// </summary>
			public const string SqlSystemVersionedDbFormat = @"
				ALTER TABLE
					{0}.{1}
				ADD
					SysStartTime datetime2(0)
				GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT DF_{1}_SysStart DEFAULT SYSUTCDATETIME(),
					SysEndTime datetime2(0)
				GENERATED ALWAYS AS ROW END HIDDEN CONSTRAINT DF_{1}_SysEnd DEFAULT CONVERT(datetime2 (0), '9999-12-31 23:59:59'),
					PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime)
				ALTER TABLE
					{0}.{1}
				SET
					(SYSTEM_VERSIONING = ON (HISTORY_TABLE = {2}.{1}History))";
		}

	}

#pragma warning restore CA1034 // Nested types should not be visible
#pragma warning restore CA1815 // Override equals and operator equals on value types

}
