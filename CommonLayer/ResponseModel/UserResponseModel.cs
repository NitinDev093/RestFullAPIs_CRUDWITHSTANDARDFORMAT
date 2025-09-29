using CommonLayer.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.ResponseModel
{
    public class UserResponseModel : UserRequestModel
    {
        public string UserId { get; set; }
        public string CreatedDate { get; set; }  
        public string UpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
