using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UMS
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            // validate input
            if (string.IsNullOrWhiteSpace(usernameTextbox.Text) ||
            string.IsNullOrWhiteSpace(passwordTextbox.Text))
            {
                MessageBox.Show(
                    "username and password is required!", 
                    "Error", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }
            // open database
            using (var db = new UniversityEntities())
            {
                // check if user exists
                var user = db.users.Where(u => u.username == usernameTextbox.Text &&
                u.password == passwordTextbox.Text).FirstOrDefault();
                if (user == null)
                {
                    MessageBox.Show(
                        "invalid username or password!",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }
                
                // hide login form
                usernameTextbox.Text = "";
                passwordTextbox.Text = "";
                this.Hide();

                // open form based on role
                switch (user.role)
                {
                    // administrator
                    case "Admin":
                        var adminForm = new FormAdmin();
                        adminForm.ShowDialog();

                        break;

                    // instructor
                    case "Instructor":
                        var facultyForm = new FormInstructor();
                        facultyForm.ShowDialog();

                        break;

                    // student
                    case "Student":
                        var studentForm = new FormStudent();
                        studentForm.ShowDialog();

                        break;

                }
                
                // show login form
                this.Show();

            }
        }
    }
}
