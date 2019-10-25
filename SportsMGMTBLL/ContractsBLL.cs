namespace SportsMGMTBLL
{
    using SportsMGMTCommon;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SportsMGMTDataAccess;
    using Interfaces.IBusinessLogic;
    using Interfaces.IDataAccess;

    public class ContractsBLL:IContracts
    {
        //call the DA layer to Read contracts and store in a list
        IContractsDataAccess contractsData;
       IExceptions ExceptionDA;

        public ContractsBLL(IContractsDataAccess contract,IExceptions exceptions)
        {
            contractsData = contract;
            ExceptionDA = exceptions;
        }
        public List<Contracts> GetContracts()
        {

            List<Contracts> contractList = contractsData.GetContracts();

            return contractList;
        }
        //call the DA layer to Create a new contract in the list
        public bool CreateContracts(Contracts contract)
        {
            bool check;
            try
            {

                 check=contractsData.CreateContract(contract);
            }
            catch(Exception ex)
            {

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

                check = contractsData.UpdateContract(contract);
            }
            catch (Exception ex)
            {
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
                check = contractsData.DeleteContracts(contract);
            }
            catch (Exception ex)
            {
                ExceptionDA.StoreExceptions(ex);
                check = false;
            }
            return check;
        }
    }
}
