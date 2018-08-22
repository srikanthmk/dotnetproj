using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Future_is_mine.Models
{
    public class Employee
    {
        //  [CandidateID]
        //,[FirstName]
        //,[LastName]
        //,[City]
        //,[Email]
        //,[password]
        //,[classification]
        //,[country]
        //,[experience]
        //,[Active]
        //,[verified]
        //,[EmailVerified]
        //,[RegistrationDate]
        //,[profilesummary]
        //,[CurrentRole]

        public int EmployeeId { get; set; }
        public  string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public string classification { get; set; }
        public bool Active { get; set; }
        public bool EmailVerified { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string profilesummary { get; set; }
        public string CurrentRole { get; set; }

        public string country { get;set; }

        public string Experience { get; set; }

        public bool verified { get; set; }

        


    }
}