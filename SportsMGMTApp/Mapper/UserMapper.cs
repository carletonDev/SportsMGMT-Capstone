using SportsMGMTApp.Models;
using SportsMGMTCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsMGMTApp.Mapper
{
    public class UserMapper
    {
    
            //create a method using the common class Users to mapp the viewmodel to an object for bll
            public Users SendToBLL(UserRegister users)
            {
                //set values
                Users user = new Users();
                 user.Address = users.Address;
                user.FirstName = users.FirstName;
                user.LastName = users.LastName;
                user.Email = users.Email;
                user.Phone = users.Phone;
                user.UserName = users.UserName;
                user.Password = users.Password;

                //return the newly constructed BLL BusinessObject Users
                return user;
            }
        public Users LoginBLLMapper(LoginUser users)
        {
            Users user = new Users();
            user.UserName = users.UserName;
            user.Password = users.Password;

            return user;

        }

        }
    }
