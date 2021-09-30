using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
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
            var pat = Operations.Patients.FirstOrDefault(patient => patient.PatientID == identifier);
            Console.WriteLine(pat == null ? $"{identifier} not found" : $"{pat.FirstName} {pat.LastName}");

            identifier = 3;
            pat = Operations.Patients.FirstOrDefault(patient => patient.PatientID == identifier);
            Console.WriteLine(pat == null ? $"{identifier} not found" : $"{pat.FirstName} {pat.LastName}");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Console.WriteLine();
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
