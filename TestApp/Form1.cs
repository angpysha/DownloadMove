using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);
            MoverLib.MoverLib ml = new MoverLib.MoverLib();
            ml.Start();
        //    MessageBox.Show(ml.geteventstate().ToString());
            ml.TestEvent += new MoverLib.MoverLib.TestingEventHandler(ev);
        }
        
        private void ev(string mes)
        {
            MessageBox.Show(mes);
        }
    }
}
