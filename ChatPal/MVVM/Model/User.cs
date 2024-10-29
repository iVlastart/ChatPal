using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatPal.MVVM.Model
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
    }
}
