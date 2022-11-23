using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WA.Security
{
	public class PlainText
	{
		public string Value { get; }

		public PlainText(string plainText)
		{
			Value = plainText;
		}

		public override string ToString()
			=> this.Value;

		public static implicit operator PlainText(string plainText)
			=> new PlainText(plainText);

		public static implicit operator string(PlainText plainTextContext)
			=> plainTextContext.Value;

		public static implicit operator SHA256Text(PlainText plainText)
			=> new SHA256Text(plainText.ToString());
	}
}
