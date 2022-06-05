using Domain.Modals.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modals.Response
{
    public class ChangePasswordReponse
    {
        public UserToGet User { get; set; } = new();
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
