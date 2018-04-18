using System;
using System.Collections.Generic;
using System.Text;

namespace PLSO2018.Entities.Support {

	public class PLSOResponse {

		private bool _wasSuccessful;

		public bool WasSuccessful {
			get {
				return _wasSuccessful;
			}
			set {
				_wasSuccessful = value;
				this.Ended = DateTime.Now;
			}
		}
		public List<string> Messages { get; set; }
		public DateTime Started { get; set; } = DateTime.Now;
		public DateTime Ended { get; set; }
		public double Duration => (Ended - Started).TotalMilliseconds;

		public PLSOResponse() {
			this.WasSuccessful = false;
			this.Messages = new List<string>();
		}

		public PLSOResponse(bool wasSuccessful, List<string> messages) {
			this.WasSuccessful = wasSuccessful;
			this.Messages = messages;
		}

		public bool AddMessage(string message) {
			bool Result = false;

			this.Messages.Add(message);
			Result = true;

			return Result;
		} // AddMessage

		public bool AddMessage(string format, params object[] args) {
			bool Result = false;

			this.Messages.Add(string.Format(format, args));
			Result = true;

			return Result;
		} // AddMessage

		public bool AddMessage(Exception exception, string className) {
			bool Result = false;

			this.Messages.Add($"<span class='redError'>Error in {className}</span>");
			this.TransformException(exception);
			Result = true;

			return Result;
		} // AddMessage

		private void TransformException(Exception exception) {
			this.Messages.Add(exception.Message);

			if (exception.InnerException != null)
				TransformException(exception.InnerException);
		} // TransformException

		public string ToHTML() {
			StringBuilder Result = new StringBuilder();

			foreach (string s in this.Messages) {
				Result.AppendFormat($"<span>{s}</span><hr class='tight' />");
			} // foreach of the messages

			return Result.ToString();
		} // ToHTML

	} // PLSOResponse


	public class PLSOResponse<T> : PLSOResponse where T : new() {

		public T Result { get; set; }

		public PLSOResponse() {
			this.WasSuccessful = false;
			this.Result = new T();
			this.Messages = new List<string>();
		}

		public PLSOResponse(bool wasSuccessful, List<string> messages, T result) {
			this.Result = result;
			this.WasSuccessful = wasSuccessful;
			this.Messages = messages;
		} // ServiceResponse - Constructor - Overload

	} // PLSOResponse<T>



	public class PLSOResponseOfByteArray : PLSOResponse {

		public byte[] Result { get; set; }

		public PLSOResponseOfByteArray() {
			this.WasSuccessful = false;
			this.Result = null;
			this.Messages = new List<string>();
			;
		} // DatabaseResponseOfByteArray - Constructor - Overload

		public PLSOResponseOfByteArray(bool wasSuccessful, List<string> messages, byte[] result) {
			this.Result = result;
			this.WasSuccessful = wasSuccessful;
			this.Messages = messages;
		} // DatabaseResponseOfByteArray - Constructor - Overload

	} // PLSOResponseOfByteArray



	public class PLSOResponseOfString : PLSOResponse {

		public string Result { get; set; }

		public PLSOResponseOfString() : base() { }

		public PLSOResponseOfString(bool wasSuccessful, List<string> messages, string result) {
			this.Result = result;
			this.WasSuccessful = wasSuccessful;
			this.Messages = messages;
		} // PLSOResponseOfString - Constructor - Overload

	} // PLSOResponseOfString

}
