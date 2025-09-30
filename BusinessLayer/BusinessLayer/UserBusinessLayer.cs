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
            ApiResponse<string> response = new ApiResponse<string>();
            user.PasswordHash = EncodeDecodeHelper.EncodePasswordToBase64(user.PasswordHash);
            int Id = _userRepositoryLayer.CreateUser(user);
            if (Id > 0)
            {
                {
                    response.Data = EncodeDecodeHelper.EncodePasswordToBase64(Id.ToString());
                    response.IsSuccess = true;
                    response.Message = "User created successfully";
                }
                EmailHelper.SendEmail(_configuration, user.Email, "RestFull APIs Regerstion completed.", "Hello, your reference ID is:" + Id);
                return response;
            }
            else
            {
                response.Data = null;
                response.IsSuccess = false;
                response.Message = "User creation failed.";
            }
            return response;
        }

        public ApiResponse<List<UserResponseModel>> GetUsers()
        {
            ApiResponse<List<UserResponseModel>> response = new ApiResponse<List<UserResponseModel>>();
            List<UserResponseModel> Items = new List<UserResponseModel>();
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

                foreach (DataRow data in result.Rows)
                {
                    UserResponseModel item = new UserResponseModel();
                    item.UserId = EncodeDecodeHelper.EncodePasswordToBase64(data["UserId"].ToString());
                    item.FirstName = data["FirstName"].ToString();
                    Items.Add(item);
                }
                response.Data = Items;
                response.IsSuccess = true;
                response.Message = "User list fetched successfully.";
            }
            else
            {

                response.Data = null;
                response.IsSuccess = false;
                response.Message = "No data found.";
            }
            return response;
        }

        public ApiResponse<UserResponseModel> getusersById(string id)
        {
            ApiResponse<UserResponseModel> response = new ApiResponse<UserResponseModel>();
            UserResponseModel user = new UserResponseModel();
            if (string.IsNullOrEmpty(id))
            {
                response.IsSuccess = false;
                response.Message = "Bad request";
                return response; ;
            }
            id = EncodeDecodeHelper.DecodeFrom64(id);
            DataTable result = _userRepositoryLayer.getusersById(id);
            if (result != null && result.Rows.Count > 0)
            {
                user.UserId = result.Rows[0]["UserId"].ToString();
                user.PasswordHash = EncodeDecodeHelper.DecodeFrom64(result.Rows[0]["PasswordHash"].ToString());
                user.FirstName = result.Rows[0]["FirstName"].ToString();
                user.LastName = result.Rows[0]["LastName"].ToString();
                user.Email = result.Rows[0]["Email"].ToString();
                user.Phone = result.Rows[0]["Phone"].ToString();
                user.DOB = result.Rows[0]["DOB"].ToString();
                user.CreatedDate = Convert.ToString(result.Rows[0]["CreatedDate"]);
                user.UpdatedDate = Convert.ToString(result.Rows[0]["UpdatedDate"]);
                user.IsActive = Convert.ToBoolean(result.Rows[0]["IsActive"]);
                user.Username = result.Rows[0]["Username"].ToString();
                user.Gender = result.Rows[0].ToString();

                response.Data = user;
                response.IsSuccess = true;
                response.Message = "User data fetched successfully.";
            }
            else
            {
                response.Data = null;
                response.IsSuccess = false;
                response.Message = "No data found.";
            }
            return response;

        }

        public ApiResponse<string> DeleteUsers(string id)
        {
            ApiResponse<string> response= new ApiResponse<string>();
            if (string.IsNullOrEmpty(id))
            {
                response.IsSuccess = false;
                response.Message = "Bad request";
            }
            id= EncodeDecodeHelper.DecodeFrom64(id);
            int result = _userRepositoryLayer.DeleteUsers(Convert.ToInt32(id));
            if (result>0)
            {
                response.IsSuccess = true;
                response.Message = "User deleted successfully.";
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "User deletion failed.";
            }
            return response;
        }
    }
}
