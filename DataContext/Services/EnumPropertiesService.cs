using DataContext;
using DataContext.Support;
using Microsoft.EntityFrameworkCore;
using PLSO2018.Entities.Common;
using PLSO2018.Entities.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PLSO2018.DataContext.Services {

	public class EnumPropertiesService : IEnumProperties {

		private static List<EnumTriplet> _EnumLookups = new List<EnumTriplet>();
		private readonly PLSODb context;

		public EnumPropertiesService(PLSODb context) {
			this.context = context;
		}

		public int GetDBID(Enum enumeration) {
			var Found = GetEnumTriplet(enumeration);

			return Found == null ? -1 : Found.Value;
		}

		private void DynamicFillTriplets(SourceTablePairing pairing) {
			if (pairing.Names.Count > 0) {
				foreach (var name in pairing.Names) {
					var Triplet = context.Set<EnumTriplet>()
						.FromSql($"GetEnumTriplet @p0, @p1", pairing.TableName, name)
						.AsNoTracking()
						.ToList();

					if ((Triplet != null) && (Triplet.Count >= 1)) {
						var Result = Triplet.FirstOrDefault();
						Result.EnumerationName = pairing.EnumerationName;
						_EnumLookups.Add(Result);
					}
				}
			} // if we found it in the database
		}

		private SourceTablePairing GetNamesIfHasSourceTable<TEnum>() {
			var Result = new SourceTablePairing();

			var Names = Enum.GetNames(typeof(TEnum));
			var type = typeof(TEnum);
			var attr = type.GetTypeInfo().GetCustomAttributes(typeof(SourceTableAttribute), false).ToList();

			if (attr.Count() > 0) {
				Result.TableName = ((SourceTableAttribute)attr[0]).Name;
				Result.EnumerationName = type.Name;
				Result.Names = Names.ToList();
			}

			return Result;
		}

		private EnumTriplet GetEnumTriplet(Enum enumeration) {
			EnumTriplet Result = null;

			SourceTableAttribute att = enumeration.GetType()
				.GetTypeInfo()
				.GetCustomAttributes<SourceTableAttribute>()
				.FirstOrDefault();

			Type etype = enumeration.GetType();

			if (att != null) {
				Result = (from t in _EnumLookups
									where t.EnumerationName == etype.Name
									&& t.KeyName == enumeration.ToString()
									select t).FirstOrDefault();

				if (Result == null) {
					// Has not already been looked up
					EnumTriplet Lookup = new EnumTriplet();

					var dr = context.Database.ExecuteSqlQuery($"GetEnumTriplet '{att.Name}', '{enumeration}'");
					var reader = dr.DbDataReader;

					while (reader.Read()) {
						Result = new EnumTriplet() {
							EnumerationName = etype.Name,
							KeyName = reader.GetString(2),
							DisplayName = reader.GetString(1),
							Value = reader.GetInt32(0),
						};
					}

					if (Result != null) {
						_EnumLookups.Add(Result);
					} // if we found it in the database
				} // if we have not already looked this value up
			} // if this is a lookup-able Enumeration

			return Result;
		}

		private async Task<EnumTriplet> GetEnumTripletAsync(Enum enumeration) {
			EnumTriplet Result = null;

			SourceTableAttribute att = enumeration.GetType().GetTypeInfo().GetCustomAttributes<SourceTableAttribute>().FirstOrDefault();
			Type etype = enumeration.GetType();

			if (att != null) {
				Result = (from t in _EnumLookups
									where t.EnumerationName == etype.Name
									&& t.KeyName == enumeration.ToString()
									select t).FirstOrDefault();


				if (Result == null) {
					// Has not already been looked up
					EnumTriplet Lookup = new EnumTriplet();

					var Get = await context.Set<EnumTriplet>()
						.FromSql($"GetEnumTriplet @p0, @p1", att.Name, enumeration)
						.AsNoTracking()
						.ToListAsync();
					Result = Get.FirstOrDefault();

					//var dr = await context.Database.ExecuteSqlQueryAsync();
					//var reader = dr.DbDataReader;

					//while (reader.Read()) {
					//	Result = new EnumTriplet() {
					//		EnumerationName = etype.Name,
					//		KeyName = reader.GetString(2),
					//		DisplayName = reader.GetString(1),
					//		Value = reader.GetInt32(0),
					//	};
					//}

					if (Result != null) {
						_EnumLookups.Add(Result);
					} // if we found it in the database
				} // if we have not already looked this value up
			} // if this is a lookup-able Enumeration

			return Result;
		}

	}
}
