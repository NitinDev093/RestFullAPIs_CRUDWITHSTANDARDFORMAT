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
        public DateTime CreatedDate { get; set; }  
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
