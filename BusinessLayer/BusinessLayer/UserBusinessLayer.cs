using BusinessLayer.IBusinessLayer;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.IRepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityLayer;

namespace BusinessLayer.BusinessLayer
{
    public class UserBusinessLayer : IUserBusinessLayer
    {
        private readonly IUserRepositoyLayer _userRepositoryLayer;

        public UserBusinessLayer(IUserRepositoyLayer userRepositoryLayer)
        {
            _userRepositoryLayer = userRepositoryLayer;
        }

        public ApiResponse<string> CreateUser(UserRequestModel user)
        {
            ApiResponse<string> response;
            user.PasswordHash= EncodeDecodeHelper.EncodePasswordToBase64(user.PasswordHash);

            int Id = _userRepositoryLayer.CreateUser(user);

            if (Id > 0) {
                response = new ApiResponse<string>()
                {
                    Data = EncodeDecodeHelper.EncodePasswordToBase64(Id.ToString()),

                    IsSuccess = true,
                    Message = "User created successfully"
                };
                return response;
            }
            response = new ApiResponse<string>()
            {
                Data = string.Empty,
                IsSuccess = false,
                Message = "Unable to create user. "
            };
            return response;
        }
    }
}
