using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.RequestModel
{
    public class UserRequestModel
    {
        public string FirstName { get; set; }   
        public string LastName { get; set; }    
        public string Gender { get; set; }      
        public string? DOB { get; set; }     
        public string Email { get; set; }       
        public string Phone { get; set; }    
        public string Username { get; set; }   
        public string PasswordHash { get; set; }
    }
}
