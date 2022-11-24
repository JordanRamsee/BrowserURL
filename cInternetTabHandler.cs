using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace BrowserURL
{
    public class cInternetTabHandler
    {
        public static string getChromeTabUrl(Process chrome)
        {
            try
            {

                AutomationElement root = AutomationElement.FromHandle(chrome.MainWindowHandle);
                Condition condTabItem = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);

                foreach (AutomationElement tabitem in root.FindAll(TreeScope.Subtree, condTabItem))
                {
                    var SearchBar = root.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"));
                    return (string)SearchBar.GetCurrentPropertyValue(ValuePatternIdentifiers.ValueProperty);

                }
                return "Change tab";
            }
            catch (Exception ex)
            {
                return "";
            }


        }

        public static string GetFirefoxUrl(Process proc)
        {
            try
            {
                //get all running process of firefox
                Process[] procsfirefox = Process.GetProcessesByName("firefox");
                foreach (Process firefox in procsfirefox)
                {
                    //the firefox process must have a window
                    if (firefox.MainWindowHandle == IntPtr.Zero)
                    {
                        continue;
                    }
                    AutomationElement sourceElement = AutomationElement.FromHandle(firefox.MainWindowHandle);
                    //works with latest version of firefox and for older version replace 'Search with Google or enter address' with this 'Search or enter address'     
                    //or you can also find editbox element name using automation spy software
                    AutomationElement editBox = sourceElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Search with Google or enter address"));
                    // if it can be found, get the value from the editbox
                    if (editBox != null && !editBox.Current.HasKeyboardFocus)
                    {
                        ValuePattern val = ((ValuePattern)editBox.GetCurrentPattern(ValuePattern.Pattern));
                        return val.Current.Value;
                        //   Console.WriteLine("\n Firefox URL found: " + val.Current.Value);
                    }


                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
            

        }




        public static string GetEdgeUrl(Process proc)
        {
            try
            {

                AutomationElement root = AutomationElement.FromHandle(proc.MainWindowHandle);
                Condition condTabItem = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);

                foreach (AutomationElement tabitem in root.FindAll(TreeScope.Subtree, condTabItem))
                {
                    var SearchBar = root.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"));
                    return (string)SearchBar.GetCurrentPropertyValue(ValuePatternIdentifiers.ValueProperty);
                    
                }
                return "Change tab";
            }
            catch (Exception ex)
            {
                return "";
            }
            
        }

        

    }
}
