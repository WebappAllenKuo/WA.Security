using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WA.Security;

namespace Demo01
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// register
			var vm = new RegisterVM { Account = "allen", Password = "123", ConfirmPassword = "123"};
			var request = vm.ToRequest();

			new MemberService().Register(request);


			// authentication
			var loginVM = new LoginVM{Account = "allen", PlainPassword = "123"};
			bool isValid = new MemberService().Authenticate(loginVM.ToDto());
			Console.WriteLine(isValid);
		}
	}



	public class MemberService
	{
		private readonly string _salt = "fjsdakl;gfdfdwsgfdsgasdf2345342asdfgfgsg!!@$$#@%";

		/// <summary>
		/// 註冊新會員
		/// </summary>
		/// <param name="request"></param>
		public void Register(RegisterRequest request)
		{
			// 驗證 Account是否唯一, PlainPassword 強度是否過關

			// 建立 Dto,內含加salt之後的 sha256 密碼
			var dto = request.ToDto(this._salt);

			new MemberRepository().Create(dto);
		}

		public bool Authenticate(LoginDto model)
		{
			string account = model.Account;
			string encryptedPassword = (model.PlainPassword + _salt).ToSha256Text().ToString();

			Member entityInDb = new MemberRepository().GetByAccount(account);

			return entityInDb != null && entityInDb.EncryptedPassword.Equals(encryptedPassword);
		}
	}

	public class MemberRepository
	{
		private static List<Member> members = new List<Member>();

		public void Create(RegisterDto model)
		{
			var entity = new Member { Account = model.Account, EncryptedPassword = model.EncryptedPassword};
			members.Add(entity);
		}

		public Member GetByAccount(string account)
		{
			return members.FirstOrDefault(m => m.Account == account);
		}
	}

	public class Member
	{
		public string Account { get; set; }
		public string EncryptedPassword { get; set; }
	}

	public class LoginVM
	{
		public string Account { get; set; }
		public string PlainPassword { get; set; }
	}

	public class LoginDto
	{
		public string Account { get; set; }
		public string PlainPassword { get; set; }
	}

	public static class LoginVMExtensions
	{
		public static LoginDto ToDto(this LoginVM model)
		{
			return new LoginDto {Account = model.Account, PlainPassword = model.PlainPassword};
		}
		}
	public class RegisterVM
	{
		public string Account { get; set; }
		public string Password { get; set; } // 明碼
		public string ConfirmPassword { get; set; } // 確認密碼
	}

	public class RegisterRequest
	{
		public string Account { get; set; }
		public string PlainPassword { get; set; } // 明碼
	}
	public class RegisterDto
	{
		public string Account { get; set; }
		public string EncryptedPassword { get; set; } // 編碼後的Password
	}

	public static class RegisterRequestExtensions
	{
		public static RegisterDto ToDto(this RegisterRequest model, string salt)
			=> new RegisterDto
			{
				Account = model.Account,
				EncryptedPassword = new SHA256Text(model.PlainPassword + salt).ToString()
			};
	}
	public static class RegisterVMExtensions
	{
		public static RegisterRequest ToRequest(this RegisterVM model)
			=> new RegisterRequest
			{
				Account = model.Account,
				PlainPassword = model.Password
			};
	}
}
