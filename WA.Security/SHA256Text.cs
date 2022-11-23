using System;
using System.Security.Cryptography;
using System.Text;

namespace WA.Security
{
	public class SHA256Text
	{
		public string EncryptedText { get; private set; }
		public SHA256Text(string plainText)
		{
			// ref https://www.cnblogs.com/yechangzhong-826217795/p/9984331.html
			byte[] bytes = Encoding.UTF8.GetBytes(plainText);
			byte[] hash = SHA256Managed.Create().ComputeHash(bytes);

			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < hash.Length; i++)
			{
				builder.Append(hash[i].ToString("x2"));
			}

			EncryptedText= builder.ToString();
		}

		private SHA256Text()
		{ }

		public override int GetHashCode()
		{
			return this.EncryptedText.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			SHA256Text x = this;
			SHA256Text y = obj as SHA256Text;
			if (x == null && y == null) return true;
			if (x == null || y == null) return false;

			return x.EncryptedText.Equals(y.EncryptedText);
		}

		public override string ToString()
			=> this.EncryptedText;

		public static SHA256Text FromEncryptedText(string encryptedText)
			=> new SHA256Text() { EncryptedText = encryptedText };

		/// <summary>
		/// 用來將使用者輸入明碼與db中存放的己編碼字串比對是否相符
		/// </summary>
		/// <param name="plainStringOrSha256Text">若傳入明碼字串，會自動轉型為 SHA256Text</param>
		/// <param name="encryptedText">己經過 SHA256 編碼的字串</param>
		/// <returns></returns>
		public static bool AreEqual(SHA256Text plainStringOrSha256Text, string encryptedText)
			=> plainStringOrSha256Text.Equals(SHA256Text.FromEncryptedText(encryptedText));

		public static bool AreEqual(PlainText plainText, SHA256Text shaText)
			=> ((SHA256Text)plainText).Equals(shaText);

		public static implicit operator SHA256Text(string plainText)
			=> new SHA256Text(plainText);
	}
}