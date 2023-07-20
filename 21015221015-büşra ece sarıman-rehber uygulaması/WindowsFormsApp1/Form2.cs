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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public Kisi Kisi = null;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            propertyGrid1.SelectedObject= Kisi;
        }
        private void tamam(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void iptal(object sender, EventArgs e)
        {
            DialogResult= DialogResult.Cancel;
        }
    }
}
