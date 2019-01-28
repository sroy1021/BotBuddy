using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BotBuddy.SanjayProsadRoy;
using System.Windows;



namespace TestHarness
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            var oRobo = new WinAction();
            var oMouse = new MouseAction();
            
            IntPtr hWnd = oRobo.GetWindow("Test Form");

            oRobo.ActivateWindow(hWnd);

            Delay(500);

            //IntPtr hCntrl = oRobo.GetControlInWindow(hWnd, "WindowsForms10.BUTTON.app.0.28bc8c8_r9_ad11", "btnHello");

           oRobo.GetControlInWindow("Test Form", "dataGridView1");
            

        }

        void Delay(int TimeInMs )
        {
            System.Threading.Thread.Sleep(TimeInMs);
            
        }


    }
}
