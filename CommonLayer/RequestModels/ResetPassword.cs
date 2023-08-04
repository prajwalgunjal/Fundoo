using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModels
{
    public class ResetPassword
    {
        public string Password { get; set; }    
        public string ConfirmResetPassword { get; set; }
    }
}
