namespace WA.Security
{
	public static class StringExtensions
	{
		public static SHA256Text ToSha256Text(this string plainText)
			=> (SHA256Text)plainText;
	}
}