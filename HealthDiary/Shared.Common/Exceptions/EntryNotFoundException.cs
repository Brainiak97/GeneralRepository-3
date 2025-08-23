namespace Team3.HealthDiary.Shared.Common.Exceptions
{
	/// <summary>
	/// Возникает, если не зайдена запись (в БД или другом источнике)
	/// </summary>
	public class EntryNotFoundException : Exception
	{
		public EntryNotFoundException( string? message )
			: base( message )
		{
		}

		public EntryNotFoundException( string? message, Exception? innerException )
			: base( message, innerException )
		{
		}
	}
}
