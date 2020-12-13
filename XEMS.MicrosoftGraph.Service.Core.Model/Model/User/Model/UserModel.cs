using System;
using System.Linq;
using XEMS.MicrosoftGraph.Service.Core.Domain.Enums;

namespace XEMS.MicrosoftGraph.Service.Core.Model.Model.User.Model
{
    public class UserModel
    {
        public UserModel()
        {
        }

        public UserModel(Microsoft.Graph.User user)
        {
            var accec = SetAccessLevel(user.JobTitle);
            var name = user.DisplayName.Split(' ');

            GUID = user.Id;
            Login = user.Mail;
            LastName = name.ElementAtOrDefault(0);
            FirstName = name.ElementAtOrDefault(1);
            FatherName = name.ElementAtOrDefault(2);
            Phone = user.MobilePhone;
            Address = user.StreetAddress;
            Birthday = user.Birthday.Value.DateTime;
            AccessLevel = accec;
            Department = accec == AccessLevel.Teacher
                ? string.Concat(user.Department, $", {user.JobTitle}")
                : user.Department;
        }

        public string GUID { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Department { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public AccessLevel AccessLevel { get; set; }

        private AccessLevel SetAccessLevel(string JobTitle)
        {
            var title = JobTitle.ToLower();
            if (title.Contains("студент"))
                return AccessLevel.Student;
            if (title.Contains("админ"))
                return AccessLevel.Admin;
            return AccessLevel.Teacher;
        }
    }
}