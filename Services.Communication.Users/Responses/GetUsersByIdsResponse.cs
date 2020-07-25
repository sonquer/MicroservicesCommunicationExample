using System.Collections.Generic;
using Services.Communication.Users.Models;

namespace Services.Communication.Users.Responses
{
    public class GetUsersByIdsResponse
    {
        public List<UserModel> Users { get; set; }
    }
}
