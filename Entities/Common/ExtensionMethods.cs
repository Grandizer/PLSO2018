using PLSO2018.Entities.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PLSO2018.Entities.Common {

	public static class ExtensionMethods {

		public static StringBuilder AppendLineFormat(this StringBuilder stringBuilder, string format, params object[] args) {
			return stringBuilder.AppendLine(string.Format(format, args));
		} // AppendLineFormat - Extension Method

		public static bool EqualTo(this AuditableBase a, AuditableBase b, out List<string> changedProperties) {
			bool Result = false;
			changedProperties = new List<string>();

			Type at = GetNonProxyType(a);
			Type bt = GetNonProxyType(b);

			if ((a == null) && (b == null))
				Result = true;
			else if ((a == null) || (b == null))
				Result = false;
			//else if (a.GetType() != b.GetType())
			//	Result = false;
			else if (at != bt)
				Result = false;
			else {
				PropertyInfo[] Props = at.GetProperties();

				foreach (PropertyInfo p in Props.Where(x => x.DeclaringType != typeof(AuditableBase))) {
					string FullTypeName = p.PropertyType.FullName;
					string PropertyName = string.Format("{0}.{1}", p.GetType().GetTypeInfo().Name, p.Name);

					if (p.PropertyType.GetTypeInfo().IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
						FullTypeName = Nullable.GetUnderlyingType(p.PropertyType).FullName;

					if ((Constants.AuditTrail.CommonLists.AuditablePropertyTypes.Contains(FullTypeName)) && (!Constants.AuditTrail.CommonLists.UnAuditedPropertyNames.Contains(PropertyName))) {
						var aValue = p.GetValue(a, null) ?? string.Empty;
						var bValue = p.GetValue(b, null) ?? string.Empty;

						if (aValue.ToString() != bValue.ToString()) {
							changedProperties.Add(p.Name);
							Result = false;
						} // if the properties are different
					} // if this is a simple primitive-ish property and not one we are .Ignore'ing (see AppealConfiguration)
				} // foreach of the Properties on the object and none of the propertis from the AuditableBase class

				Result = changedProperties.Count == 0;
			} // if they are both null

			return Result;
		} // EqualTo

		private static Type GetNonProxyType(object o) {
			Type Result = null;

			if ((o.GetType().GetTypeInfo() != null) && (o.GetType().Namespace == "System.Data.Entity.DynamicProxies"))
				Result = o.GetType().GetTypeInfo().BaseType;
			else
				Result = o.GetType();

			return Result;
		} // GetNonProxyType

	}
}
