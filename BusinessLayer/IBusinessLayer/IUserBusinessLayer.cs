using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBusinessLayer
{
    public interface IUserBusinessLayer
    {
        ApiResponse<string> CreateUser(UserRequestModel user);
        ApiResponse<List<UserResponseModel>> GetUsers();
        ApiResponse<UserResponseModel> getusersById(string id);
        ApiResponse<string> DeleteUsers(string id);
        ApiResponse<string> Login(string username, string password);
    }
}
