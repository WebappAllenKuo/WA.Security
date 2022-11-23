using NUnit.Framework;

namespace WA.Security.UnitTests
{
	public class SHA256TextTests
	{
		[TestCase("allen", "ec47860233d36cad00db3ed18c8ed7d57690cb8689caa86cce93ef75d767e7f1")]
		public void Sha245Encrypted(string source, string expected)
		{
			var sut = new SHA256Text(source);

			Assert.AreEqual(expected, sut.EncryptedText);
		}

		[TestCase("allen", "ec47860233d36cad00db3ed18c8ed7d57690cb8689caa86cce93ef75d767e7f1")]
		public void �����૬(string source, string expected)
		{
			SHA256Text sut = source;

			Assert.AreEqual(expected, sut.EncryptedText);
		}

		[TestCase("allen", "ec47860233d36cad00db3ed18c8ed7d57690cb8689caa86cce93ef75d767e7f1")]
		public void Test_ToSha256Text�X�R��k(string source, string expected)
		{
			SHA256Text sut = source.ToSha256Text();

			Assert.AreEqual(expected, sut.ToString());
		}

		[TestCase("ec47860233d36cad00db3ed18c8ed7d57690cb8689caa86cce93ef75d767e7f1")]
		public void �N�v�s�X��r�ഫ��Sha256Text����(string enctryptedText)
		{
			SHA256Text sha256Text = SHA256Text.FromEncryptedText(enctryptedText);

			Assert.AreEqual(enctryptedText, sha256Text.EncryptedText);
		}

		[Test]
		public void EqualsTest()
		{
			var x = new SHA256Text("allen");
			var y = new SHA256Text("allen");

			bool actual = x.Equals(y);

			Assert.IsTrue(actual);
		}

		[Test]
		public void AreEqual_�����X�P�s�X�᪺�r��()
		{
			string plainText = "allen";
			string encryptedText = "ec47860233d36cad00db3ed18c8ed7d57690cb8689caa86cce93ef75d767e7f1";

			bool actual = SHA256Text.AreEqual((SHA256Text)plainText, encryptedText);

			Assert.IsTrue(actual);
		}

		[Test]
		public void AreEqual_���PlainText_object�P�s�X�᪺�r��()
		{
			string plainValue = "allen";
			string encryptedText = "ec47860233d36cad00db3ed18c8ed7d57690cb8689caa86cce93ef75d767e7f1";

			bool actual = SHA256Text
							.AreEqual((PlainText)plainValue,
											SHA256Text.FromEncryptedText(encryptedText));

			Assert.IsTrue(actual);
		}
	}
}