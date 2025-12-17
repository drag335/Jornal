using System;
using System.Windows.Forms;
using Учетуспеваемоси;

namespace Учетуспеваемоси
{
    public partial class FormRegistration : Form
    {
        private Registration registration;

        public FormRegistration()
        {
            InitializeComponent();
            registration = new Registration();
        }

        private void buttonZahodGosta_Click(object sender, EventArgs e)
        {
            textBoxPassword.Text = "";
            textBoxPassword.Enabled = false;
            textBoxLogin.Focus();
        }

        private void buttonVhod_Click(object sender, EventArgs e)
        {
            Enter();
        }

        private void Enter()
        {

            MessageBox.Show("проверка работы ");

            assessments assessment = new assessments();
            assessment.Show();
            this.Close();
        }

        private void textBoxLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!textBoxPassword.Enabled)
                {
                    Enter();
                    buttonVhod.Focus();
                }
                else
                {
                    textBoxPassword.Focus();
                }
            }
        }
        private void textBoxPassword_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Enter();
                buttonVhod.Focus();
            }
        }

        private void btnLookPassword_Click(object sender, EventArgs e)
        {
            if (textBoxPassword.PasswordChar == '\0')
                textBoxPassword.PasswordChar = '*';
            else
                textBoxPassword.PasswordChar = '\0';
        }
    }
}