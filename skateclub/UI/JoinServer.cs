using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace skateclub.UI
{
    public partial class JoinServer : Form
    {
        Func<string, Task<bool>> callback;

        bool hold;

        public JoinServer(Func<string, Task<bool>> callback)
        {
            this.callback = callback;

            FormClosing += (s, a) => a.Cancel = hold;

            InitializeComponent();
        }

        private void PasswordInput_Load(object sender, EventArgs e)
        {

        }

        private async void enterButton_Click(object sender, EventArgs e)
        {
            hold = true;

            UpdateUI();

            if (await callback.Invoke(passwordBox.Text))
            {
                hold = false;
                Close();
            } else
            {
                hold = false;
                UpdateUI();
            }
        }

        void UpdateUI()
        {
            enterButton.Enabled = !hold;
            passwordBox.Enabled = !hold;
            cancelButton.Enabled = !hold;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
