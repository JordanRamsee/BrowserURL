using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BrowserURL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer timer = new Timer();
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = 3000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                Process currentProcess = Process.GetProcessById(WinAPIFunctions.GetWindowProcessId(WinAPIFunctions.GetforegroundWindow()));
                Browser_Name_URL.Document.Blocks.Clear();
                Browser_Name_URL.AppendText(currentProcess.ProcessName + Environment.NewLine );
                Browser_Name_URL.AppendText(Environment.NewLine);
                switch (currentProcess.ProcessName)
                {
                    case "chrome":
                        Browser_Name_URL.AppendText(cInternetTabHandler.getChromeTabUrl(currentProcess));
                        break;
                    case "firefox":
                        Browser_Name_URL.AppendText(cInternetTabHandler.GetFirefoxUrl(currentProcess));
                        break;
                    case "msedge":
                        Browser_Name_URL.AppendText(cInternetTabHandler.GetEdgeUrl(currentProcess));
                        break;
                    default:
                        Browser_Name_URL.AppendText("Not a Browser");
                        break;
                }
                
                
            }), DispatcherPriority.Render);
            

        }

        public class WinAPIFunctions
        {
            //Used to get Handle for Foreground Window
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            private static extern IntPtr GetForegroundWindow();

            //Used to get ID of any Window
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
            public delegate bool WindowEnumProc(IntPtr hwnd, IntPtr lparam);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool EnumChildWindows(IntPtr hwnd, WindowEnumProc callback, IntPtr lParam);

            public static int GetWindowProcessId(IntPtr hwnd)
            {
                int pid;
                GetWindowThreadProcessId(hwnd, out pid);
                return pid;
            }

            public static IntPtr GetforegroundWindow()
            {
                return GetForegroundWindow();
            }
        }

        
    }
}
