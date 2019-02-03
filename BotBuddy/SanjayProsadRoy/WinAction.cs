using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Forms;
using static BotBuddy.SanjayProsadRoy.Win32;


namespace BotBuddy.SanjayProsadRoy
{
    public partial class WinAction : BotBuddy
    {


        // This enumeration holds all the possible values that can be passed onto the ShowWindow function.
        public enum SW : int
        {
            HIDE = 0,
            SHOWNORMAL = 1,
            SHOWMINIMIZED = 2,
            SHOWMAXIMIZED = 3,
            SHOWNOACTIVATE = 4,
            SHOW = 5,
            MINIMIZE = 6,
            SHOWMINNOACTIVE = 7,
            SHOWNA = 8,
            RESTORE = 9,
            SHOWDEFAULT = 10
        }

        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(POINT pt);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetActiveWindow(IntPtr hWnd);

        // The SetForegroundWindow will activate the window, setting the window thread to the foreground thread, as
        // well as activating keyboard input for the specified window.
        [DllImport("user32.dll")]
        private static extern long SetForegroundWindow(IntPtr hWnd);

        // The ShowWindow function can do the same as SetForegroundWindow, but it gives much greater control
        // over what happens, by customizing the parameters sent through the cmd parameter.
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool BringWindowToTop(HandleRef hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetFocus(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

        [DllImport("user32.dll")]
        static extern IntPtr AttachThreadInput(IntPtr idAttach,
                             IntPtr idAttachTo, bool fAttach);

        [DllImport("user32.dll")]
        static extern IntPtr GetFocus();

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(int xPoint, int yPoint);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetClassName(IntPtr hWnd, System.Text.StringBuilder lpClassName, int nMaxCount);

        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SendMessage(int hWnd, int Msg, int wparam, int lparam);


        const int WM_GETTEXT = 0x000D;
        const int WM_GETTEXTLENGTH = 0x000E;



        /// <summary>
        ///     Retrieves a handle to the top-level window whose class name and window name match the specified strings. This
        ///     function does not search child windows. This function does not perform a case-sensitive search. To search child
        ///     windows, beginning with a specified child window, use the
        ///     <see cref="!:https://msdn.microsoft.com/en-us/library/windows/desktop/ms633500%28v=vs.85%29.aspx">FindWindowEx</see>
        ///     function.
        ///     <para>
        ///     Go to https://msdn.microsoft.com/en-us/library/windows/desktop/ms633499%28v=vs.85%29.aspx for FindWindow
        ///     information or https://msdn.microsoft.com/en-us/library/windows/desktop/ms633500%28v=vs.85%29.aspx for
        ///     FindWindowEx
        ///     </para>
        /// </summary>
        /// <param name="lpClassName">
        ///     C++ ( lpClassName [in, optional]. Type: LPCTSTR )<br />The class name or a class atom created by a previous call to
        ///     the RegisterClass or RegisterClassEx function. The atom must be in the low-order word of lpClassName; the
        ///     high-order word must be zero.
        ///     <para>
        ///     If lpClassName points to a string, it specifies the window class name. The class name can be any name
        ///     registered with RegisterClass or RegisterClassEx, or any of the predefined control-class names.
        ///     </para>
        ///     <para>If lpClassName is NULL, it finds any window whose title matches the lpWindowName parameter.</para>
        /// </param>
        /// <param name="lpWindowName">
        ///     C++ ( lpWindowName [in, optional]. Type: LPCTSTR )<br />The window name (the window's
        ///     title). If this parameter is NULL, all window names match.
        /// </param>
        /// <returns>
        ///     C++ ( Type: HWND )<br />If the function succeeds, the return value is a handle to the window that has the
        ///     specified class name and window name. If the function fails, the return value is NULL.
        ///     <para>To get extended error information, call GetLastError.</para>
        /// </returns>
        /// <remarks>
        ///     If the lpWindowName parameter is not NULL, FindWindow calls the <see cref="M:GetWindowText" /> function to
        ///     retrieve the window name for comparison. For a description of a potential problem that can arise, see the Remarks
        ///     for <see cref="M:GetWindowText" />.
        /// </remarks>
        // For Windows Mobile, replace user32.dll with coredll.dll

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Find window by Caption only. Note you must pass IntPtr.Zero as the first parameter.

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        // You can also call FindWindow(default(string), lpWindowName) or FindWindow((string)null, lpWindowName)

       

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        public IntPtr GetWindow(string windowTitle)
        {
            IntPtr hWnd = IntPtr.Zero;
            foreach (Process pList in Process.GetProcesses())
            {
                if (pList.MainWindowTitle.Contains(windowTitle))
                {
                    hWnd = pList.MainWindowHandle;
                    return hWnd;
                }
            }
            return hWnd;
        }

        public int GetWindowProcessId(string windowTitle)
        {
            IntPtr hWnd = IntPtr.Zero;
            foreach (Process pList in Process.GetProcesses())
            {
                if (pList.MainWindowTitle.Contains(windowTitle))
                {
                    return pList.Id;
                }
            }
            return 0;
        }

        public bool ActivateWindow(IntPtr hWnd)
        {
            // return SetActiveWindow(hWnd);
            // We'll activate the window by calling the SetForegroundWindow function, passing in the handle to the window.
            SetForegroundWindow(hWnd);

            // Calling the ShowWindow function with the SHOWNA parameter will put the window in the foreground,
            // but it won't be activated.
            ShowWindow(hWnd, (int)SW.SHOWNOACTIVATE);

            ShowWindow(hWnd, (int)SW.SHOWNA);
            return BringWindowToTop(hWnd);


        }

        public bool WindowSetFocus(IntPtr hWnd)
        {
            return (SetFocus(hWnd) != IntPtr.Zero ? true : false);

        }

        public string GetControlText(IntPtr hWnd)
        {

            // Get the size of the string required to hold the window title (including trailing null.)
            Int32 titleSize = SendMessage((int)hWnd, WM_GETTEXTLENGTH, 0, 0).ToInt32();

            // If titleSize is 0, there is no title so return an empty string (or null)
            if (titleSize == 0)
                return String.Empty;

            StringBuilder title = new StringBuilder(titleSize + 1);

            SendMessage(hWnd, (int)WM_GETTEXT, title.Capacity, title);

            return title.ToString();
        }


        //public IntPtr FocusedControlInActiveWindow(IntPtr activeWindowHandle)
        //{


        //    //IntPtr activeWindowThread =
        //    //  GetWindowThreadProcessId(activeWindowHandle, IntPtr.Zero);
        //    //IntPtr thisWindowThread =
        // GetWindowThreadProcessId(activeWindowThread, IntPtr.Zero);

        //    //AttachThreadInput(activeWindowThread, thisWindowThread, true);
        //    //IntPtr focusedControlHandle = GetFocus();
        //    //AttachThreadInput(activeWindowThread, thisWindowThread, false);

        //    //return focusedControlHandle;
        //}

        public int GetWindowProcessId(IntPtr hWnd)
        {
            return GetWindowThreadProcessId(hWnd, IntPtr.Zero);
        }

        public IntPtr GetWindowFromLocation(int x, int y)
        {
            IntPtr w = WindowFromPoint(x, y);
            return w;
        }



        public string GetWinClass(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero)
                return null;
            StringBuilder classname = new StringBuilder(100);
            IntPtr result = GetClassName(hwnd, classname, classname.Capacity);
            if (result != IntPtr.Zero)
                return classname.ToString();
            return null;

        }



        // Find window by Caption, and wait 1/2 a second and then try again.
        public  IntPtr GetWindow(string className, string windowName, bool wait,  int waitTimeInMiliSecond = 500 )
        {
            int delay = 100;
            IntPtr hWnd = FindWindow(className, windowName);
            while (hWnd == IntPtr.Zero && delay < waitTimeInMiliSecond && wait)
            {
                System.Threading.Thread.Sleep(delay);
                hWnd = FindWindow(className, windowName);
                delay += 100;
            }

            return hWnd;
        }


        public IntPtr GetControlInWindow(IntPtr hWnd, string className, string caption )
        {
            IntPtr hCntrl = IntPtr.Zero;

            hCntrl = FindWindowEx(hWnd, hCntrl, className, caption);

            return hCntrl;
        }

       
        public void GetControlInWindow(string windowTitle, string classNameOfControl)
        {
            var procId = GetWindowProcessId(windowTitle);
            var parentWindow = AutomationElement.RootElement.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ProcessIdProperty, procId));
            var childWindow = parentWindow.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, classNameOfControl));

            //MessageBox.Show(GetValueOfElemnt(childWindow));
            object patternObj;

            // AutomationElement child =   FindChildAt(childWindow, 2);

            //var pattern = childWindow.TryGetCurrentPattern(SelectionPattern.Pattern);
            //var arrayOfRows = pattern.Current.GetSelection();

            //if ( childWindow.TryGetCurrentPattern(SelectionPattern.Pattern, out patternObj))
            // {

            // }

            test(childWindow);


            var childWindow2 = parentWindow.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, "pnlExtraCols"));

            test(childWindow2);
            //    var arrayOfRows = patternObj.Current.GetSelection();
            var ch = AutomationElement.RootElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.ClassNameProperty, "WindowsForms10.Window.8.app.0.13965fa_r33_ad139"));


            //((InvokePattern)ch.GetCurrentPattern(InvokePattern.Pattern)).Invoke();


            var allChild = childWindow.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Text));

            MessageBox.Show(allChild.Count.ToString());
            foreach (AutomationElement e in allChild)
            {
                MessageBox.Show(e.Current.Name);

                if (e.Current.ClassName != "") { MessageBox.Show(e.Current.ClassName);

                    MessageBox.Show(GetValueOfElemnt(e));

                }

               
            }
            //childWindow.SetFocus();
            MessageBox.Show(allChild.Count.ToString());
            var gridRows = childWindow.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.IsControlElementProperty, true));
            MessageBox.Show(gridRows.Count.ToString());

            foreach (AutomationElement dRow in gridRows)
            {
                if (dRow.Current.Name != "Top Row")
                {

                   // object patternObj = null;
                    string tmpValue = "";
                    if (dRow.TryGetCurrentPattern(ValuePattern.Pattern, out patternObj))
                    {
                        var valuePattern = (ValuePattern)patternObj;
                        tmpValue = valuePattern.Current.Value;
                    }
                    else if (dRow.TryGetCurrentPattern(TextPattern.Pattern, out patternObj))
                    {
                        var textPattern = (TextPattern)patternObj;
                        tmpValue = textPattern.DocumentRange.GetText(-1).TrimEnd('\r'); // often there is an extra '\r' hanging off the end.
                    }
                    else
                    {
                        tmpValue = dRow.Current.Name;

                    }

                    MessageBox.Show(tmpValue);

                }
               
                
            }



           


        }
        private AutomationElement GetControlFromCursorPos()
        {
            POINT pt;
            GetCursorPos(out pt);
            IntPtr hwnd = WindowFromPoint(pt);
            AutomationElement el = AutomationElement.FromHandle(hwnd);
            return el;

        }

        public string GetValueOfList(string windowTitle, int index)
        {
            var procId = GetWindowProcessId(windowTitle);
            var parentWindow = AutomationElement.RootElement.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ProcessIdProperty, procId));
            var childWindow = parentWindow.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, "lvwComet"));

            var row = childWindow.FindAll(TreeScope.Subtree, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.DataItem));


           // AutomationElement element = GetControlFromCursorPos();
           // test(element);
            return GetItemValueFromCurPosInListView(row[0], index);

            
        }

        private string GetItemValueFromCurPosInListView(AutomationElement element, int index)
        {
            
            
            //int i = 0;
            object patternObj;
            string tmpValue = "";

            TreeWalker walker = TreeWalker.ControlViewWalker;
            AutomationElement child = walker.GetFirstChild(element);
            for (int x = 1; x <= index; x++)
            {
                child = walker.GetNextSibling(child);
                if (child == null)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }


            if (child.TryGetCurrentPattern(ValuePattern.Pattern, out patternObj))
            {
                var valuePattern = (ValuePattern)patternObj;
                tmpValue = valuePattern.Current.Value;

                ((ValuePattern)valuePattern).SetValue("D5");
                //var pattern = (SelectionPattern)patternObj;
                //var arrayOfRows = pattern.Current.GetSelection();

            }
            else if (child.TryGetCurrentPattern(TextPattern.Pattern, out patternObj))
            {
                var textPattern = (TextPattern)patternObj;
                tmpValue = textPattern.DocumentRange.GetText(-1).TrimEnd('\r'); // often there is an extra '\r' hanging off the end.
                                                                                //var pattern = (SelectionPattern)patternObj;
                var valuePattern = (ValuePattern)patternObj;
                ((ValuePattern)valuePattern).SetValue("D5");

                //var arrayOfRows = pattern.Current.GetSelection();
            }
            else
            {
                tmpValue = child.Current.Name;

            }



            return tmpValue;

        }


        void test(AutomationElement el)
        {
            TreeWalker walker = TreeWalker.ContentViewWalker;
            int i = 0;
            object patternObj;
            string tmpValue = "";
            for (AutomationElement child = walker.GetFirstChild(el); child != null; child = walker.GetNextSibling(child))
            {

                if (child.TryGetCurrentPattern(ValuePattern.Pattern, out patternObj))
                {
                    var valuePattern = (ValuePattern)patternObj;
                    tmpValue = valuePattern.Current.Value;
                    //var pattern = (SelectionPattern)patternObj;
                    //var arrayOfRows = pattern.Current.GetSelection();
                }
                else if (child.TryGetCurrentPattern(TextPattern.Pattern, out patternObj))
                {
                    var textPattern = (TextPattern)patternObj;
                    tmpValue = textPattern.DocumentRange.GetText(-1).TrimEnd('\r'); // often there is an extra '\r' hanging off the end.
                    //var pattern = (SelectionPattern)patternObj;
                    //var arrayOfRows = pattern.Current.GetSelection();
                }
                else
                {
                    tmpValue = child.Current.Name;

                }

                tmpValue = "";
                //MessageBox.Show(tmpValue);
                //! Select The Item Here ...
                //child.SetFocus();
            }
        }




        AutomationElement FindChildAt(AutomationElement parent, int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            TreeWalker walker = TreeWalker.ControlViewWalker;
            AutomationElement child = walker.GetFirstChild(parent);
            for (int x = 1; x <= index; x++)
            {
                child = walker.GetNextSibling(child);
                if (child == null)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            return child;
        }


        private string GetValueOfElemnt(AutomationElement element)
        {

            object patternObj = null;
            string tmpValue = "";
            
            if (element.Current.Name != "Top Row")
            {

                
                if (element.TryGetCurrentPattern(ValuePattern.Pattern, out patternObj))
                {
                    var valuePattern = (ValuePattern)patternObj;
                    tmpValue = valuePattern.Current.Value;
                    var pattern = (SelectionPattern)patternObj;
                    var arrayOfRows = pattern.Current.GetSelection();
                }
                else if (element.TryGetCurrentPattern(TextPattern.Pattern, out patternObj))
                {
                    var textPattern = (TextPattern)patternObj;
                    tmpValue = textPattern.DocumentRange.GetText(-1).TrimEnd('\r'); // often there is an extra '\r' hanging off the end.
                    var pattern = (SelectionPattern)patternObj;
                    var arrayOfRows = pattern.Current.GetSelection();
                }
                else
                {
                    tmpValue = element.Current.Name;

                }

                MessageBox.Show(tmpValue);

            }

            return tmpValue;
        }

//--------------------------------------------------------------------------------------
        private GridPattern GetGridPattern(
     AutomationElement targetControl)
        {
            GridPattern gridPattern = null;

            try
            {
                gridPattern =
                    targetControl.GetCurrentPattern(
                    GridPattern.Pattern)
                    as GridPattern;
            }
            // Object doesn't support the 
            // GridPattern control pattern
            catch (InvalidOperationException)
            {
                return null;
            }

            return gridPattern;
        }

        private GridItemPattern GetGridItemPattern(
    AutomationElement targetControl)
        {
            GridItemPattern gridItemPattern = null;

            try
            {
                gridItemPattern =
                    targetControl.GetCurrentPattern(
                    GridItemPattern.Pattern)
                    as GridItemPattern;
            }
            // Object doesn't support the 
            // GridPattern control pattern
            catch (InvalidOperationException)
            {
                return null;
            }

            return gridItemPattern;
        }


        //// THE FOLLOWING METHOD REFERENCES THE SetForegroundWindow API
        //public static bool BringWindowToTop(string windowName, bool wait)
        //{
        //    int hWnd = FindWindow(windowName, wait);
        //    if (hWnd != 0)
        //    {
        //        return SetForegroundWindow((IntPtr)hWnd);
        //    }
        //    return false;
        //}





    }
}
