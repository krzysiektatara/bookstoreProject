using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BADataAccessLibrary.Models
{
    public class VMLogin
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool KeepLoggedIn { get; set; }
    }
}
