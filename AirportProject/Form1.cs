using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AirportLibrary;

namespace AirportProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AirportLib myClass = new AirportLib();
            string responseString;
            bool isValid;
            string[] values;
            responseString = myClass.getData(textBox1.Text, textBox2.Text);

            isValid = myClass.checkIfValid(responseString);

            if (isValid)
            {
                values = myClass.getValues(responseString);
                label3.Text = values[0];
                label4.Text = values[1];
                label5.Text = values[2];
                label6.Text = values[3];
            }

            else
            {
                label3.Text = "An error has occured.  please check your airports.";
                label4.Text = "There might be no flights between those airports.";
                label5.Text = "";
                label6.Text = "";
            }
            
        }
    }
}
