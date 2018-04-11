using System;

namespace PLSO2018.Entities.Support {

	[AttributeUsage(AttributeTargets.Enum)]
	public class SourceTableAttribute : Attribute {

		public string Name { get; set; }

		public SourceTableAttribute() {

		}

		public SourceTableAttribute(string name) {
			Name = name;
		}

	}
}
