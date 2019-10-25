

namespace SportsMGMTDataAccess
{
    using Interfaces.IDataAccess;
    using SportsMGMTCommon;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    //creates the connection for contracts  to CRUD
    public class ContractsDataAccess:IContractsDataAccess
    {
        public string Connection = AppSettings.Default.ConnectionString;
        //Store all contracts in the database in a list
        public List<Contracts> GetContracts()
        {
            //make a list to store contracts
            List<Contracts> getContracts = new List<Contracts>();
            //try to populate the list
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetContracts", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        con.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Contracts contract = new Contracts();
                                contract.ContractID = (int)reader["contractID"];
                                contract.ContractType = (string)reader["contract_type"];
                                contract.Salary = (decimal)reader["salary"];
                                getContracts.Add(contract);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExeceptionDataAccess exception = new ExeceptionDataAccess();
                exception.StoreExceptions(ex);
            }
            return getContracts;
        }
        //Create Contracts
        public bool CreateContract(Contracts contract)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_CreateContract", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@type", contract.ContractType);
                        command.Parameters.AddWithValue("@salary",contract.Salary);
                        con.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ExeceptionDataAccess exception = new ExeceptionDataAccess();
                exception.StoreExceptions(ex);
                return false;
            }


            return true;
        }
        //Update Contracts By Id
        public bool UpdateContract(Contracts contract)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_UpdateContract", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@type", contract.ContractType);
                        command.Parameters.AddWithValue("@salary",contract.Salary);
                        command.Parameters.AddWithValue("@id", contract.ContractID);

                        con.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ExeceptionDataAccess exception = new ExeceptionDataAccess();
                exception.StoreExceptions(ex);
                return false;
            }


            return true;
        }

        //Delete Contracts
        public bool DeleteContracts(Contracts contract)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_DeleteContractByName", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@name", contract.ContractType);

                         con.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ExeceptionDataAccess exception = new ExeceptionDataAccess();
                exception.StoreExceptions(ex);
                return false;
            }


            return true;
        }
    }
}
