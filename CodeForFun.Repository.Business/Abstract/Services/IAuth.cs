using CodeForFun.Repository.Entities.Concrete;
using Microsoft.IdentityModel.Tokens;

namespace CodeForFun.Repository.Business.Abstract.Services
{
	public interface IAuth
	{
		SecurityToken Login(string username, string password);
		string Register(User ss, string password);

	}
}
