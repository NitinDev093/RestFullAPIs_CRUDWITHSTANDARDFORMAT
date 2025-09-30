using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.IRepositoryLayer
{
    public interface IUserRepositoyLayer
    {
        int CreateUser(UserRequestModel user);
        DataTable GetUsers();
        DataTable getusersById(string id);
        int DeleteUsers(int id);
    }
}
