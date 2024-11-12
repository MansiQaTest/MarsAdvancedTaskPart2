using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAdvancedTaskPart2.TestModel
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class TestCaseData
    {
        public string TestCase { get; set; }
        public LoginModel Data { get; set; }
    }
}
