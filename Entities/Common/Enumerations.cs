using PLSO2018.Entities.Support;

namespace PLSO2018.Entities.Common {

	[SourceTable("AuditAction")]
	public enum AuditActionTypes {
		Add = 1,
		Update,
		Delete,
		ManualDelete,
	}

}
