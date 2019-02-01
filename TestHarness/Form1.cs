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
            
            var oImg = new ImgAction();

            string mainWindow = "Comet Desktop - [Pega Production (PRD) System] -PEGA 7- [Build  2018.10.30.1400 ]";


            IntPtr hWnd = oRobo.GetWindow(mainWindow);

            oRobo.ActivateWindow(hWnd);

            Delay(500);

            oImg.ImageFile = txtPath.Text.Trim();

            System.Drawing.Point Location;

            Location = oImg.GetImageLocation();

            txtX.Text = Location.X.ToString();
            txtY.Text = Location.Y.ToString();
           
            //IntPtr hCntrl = oRobo.GetControlInWindow(hWnd, "WindowsForms10.BUTTON.app.0.28bc8c8_r9_ad11", "btnHello");

            // oRobo.GetControlInWindow(mainWindow, "CometListview");


        }

        void Delay(int TimeInMs )
        {
            System.Threading.Thread.Sleep(TimeInMs);
            
        }

        private void btnClick_Click(object sender, EventArgs e)
        {
            string mainWindow = "Comet Desktop - [Pega Production (PRD) System] -PEGA 7- [Build  2018.10.30.1400 ]";

            var oComet = new CometAction(mainWindow);

            oComet.PutOverride("02");

            //var oMouse = new MouseAction();
            //var oRobo = new WinAction();
            //oMouse.SetCursorPosition(Convert.ToInt32(txtX.Text.Trim()), Convert.ToInt32(txtY.Text.Trim()));

            //Delay(200);

            //oMouse.MouseDblClick(Convert.ToInt32(txtX.Text.Trim()), Convert.ToInt32(txtY.Text.Trim()));
            //Delay(100);

            //SendKeys.Send("02");

          //  MessageBox.Show(oRobo.GetValueOfList(Convert.ToInt32(txtIndex.Text.Trim())));


        }
    }
}
