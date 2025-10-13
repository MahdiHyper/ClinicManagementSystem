using ClinicManagementSystem.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicManagementSystem.UI.PeopleForms
{
    public partial class ctrlPersonInfo : UserControl
    {
        private int _PersonID = -1;
        private clsPerson _Person;

        public ctrlPersonInfo()
        {
            InitializeComponent();
        }

        public ctrlPersonInfo(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        public void LoadPersonInfo(int PersonID) //External Call
        {
            _PersonID = PersonID;
            _LoadPersonData();
        }

        private void ctrlPersonInfo_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                _LoadPersonData();
            }
        }

        private void _LoadPersonData()
        {
            _Person = clsPerson.GetPersonInfo(_PersonID);

            if (_Person == null) 
            {
                MessageBox.Show(
                    "There is no Person with person Id : " + _PersonID.ToString(),
                    "Error - Person Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            _FillWithPersonInfo();
        }

        private void _FillWithPersonInfo()
        {
            lblFullName.Text = _Person.FullName;
            lblPersonID.Text = _PersonID.ToString();
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblPhoneNumber.Text = _Person.PhoneNumber; 
            lblEmail.Text = _Person.Email;
            lblGender.Text = _Person.Gender == 1 ? "Male" : "Female";

         
            if (_Person.Gender == 1)
            {
                PictureIcon.Image = Properties.Resources.man;
            }
            else
            {
                PictureIcon.Image = Properties.Resources.woman;
            }
        }

        public int PersonID
        {
            get { return _PersonID; }
        }

        public clsPerson Person
        {
            get { return _Person; }
        }
    }
}