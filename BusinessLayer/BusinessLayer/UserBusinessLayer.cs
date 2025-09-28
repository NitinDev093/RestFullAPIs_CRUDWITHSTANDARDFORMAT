using BusinessLayer.IBusinessLayer;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RepositoryLayer.IRepositoryLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using UtilityLayer;

namespace BusinessLayer.BusinessLayer
{
    public class UserBusinessLayer : IUserBusinessLayer
    {
        private readonly IUserRepositoyLayer _userRepositoryLayer;
        private readonly IConfiguration _configuration;
    
        public UserBusinessLayer(IUserRepositoyLayer userRepositoryLayer, IConfiguration configuration)
        {
            _userRepositoryLayer = userRepositoryLayer;
            _configuration = configuration;

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
                EmailHelper.SendEmail(_configuration,user.Email,"RestFull APIs Regerstion completed.", "Hello, your reference ID is:"+ Id);
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

        public ApiResponse<List<UserResponseModel>> GetUsers()
        {
            ApiResponse<List<UserResponseModel>> response;
            List <UserResponseModel> Items = new List<UserResponseModel>();
            DataTable result = _userRepositoryLayer.GetUsers();
            if (result.Rows.Count > 0)
            {
                //string SerializeObject = JsonConvert.SerializeObject(result);
                //Items = JsonConvert.DeserializeObject<List<UserResponseModel>>(SerializeObject);

                //foreach (var item in Items)
                //{
                //    item.UserId = EncodeDecodeHelper.EncodePasswordToBase64(item.UserId.ToString());
                //    item.PasswordHash = EncodeDecodeHelper.DecodeFrom64(item.PasswordHash);
                //}


                //for (int i = 0; i <= Items.Count; i++) {
                //    Items[i].UserId = EncodeDecodeHelper.EncodePasswordToBase64(Items[i].UserId.ToString());
                //}

                foreach (DataRow data in result.Rows) {
                    UserResponseModel item = new UserResponseModel()
                    {
                        UserId = EncodeDecodeHelper.EncodePasswordToBase64(data["UserId"].ToString()),
                        FirstName = data["FirstName"].ToString()
                    };

                    Items.Add(item);
                }


                response = new ApiResponse<List<UserResponseModel>>()
                {
                    Data = Items,
                    IsSuccess = true,
                    Message = "User list fetched successfully."
                };
            }
            else { 

                response = new ApiResponse<List<UserResponseModel>>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "No data found."
                };
            }

            return response;








        }
    }
}
