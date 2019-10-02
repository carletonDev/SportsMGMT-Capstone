namespace SportsMGMTBLL
{
    using SportsMGMTCommon;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SportsMGMTDataAccess;
    public class ContractsBLL
    {
        //call the DA layer to Read contracts and store in a list
        public List<Contracts> GetContracts()
        {
            ContractsDataAccess contractsData = new ContractsDataAccess();
            List<Contracts> contractList = contractsData.GetContracts();

            return contractList;
        }
        //call the DA layer to Create a new contract in the list
        public bool CreateContracts(Contracts contract)
        {
            bool check;
            try
            {
                ContractsDataAccess contractsData = new ContractsDataAccess();
                 check=contractsData.CreateContract(contract);
            }
            catch(Exception ex)
            {
                ExeceptionDataAccess ExceptionDA = new ExeceptionDataAccess();
                ExceptionDA.StoreExceptions(ex);
                check=false;
            }
            return check;
        }

        //call the DA layer to Update Contracts
        public bool UpdateContract(Contracts contract)
        {
            bool check;
            try
            {
                ContractsDataAccess contractsData = new ContractsDataAccess();
                check = contractsData.UpdateContract(contract);
            }
            catch (Exception ex)
            {
                ExeceptionDataAccess ExceptionDA = new ExeceptionDataAccess();
                ExceptionDA.StoreExceptions(ex);
                check = false;
            }
            return check;
        }

        //call the DA layer to Delete Contracts by Name
        public bool DeleteContracts(Contracts contract)
        {
            bool check;
            try
            {
                ContractsDataAccess contractsData = new ContractsDataAccess();
                check = contractsData.DeleteContracts(contract);
            }
            catch (Exception ex)
            {
                ExeceptionDataAccess ExceptionDA = new ExeceptionDataAccess();
                ExceptionDA.StoreExceptions(ex);
                check = false;
            }
            return check;
        }
    }
}
