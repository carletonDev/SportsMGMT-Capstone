
namespace SportsMGMTCommon
{
    using System;
    public class Users
    {
        public static readonly NullUser Null=NullUserInst;
        private static NullUser NullUserInst { get => new NullUser(); }
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

    public class NullUser:Users
    {
        public NullUser()
        {
            UserID = 0;
            TeamID = 0;
            ContractID = 0;
            FirstName = "No FirstName Provided";
            LastName = "No Last Name Provided";
            FullName = "No Full Name";
            UserModified = 0;
            RoleID = 0;
            Address = "No Address Provided";
            Email = "No Email Provided";
            Phone = "No Phone Provided";
            UserName = "UserName";
            Password = "Password";
            InjuryStatus = false;
            InjuryDescription = "No Injuries Description provided";
            ContractDuration = 0;
            ContractStart = DateTime.Now;

        }
    }
}
