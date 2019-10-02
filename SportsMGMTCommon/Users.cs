
namespace SportsMGMTCommon
{
    using System;
    public class Users
    {
        //users common object
        public int UserID { get; set; }
        public int TeamID { get; set; }
        public int ContractID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int UserModified { get; set; }
        public int RoleID { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool InjuryStatus { get; set; }
        public string InjuryDescription { get; set; }
        public int ContractDuration { get; set; }
        public DateTime ContractStart { get; set; }
    }
}
