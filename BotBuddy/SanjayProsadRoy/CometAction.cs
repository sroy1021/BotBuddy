using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BotBuddy.SanjayProsadRoy;

namespace BotBuddy.SanjayProsadRoy
{
  public class CometAction
    {

        private string _CometTitle { get; set; }
        private WinAction oWin { get; set; }

        private MouseAction oMouse { get; set; }
        private ImgAction oImg { get; set; }
        private string ImageFileToFind { get; set; }

        private Dictionary<string,string> InputFields { get; set; }

       public enum InputField
        {
            Override,
            RemarkCode,
            ProcCode
        }

        public CometAction(string CometTitle)
        {
            _CometTitle = CometTitle;
             oMouse = new MouseAction();
             oWin = new WinAction();
             oImg = new ImgAction();           

       InputFields =   new Dictionary<string, string>()
            {
                {"Override","Ov.PNG"},
                {"RemarkCode","Rc.PNG"},
                {"ProcCode", "Proc.PNG" },


            };
        }
            

        public string FillAllRowsOfCometField(InputField field)
        {
            try
            {
                 string mainWindow = _CometTitle;

                ImageFileToFind = @"C:\Users\sroy1021\Desktop\CometImage\Ov.PNG";
                IntPtr hWnd = oWin.GetWindow(mainWindow);

                oWin.ActivateWindow(hWnd);

                Delay(500);

                oImg.ImageFile = InputFields[field.ToString()];

                System.Drawing.Point Location;

                Location = oImg.GetImageLocation();

                int X = Convert.ToInt32(Location.X);
                int Y = Convert.ToInt32(Location.Y);


                oMouse.MouseDblClick(X + 10, Y + 20);

                Delay(100);

               // KeySend(Override);

                Delay(200);

                oMouse.MouseDblClick(X + 5, Y + 10);

            }
            catch (Exception ex)
            {
                 return "Fail";
            }
            return "Success";


        }

        public string SetCursorToCometField(InputField field, string value, int yIndex)
        {
            try
            {
                string mainWindow = _CometTitle;

                ImageFileToFind = InputFields[field.ToString()]; ;
                IntPtr hWnd = oWin.GetWindow(mainWindow);

                oWin.ActivateWindow(hWnd);

                Delay(500);

                oImg.ImageFile = ImageFileToFind;

                System.Drawing.Point Location;

                Location = oImg.GetImageLocation();

                int X = Convert.ToInt32(Location.X);
                int Y = Convert.ToInt32(Location.Y);


                oMouse.MouseDblClick(X + 10, Y + 20);

                Delay(100);

               
            }
            catch (Exception ex)
            {
                return "Fail";
            }
            return "Success";


        }

        public void FillValueFromCurrentCursorPosition(string value, int yIndex)
        {
            for (int i = 0; i < yIndex; i++)
            {
                KeySend("{DOWN ARROW}", yIndex);
                Delay(50);
            }

            KeySend(value);

            Delay(100);
        }

        void Delay(int TimeInMs)
        {
            System.Threading.Thread.Sleep(TimeInMs);

        }

        void KeySend(string key, int repeate = 1)
        {
            for (int i = 1; i <= repeate; i++)
            {
                SendKeys.Send(key);
                Delay(50);
            }
            
            
        }

    }
}
