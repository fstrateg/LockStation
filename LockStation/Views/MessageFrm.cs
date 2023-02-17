using LockStation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockStation
{
    public partial class MessageFrm : Form
    {
        MainModel Model = new MainModel();
        Timer Timer = new Timer();
        public MessageFrm()
        {
            InitializeComponent();
        }

        private void notyfyTool_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button== MouseButtons.Middle) Model.CanClose=true;          
            if (e.Button == MouseButtons.Left)  this.Show();
        }

        private void MessageFrm_Load(object sender, EventArgs e)
        {
            bindingSource1.DataSource = Model;
            this.FormClosing += (s0, e0) => { 
                if (!Model.CanClose)
                {
                    e0.Cancel = true;
                    this.Hide();
                    return;
                }
            };
            Timer.Interval= 60000/30;
            Timer.Start();
            Timer.Tick += (e0, s0) => {
                Model.Tick();
                bindingSource1.ResetBindings(false);
            };

            Model.NeedClose += (e0, s0) => {
                this.Show();
            };
            Model.ShutDown += (e0, s0) =>
            {
                MessageBox.Show("Выключаемся!");
            };
        }
    }
}
