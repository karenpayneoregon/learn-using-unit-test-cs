using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormApp2Lib;
using WindowsFormApp2Lib.Classes;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GetPatientButton_Click(object sender, EventArgs e)
        {
            int identifier = 5;

            /*
             * This asserts that the result of FirstOrDefault if object is not null
             */
            if (Operations.Patients.FirstOrDefault(patient => patient.PatientID == identifier) is {} patientItem1)
            {
                Debug.WriteLine($"{patientItem1.FirstName} {patientItem1.LastName}");
            }
            else
            {
                Debug.WriteLine($"Failed to find {identifier}");
            }
            

            identifier = 3;
            
            if (Operations.Patients.FirstOrDefault(patient => patient.PatientID == identifier) is { } patientItem2)
            {
                Debug.WriteLine($"{patientItem2.FirstName} {patientItem2.LastName}");
            }
            else
            {
                Debug.WriteLine($"Failed to find {identifier}");
            }

            var pat = Operations.Patients.FirstOrDefault(patient => patient.PatientID == identifier);
            Debug.WriteLine(pat is null ? $"Failed to find {identifier}" : $"{pat.FirstName} {pat.LastName}");
        }
        /// <summary>
        /// WindowsFormApp2Lib.Classes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
        }
    }

    public class Patient
    {
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Operations
    {
        public static List<Patient> Patients => new List<Patient>()
        {
            new Patient() {PatientID = 1, FirstName = "Joe", LastName = "Jones"},
            new Patient() {PatientID = 2, FirstName = "Anne", LastName = "White"},
            new Patient() {PatientID = 3, FirstName = "Mary", LastName = "Smith"}
        };
    }
}
