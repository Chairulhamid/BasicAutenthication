using System;
using System.Collections.Generic;
using System.Text;

namespace BasicAuthentication
{
    public class User
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }

        public User()
        {

        }
        public User(string namaDepan, string namaBelakang, string uName, string pass)
        {
            this.firstName = namaDepan;
            this.lastName = namaBelakang;
            this.userName = uName;
            this.password = pass;
        }

        protected static User FirstOrDefault(object p)
        {
            throw new NotImplementedException();
        }
    }
}
