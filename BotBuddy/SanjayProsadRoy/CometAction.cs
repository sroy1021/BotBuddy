using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using BotBuddy.SanjayProsadRoy;
using Microsoft.VisualBasic;

namespace BotBuddy.SanjayProsadRoy
{
  public class CometAction : IDisposable
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
            ProcCode,
            Pos,
            Charge,
            Nc,
            Allow,
            Ded,
            Paid,
            Coins,
            Prefix,
            PaymentScreen,
            Dup,
            Allow_Newcoins,
            Ov_Newcoins,
            Proc_Newcoins,
            Pos_Newcoins,
            CauseCode_GlobalMat

        }

        public CometAction(string CometTitle)
        {

            if (ValidDate())
            {

                _CometTitle = CometTitle;
                oMouse = new MouseAction();
                oWin = new WinAction();
                oImg = new ImgAction();

                InputFields = new Dictionary<string, string>()
            {
                {"Override","Ov.PNG"},
                {"RemarkCode","Rc.PNG"},
                {"ProcCode", "Proc.PNG" },
                {"Pos", "Pos.PNG" },
                {"Charge", "Charge.PNG" },
                {"Nc", "Nc.PNG" },
                {"Allow", "Allow.PNG" },
                {"Ded", "Ded.PNG" },
                {"Paid", "Paid.PNG" },
                {"Coins", "Coins.PNG" },
                {"Prefix", "Prefix.PNG" },
                {"PaymentScreen", "PaymentScreen.PNG" },
                {"Dup", "Dup.PNG" },
                {"Allow_Newcoins", "Allow_Newcoins.PNG" },
                {"Ov_Newcoins", "Ov_Newcoins.PNG" },
                {"Proc_Newcoins", "Proc_Newcoins.PNG" },
                {"Pos_Newcoins", "Pos_Newcoins.PNG" },
                {"CauseCode_GlobalMat", "CauseCode_GlobalMat.PNG" },
               
            };
            }

           
        }
          
        public string SetFocusOnButton(string buttonName)
        {
            try
            {
                oWin.ControlSetFocus(_CometTitle, buttonName);
                Delay(100);
               // KeySend("{ENTER}");
            }
            catch (Exception)
            {

                return "Fail";
            }
            return "Success";
        }
        public String ClickOnHeader(InputField field)
        {
            try
            {
                string mainWindow = _CometTitle;

                ImageFileToFind = Application.StartupPath + "/CometImage/" + InputFields[field.ToString()];         // @"C:\Users\sroy1021\Desktop\CometImage\Ov.PNG";
                IntPtr hWnd = oWin.GetWindow(mainWindow);

                // oWin.ActivateWindow(hWnd);

                // Delay(500);

                oImg.ImageFile = ImageFileToFind;       //InputFields[field.ToString()];

                System.Drawing.Point Location;

                Location = oImg.GetImageLocation();

                int X = Convert.ToInt32(Location.X);
                int Y = Convert.ToInt32(Location.Y);
                Delay(100);

                oMouse.MouseDblClick(X + 5, Y + 10);

                Delay(100);
            }
            catch (Exception ex)
            {

                return "Fail : -" + ex.StackTrace.ToString();
            }
            return "Success";
        }
        public string SetCursorToCometField(InputField field)
        {
            try
            {
                 string mainWindow = _CometTitle;

                ImageFileToFind =  Application.StartupPath + "/CometImage/" + InputFields[field.ToString()];         // @"C:\Users\sroy1021\Desktop\CometImage\Ov.PNG";
                IntPtr hWnd = oWin.GetWindow(mainWindow);

               // oWin.ActivateWindow(hWnd);

               // Delay(500);

                oImg.ImageFile = ImageFileToFind;       //InputFields[field.ToString()];

                System.Drawing.Point Location;

                Location = oImg.GetImageLocation();

                int X = Convert.ToInt32(Location.X);
                int Y = Convert.ToInt32(Location.Y);


                oMouse.MouseClick(X + 10, Y + 20);

                Delay(100);

                //KeySend("{DELETE}");
                //Delay(100);

                //value = value.PadLeft(2, '0');
                ////MessageBox.Show(value);
                //Delay(100);
                //KeySend(value);

                //Delay(200);

                //oMouse.MouseDblClick(X + 5, Y + 10);

                //Delay(500);

            // MessageBox.Show("Click" + ClickOnButton("cmdCalc"));

            }
            catch (Exception ex)
            {
                 return "Fail : -" + ex.StackTrace.ToString();
            }
            return "Success";


        }
        public bool CheckIfPaymentScreen()
        {
            string mainWindow = _CometTitle;

            ImageFileToFind = Application.StartupPath + "/CometImage/" + InputFields[InputField.PaymentScreen.ToString()];         // @"C:\Users\sroy1021\Desktop\CometImage\Ov.PNG";
            IntPtr hWnd = oWin.GetWindow(mainWindow);

            // oWin.ActivateWindow(hWnd);

            // Delay(500);

            oImg.ImageFile = ImageFileToFind;       //InputFields[field.ToString()];

            System.Drawing.Point Location;

            Location = oImg.GetImageLocation();

           
           if (Location.X ==0  && Location.Y == 0)
            {
                return true;
            }
           else {
                return false;
            }


        }
        //public string SetCursorToCometField(InputField field, string value, int yIndex)
        //{
        //    try
        //    {
        //        string mainWindow = _CometTitle;

        //        ImageFileToFind = InputFields[field.ToString()]; ;
        //        IntPtr hWnd = oWin.GetWindow(mainWindow);

        //        oWin.ActivateWindow(hWnd);

        //        Delay(500);

        //        oImg.ImageFile = ImageFileToFind;

        //        System.Drawing.Point Location;

        //        Location = oImg.GetImageLocation();

        //        int X = Convert.ToInt32(Location.X);
        //        int Y = Convert.ToInt32(Location.Y);


        //        oMouse.MouseDblClick(X + 10, Y + 20);

        //        Delay(100);

               
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Fail";
        //    }
        //    return "Success";


        //}

          public void SendDoublClick()
        {
            oMouse.MouseDblClick();
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

       public void Delay(int TimeInMs)
        {
            System.Threading.Thread.Sleep(TimeInMs);

        }

       public void KeySend(string key, int repeate = 1)
        {
            for (int i = 1; i <= repeate; i++)
            {
                SendKeys.Send(key);
                Delay(50);
            }

           // My.Computer.Keyboard.SendKeys(key, True);
        }

        public string GetValueFromFocusedPaymentGrid()
        {
            object patternObj;
            var tmpValue = "";
            

            var child = AutomationElement.FocusedElement;

            if (child.TryGetCurrentPattern(ValuePattern.Pattern, out patternObj))
            {
                var valuePattern = (ValuePattern)patternObj;
                 tmpValue = valuePattern.Current.Value;                            

      
            }
            return tmpValue;
        }

        public string GetValueFromPaymentGrid(InputField field)
        {
            string result = "";
            // FillCometField(field);
            SetCursorToCometField(field);
            Delay(100);

           //  AutomationElement parentWindow = AutomationElement.RootElement.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ProcessIdProperty, procId));
          //  var childWindow = parentWindow.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationPropertyChangedEvent, AutomationElement.FocusedElement));
           // IntPtr hWnd = oWin.GetWindow(_CometTitle);
            object patternObj;

            for (int i = 0; i < 7; i++)
            {
               
                var child = AutomationElement.FocusedElement;

                if (child.TryGetCurrentPattern(ValuePattern.Pattern, out patternObj))
                {
                    var valuePattern = (ValuePattern)patternObj;
                  var  tmpValue = valuePattern.Current.Value;

                    if (tmpValue != "")
                    {
                        result = result + "," + tmpValue;

                    }
                }

               
                Delay(200);
                KeySend("{DOWN}");
                Delay(200);

            }


            MessageBox.Show(result);

            //var listElement = oWin.GetControlFromCursorPos();


            //var rowList = listElement.FindAll(TreeScope.Subtree, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));

      

            //for (int i = 0; i < 12; i++)
            //{

            //    result = result + "," + oWin.GetItemValueFromCurPosInListView(rowList[0], i);
            //}


            return result;

           // return oWin.GetValueFromGrid();

             
        }

        #region MyRegion
        private bool ValidDate()
        {
            DateTime dt1 = DateTime.Parse("08/08/2019");
            DateTime dt2 = DateTime.Now;

            if (dt1.Date > dt2.Date)
            {
                //It's a later date

                return true;
            }
            else
            {
                //It's an earlier or equal date
                return false;
            }
        }

        #endregion


        public string SetValueToPaymentGrid(string value)
        {
                        
            object patternObj;
            
            try
            {

                var child = AutomationElement.FocusedElement;

                if (child.TryGetCurrentPattern(ValuePattern.Pattern, out patternObj))
                {
                    var valuePattern = (ValuePattern)patternObj;
                    // var tmpValue = valuePattern.Current.Value;
                    valuePattern.SetValue(value);

                }

            }
            catch (Exception)
            {

                return "Fail";
            }

            return "Success";
              
       }

        

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
