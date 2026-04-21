using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace UsersManagment
{
    public partial class frmAddUpdateUsers : Form
    {
        private enum enMode { AddUser = 1, UpdateUser = 2}

        private enMode _Mode;
        private clsUsers _User;
        private int _UserID;

        public frmAddUpdateUsers(int userID)
        {
            InitializeComponent();

            _UserID = userID;

            if (userID == -1)
            {
                _Mode = enMode.AddUser;
            }
            else
            {
                _Mode = enMode.UpdateUser;
            }
        }

        private void lblUploadPhoto_Click(object sender, EventArgs e)
        {
            DialogResult result = ofdImage.ShowDialog();

            if (result == DialogResult.OK)
            {
                pbProfile.Load(ofdImage.FileName);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _User.UserName = txtUsername.Text;
            _User.Email = txtEmail.Text;
            _User.Password = txtPassword.Text;

            if (pbProfile.Image != null)
                _User.ImagePath = pbProfile.ImageLocation;
            else
                _User.ImagePath = "";

            if (_User.Save())
            {
                MessageBox.Show("User Save Successfully.");
                _Mode = enMode.UpdateUser;
            }
            else
            {
                MessageBox.Show("User Not Save :( ");
                return;
            }

            _UserID = _User.ID;
            _RefreshForm();
        }

        private void _AddModeForm()
        {
            lblTitle.Text = "Add New User";

            txtID.Text = "???";
            txtID.Enabled = false;

            txtUsername.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            pbProfile.Image = null;

        }

        private void _UpdateModeFrom()
        {
            lblTitle.Text = "Update User";

            txtID.Text = _User.ID.ToString();
            txtID.Enabled = false;

            txtUsername.Text = _User.UserName;
            txtEmail.Text = _User.Email;
            txtPassword.Text = _User.Password;
            
            if (_User.ImagePath != "" && File.Exists(_User.ImagePath))
            {
                pbProfile.Load(_User.ImagePath);
            }
            else
            {
                pbProfile.Image = null;
            }
        }

        private void _RefreshForm()
        {
            switch (_Mode)
            {
                case enMode.AddUser:
                    _User = new clsUsers();
                    _AddModeForm();
                    break;

                case enMode.UpdateUser:
                    _User = clsUsers.Find(_UserID);
                    if (_User == null)
                    {
                        MessageBox.Show("the user not found");
                        this.Close();
                    }
                    _UpdateModeFrom();
                    break;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            _RefreshForm();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
