using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArea.Entities
{
    public class Member
    {
        public string MemberId { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public int Role { get; set; }

        public DateTime JoinDate { get; set; }
    }
}
