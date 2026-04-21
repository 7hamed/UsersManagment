using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{

    internal enum enMode { AddNew = 1, Update = 2}

    public class clsUsers
    {

        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImagePath { get; set; }

        private enMode _Mode;

        public clsUsers()
        {
            this.ID = -1;
            this.UserName = "";
            this.Email = "";
            this.Password = "";
            this._Mode = enMode.AddNew;
        }

        private clsUsers(int id, string userName, string email, string password, string imagePath)
        {
            this.ID = id;
            this.UserName = userName;
            this.Email = email;
            this.Password = password;
            this._Mode = enMode.Update;
        }

        static public clsUsers Find(int id)
        {
            string userName = "";
            string email = "";
            string password= "";
            string imagePath = "";

            if (clsUsersDataAccess.GetUserInfoByID(id, ref userName, ref email, ref password, ref imagePath))
            {
                return new clsUsers(id, userName, email, password, imagePath);
            }
            else
            {
                return null;
            }

        }

        private bool _AddNewUser()
        {
            this.ID = clsUsersDataAccess.AddNewUser(UserName, Email, Password, ImagePath);

            return (this.ID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUsersDataAccess.UpdateUserByID(ID, UserName, Email, Password, ImagePath);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    if (_UpdateUser())
                        return true;
                    else
                        return false;

                default:
                    return false;

            }
        }

        static public DataTable GetAllUsers()
        {
            return clsUsersDataAccess.GetAllUsers();
        }

        static public bool Delete(int id)
        {
            return clsUsersDataAccess.DeleteContactByID(id);
        }

    }
}
