using CommonLayer.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.IRepositoryLayer
{
    public interface IUserRepositoyLayer
    {
        int CreateUser(UserRequestModel user);
    }
}
