using System.Collections.Generic;

namespace Services.Communication.Users.Requests
{
    public class GetUsersByIdsRequest
    {
        public List<long> Ids { get; set; }
    }
}
