namespace SportsMGMTCommon
{
    public class Roles
    {
        //create properties for Database Object Roles
        public static NullRoles Null = NullRolesInst;
        private static NullRoles NullRolesInst { get => new NullRoles(); }
       public int RoleID { get; set; }
       public string RoleType { get; set; }
    }
    public class NullRoles : Roles
    {
        public NullRoles()
        {
            RoleID = 0;
            RoleType = "Player";
        }
    }
    public enum Role
    {
        Admin=1,
        Coach=2,
        Player=3,
        Cheerleader=4
    }
}
