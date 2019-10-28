
namespace SportsMGMTCommon
{
    public class Contracts
    {
        //instantiate NullContracts singleton for default values
        public static NullContracts Null = NullContractInst;
        private static NullContracts NullContractInst { get => new NullContracts(); }
        //create Class Properties for Database Object Contracts
        public int ContractID { get; set; }
        public string ContractType { get; set; }
        public decimal Salary { get; set; }
    }
    public class NullContracts : Contracts
    {
        public NullContracts()
        {
            ContractID = 0;
            ContractType = "No Contract";
            Salary = 0.0M;
        }
    }
}
