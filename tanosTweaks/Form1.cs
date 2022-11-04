using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


/*
 * hello there :)
 * this little tool i made is just a simple tweaker i made
 * the code looks a little bit ugly but it works!
*/


namespace tanosTweaks
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("Shell32.dll")]
        static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlag dwFlags);

        enum RecycleFlag : int
        {
            SHERB_NOCONFIRMATION = 0x00000001,
            SHERB_NOPROGRESSUI = 0x00000001,
            SHERB_NOSOUND = 0x00000004
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }

        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public string isAt = "home";

        private void HideAll(string except)
        {
            home.Visible = false;
            gamingtweaks.Visible = false;
            desktoptweaks.Visible = false;
            additional.Visible = false;
            gconfig.Visible = false;
            caution.Visible = false;

            if (except == "home")
                home.Visible = true; isAt = "home";
            if (except == "caution")
                caution.Visible = true; isAt = "caution";
            if (except == "gconfig")
                gconfig.Visible = true; isAt = "gconfig";
            if (except == "gamingtweaks")
                gamingtweaks.Visible = true; isAt = "gamingtweaks";
            if (except == "desktoptweaks")
                desktoptweaks.Visible = true; isAt = "desktoptweaks";
            if (except == "additional")
                additional.Visible = true; isAt = "additional";
        }
        private void RunPowershell(string argument)
        {
            try
            {
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.FileName = "powershell.exe";
                processInfo.Arguments = $@"" + argument + "";

                Process process = new Process();
                process.StartInfo = processInfo;
                process.Start();
            } catch
            {
                Console.WriteLine("error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HideAll("gamingtweaks");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HideAll("desktoptweaks");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HideAll("additional");
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fixlag_Click(object sender, EventArgs e)
        {
            HideAll("caution");
        }

        private void lessinputdelay_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Keyboard\" /v \"KeyboardDelay\" /t REG_SZ /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Keyboard\" /v \"KeyboardSpeed\" /t REG_SZ /d \"31\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C echo OK.");
        }

        private void disablegamemode_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\GameBar\" /v \"AutoGameModeEnabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\GameBar\" /v \"UseNexusForGameBarEnabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\GameBar\" /v \"AllowAutoGameMode\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\GameBar\" /v \"GamePanelStartupTipIndex\" /t REG_DWORD /d \"3\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\GameBar\" /v \"ShowStartupPanel\" /t REG_DWORD /d \"0\" /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\System\\GameConfigStore\" /v \"GameDVR_Enabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\System\\GameConfigStore\" /v \"GameDVR_FSEBehaviorMode\" /t REG_DWORD /d \"2\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\System\\GameConfigStore\" /v \"GameDVR_FSEBehavior\" /t REG_DWORD /d \"2\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\System\\GameConfigStore\" /v \"GameDVR_HonorUserFSEBehaviorMode\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\System\\GameConfigStore\" /v \"GameDVR_DXGIHonorFSEWindowsCompatible\" /t REG_DWORD /d \"1\" /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\GameDVR\" /v \"AllowGameDVR\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C echo OK.");
        }

        private void disableindexing_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("CMD.exe", "/C sc config WSearch start=disabled");
            System.Diagnostics.Process.Start("CMD.exe", "/C sc stop WSearch >nul 2>nul");
            System.Diagnostics.Process.Start("CMD.exe", "/C echo OK.");
        }
        private void pccleanup_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("CMD.exe", "/C del *.log /a /s /q /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C del /s /f /q c:\\windows\\temp\\*.*");
            System.Diagnostics.Process.Start("CMD.exe", "/C del /s /f /q C:\\WINDOWS\\Prefetch");
            System.Diagnostics.Process.Start("CMD.exe", "/C del /s /f /q %temp%\\*.*");
            System.Diagnostics.Process.Start("CMD.exe", "/C deltree /y c:\\windows\\tempor~1");
            System.Diagnostics.Process.Start("CMD.exe", "/C net stop wuauserv");
            System.Diagnostics.Process.Start("CMD.exe", "/C net stop UsoSvc");
            Thread.Sleep(1000);
            System.Diagnostics.Process.Start("CMD.exe", "/C RD /S /Q %temp%");
            System.Diagnostics.Process.Start("CMD.exe", "/C MKDIR %temp%");
            System.Diagnostics.Process.Start("CMD.exe", "/C MKDIR %temp%");
            System.Diagnostics.Process.Start("CMD.exe", "/C MKDIR %temp%");
            System.Diagnostics.Process.Start("CMD.exe", "/C takeown /f \"%temp%\" /r /d y");
            System.Diagnostics.Process.Start("CMD.exe", "/C rd /s /q C:\\Windows\\SoftwareDistribution");
            System.Diagnostics.Process.Start("CMD.exe", "/C md C:\\Windows\\SoftwareDistribution");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int text = rnd.Next(1);
            if (text == 1)
            {
                label2.Text = "this is a free, fast and open source\ntweaker";
            }
            else
            {
                label2.Text = "a small, fast, open source and\neffective tweaker.";
            }
            string name = Environment.UserName;
            label1.Text = "Hello " + name + " 👋,";
            if (!IsAdministrator())
                MessageBox.Show("Please run this application as administrator, most of the functions will not work properly without it.", "tanosTweaks");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("CMD.exe", "/C DISM /Online /Cleanup-Image /CheckHealth && DISM /Online /Cleanup-Image /ScanHealth && DISM /Online /Cleanup-Image /RestoreHealth && SFC /scannow && echo Done! Check %windir%/Logs/CBS/CBS.log and %windir%\\Logs\\DISM\\dism.log for logs && pause");
        }

        private void unindefender_Click(object sender, EventArgs e)
        {
            if (File.Exists("dControl.exe"))
                System.Diagnostics.Process.Start("dControl.exe");
            else
            {
                MessageBox.Show("Downloading DControl, please wait");
                var client = new WebClient();
                client.DownloadFile("https://tanos.gq/files/dControl.exe", "dControl.exe");
                System.Diagnostics.Process.Start("dControl.exe");
            }
        }

        private void unindrive_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("CMD.exe", "/C taskkill /f /im OneDrive.exe");
            System.Diagnostics.Process.Start("CMD.exe", "/C %SystemRoot%\\SysWOW64\\OneDriveSetup.exe /uninstall");
            System.Diagnostics.Process.Start("CMD.exe", "/C %SystemRoot%\\System32\\OneDriveSetup.exe /uninstall");

            Thread.Sleep(1000);

            System.Diagnostics.Process.Start("CMD.exe", "/C rd \"%UserProfile%\\OneDrive\" /s /q");
            System.Diagnostics.Process.Start("CMD.exe", "/C rd \"%LocalAppData%\\Microsoft\\OneDrive\" /s /q");
            System.Diagnostics.Process.Start("CMD.exe", "/C rd \"%ProgramData%\\Microsoft OneDrive\" /s /q");
            System.Diagnostics.Process.Start("CMD.exe", "/C rd \"C:\\OneDriveTemp\" /s /q");
            System.Diagnostics.Process.Start("CMD.exe", "/C del \"%USERPROFILE%\\AppData\\Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\OneDrive.lnk\" / s /f /q");

            System.Diagnostics.Process.Start("CMD.exe", "/C REG Delete \"HKEY_CLASSES_ROOT\\CLSID\\{018D5C66-4533-4307-9B53-224DE2ED1FE6}\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C REG Delete \"HKEY_CLASSES_ROOT\\Wow6432Node\\CLSID\\{018D5C66-4533-4307-9B53-224DE2ED1FE6}\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C REG ADD \"HKEY_CLASSES_ROOT\\CLSID\\{018D5C66-4533-4307-9B53-224DE2ED1FE6}\" /v System.IsPinnedToNameSpaceTree /d \"0\" /t REG_DWORD /f");
        }

        private void cleanup_Click(object sender, EventArgs e)
        {
            string desktop_folder = @"C:\Users\" + Environment.UserName + @"\Desktop";
            string video_folder = @"C:\Users\" + Environment.UserName + @"\Videos";
            string picture_folder = @"C:\Users\" + Environment.UserName + @"\Pictures";
            string documents_folder = @"C:\Users\" + Environment.UserName + @"\Documents";
            string executables_folder = @"C:\Users\" + Environment.UserName + @"\Desktop\Executables";

            richTextBox1.Text += "\nMoving MP4s to " + video_folder;
            System.Diagnostics.Process.Start("CMD.exe", "/C move \"" + desktop_folder + "\\*.mp4 \" \"" + video_folder + "\"");

            richTextBox1.Text += "\nMoving JPG to " + picture_folder;
            System.Diagnostics.Process.Start("CMD.exe", "/C move \"" + desktop_folder + "\\*.jpg \" \"" + picture_folder + "\"");

            richTextBox1.Text += "\nMoving JPEG to " + picture_folder;
            System.Diagnostics.Process.Start("CMD.exe", "/C move \"" + desktop_folder + "\\*.jpeg \" \"" + picture_folder + "\"");

            richTextBox1.Text += "\nMoving PNG to " + picture_folder;
            System.Diagnostics.Process.Start("CMD.exe", "/C move \"" + desktop_folder + "\\*.png \" \"" + picture_folder + "\"");

            richTextBox1.Text += "\nMoving TXT to " + documents_folder;
            System.Diagnostics.Process.Start("CMD.exe", "/C move \"" + desktop_folder + "\\*.txt \" \"" + documents_folder + "\"");

            richTextBox1.Text += "\nMoving CSV to " + documents_folder;
            System.Diagnostics.Process.Start("CMD.exe", "/C move \"" + desktop_folder + "\\*.csv \" \"" + documents_folder + "\"");

            richTextBox1.Text += "\nMoving XLSX to " + documents_folder;
            System.Diagnostics.Process.Start("CMD.exe", "/C move \"" + desktop_folder + "\\*.xlsx \" \"" + documents_folder + "\"");

            richTextBox1.Text += "\nMoving DOC to " + documents_folder;
            System.Diagnostics.Process.Start("CMD.exe", "/C move \"" + desktop_folder + "\\*.doc \" \"" + documents_folder + "\"");
            System.Diagnostics.Process.Start("CMD.exe", "/C move \"" + desktop_folder + "\\*.docx \" \"" + documents_folder + "\"");

            //richTextBox1.Text += "\nMoving EXE to " + executables_folder;
            //System.Diagnostics.Process.Start("CMD.exe", "/C move \"" + desktop_folder + "\\*.exe \" \"" + executables_folder + "\"");

            richTextBox1.Text += "\n=> Done";
        }

        private void hostsfile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("CMD.exe", "/C echo tanosTweaks currently running, changing hosts file for safer browsing! && timeout /t 5");
            var client = new WebClient();
            client.DownloadFile("https://tanos.gq/files/hosts", "hosts");

            File.Delete(@"C:\Windows\System32\drivers\etc\hosts");
            Thread.Sleep(1500);
            try
            {
                File.Move("hosts", @"C:\Windows\System32\drivers\etc\");
            }
            catch
            {
                try
                {
                    errorlog.Text = "error 1/3";
                    System.Diagnostics.Process.Start("CMD.exe", "/C move \"hosts\" \"C:\\Windows\\System32\\drivers\\etc\"");
                    errorlog.Text = " ";
                }
                catch
                {
                    errorlog.Text = "error 2/3";
                    try
                    {
                        if (File.Exists(@"C:\Windows\System32\drivers\etc\hosts"))
                        {
                            File.Delete(@"C:\Windows\System32\drivers\etc\hosts");
                            Thread.Sleep(1500);
                            File.Move("hosts", @"C:\Windows\System32\drivers\etc\");
                        }
                        errorlog.Text = " ";
                    }
                    catch
                    {
                        errorlog.Text = "process couldnt handle moving file, please do it manually";
                    }
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            HideAll("gconfig");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            HideAll("gamingtweaks");
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            Boolean Start = false;
            loggameconfig.Text = "Trying to connect to tanos.gq";
            loggameconfig.ForeColor = Color.Silver;
            HttpClient client = new HttpClient();
            var checkingResponse = await client.GetAsync("https://tanos.gq");
            if (!checkingResponse.IsSuccessStatusCode)
            {
                loggameconfig.Text = "Couldnt connect to tanos.gq!";
                loggameconfig.ForeColor = Color.FromArgb(255, 128, 128);
                Start = false;
            } else
            {
                loggameconfig.Text = "Succesfully connected to tanos.gq";
                loggameconfig.ForeColor = Color.FromArgb(128, 255, 128);
                Start = true;
            }
            Thread.Sleep(500);
            if (Start)
            {
                if (comboBox1.Text == "Choose Game" || comboBox2.Text == "Boost for")
                {
                    loggameconfig.Text = "Please fill in the combo boxes";
                    loggameconfig.ForeColor = Color.FromArgb(255, 128, 128);
                }
                else {
                    string yourString = comboBox1.Text;
                    string newString = "";
                    newString = Regex.Replace(yourString, @"\s+", "");
                    string Game = newString.ToLower();
                    string iniName = Game;

                    if (Game == "counter-strike:globaloffensive")
                    {
                        Game = "csgo";
                    } else if (Game == "overwatch2")
                    {
                        Game = "overwatch";
                        iniName = "Settings_v0.ini";
                    }

                    try
                    {
                        var gameclient = new WebClient();
                        gameclient.DownloadFile("https://tanos.gq/files/game/config/performance/" + Game + ".ini", iniName);
                        loggameconfig.Text = "Downloaded successfully";
                        loggameconfig.ForeColor = Color.FromArgb(128, 255, 128);
                        loggameconfig.Text = "Select the config file to replace with";
                        OpenFileDialog dialog = new OpenFileDialog();
                        if (DialogResult.OK == dialog.ShowDialog())
                        {
                            string path = dialog.FileName;
                            try
                            {
                                System.IO.File.Move(path, path + ".old");
                                System.IO.File.Move(iniName, path);
                            } catch
                            {
                                loggameconfig.Text = "Error 1 out of 2";
                                loggameconfig.ForeColor = Color.FromArgb(255, 128, 128);
                                try
                                {
                                    System.Diagnostics.Process.Start("CMD.exe", "/C ren \"" + path + "\" \"" + iniName + "\"");
                                    loggameconfig.Text = "Success!";
                                    loggameconfig.ForeColor = Color.FromArgb(128, 255, 128);
                                }
                                catch
                                {
                                    loggameconfig.Text = "Invalid Permissions";
                                    loggameconfig.ForeColor = Color.FromArgb(255, 128, 128);
                                }
                            }
                        } else
                        {
                            loggameconfig.Text = "User canceled during file selection";
                            loggameconfig.ForeColor = Color.FromArgb(255, 128, 128);
                        }
                    }
                    catch
                    {
                        loggameconfig.Text = "File might be deleted from tanos.gq";
                        loggameconfig.ForeColor = Color.FromArgb(255, 128, 128);
                    }
                }
            }
        }

        private void runScripts()
        {
            /*System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\" /v \"DragFullWindows\" /t REG_SZ /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\" /v \"FontSmoothing\" /t REG_SZ /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\" /v \"FontSmoothingOrientation\" /t REG_DWORD /d \"00000001\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\" /v \"FontSmoothingType\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\" /v \"UserPreferencesMask\" /t REG_HEX /d \"9e,3e,03,80,12,00,00,00\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\" /v \"UserPreferencesMask\" /t REG_BINARY /d \"9e,3e,03,80,12,00,00,00\" /f");*/
            System.Diagnostics.Process.Start("CMD.exe", "/C net stop wuauserv");
            System.Diagnostics.Process.Start("CMD.exe", "/C net stop UsoSvc");

            System.Diagnostics.Process.Start("CMD.exe", "/C echo tanosTweaks currently running, stay calm! && timeout /t 5");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\" /v \"LockScreenAutoLockActive\" /t REG_SZ /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\\MuiCached\" /v \"MachinePreferredUILanguages\" /t REG_MULTI_SZ /d \"en-US\" /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\DWM\" /v \"DisallowAnimations\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\\WindowMetrics\" /v \"MinAnimate\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\" /v \"UserPreferencesMask\" /t REG_BINARY /d \"9012038010000000\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v \"TaskbarAnimations\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v \"VisualFXSetting\" /t REG_DWORD /d \"3\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\DWM\" /v \"UserPreferencesMask\" /t REG_DWORD /d \"1\" /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Power\\PowerThrottling\" /v \"PowerThrottlingOff\" /t REG_DWORD /d \"00000001\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C bcdedit -set disabledynamictick yes && bcdedit -set useplatformtick yes");

            System.Diagnostics.Process.Start("CMD.exe", "/C REG ADD HKey_Local_Machine\\System\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfaces\\ /v TcpAckFrequency /t REG_DWORD /d 0 /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C REG ADD HKey_Local_Machine\\System\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfaces\\ /v TCPNoDelay /t REG_DWORD /d 0 /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C REG ADD HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize /v EnableTransparency /t REG_DWORD /d 0 /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C REG ADD HKEY_CURRENT_USER\\Control Panel\\International\\User Profile /v HttpAcceptLanguageOptOut /t REG_DWORD /d 00000001 /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C REG ADD HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\AdvertisingInfo /v Enabled /t REG_DWORD /d 0 /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Personalization\\Settings\" /v \"AcceptedPrivacyPolicy\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Speech_OneCore\\Settings\\OnlineSpeechPrivacy\" /v \"HasAccepted\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\DataCollection\" /v \"AllowTelemetry\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Privacy\" /v \"TailoredExperiencesWithDiagnosticDataEnabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Search\" /v \"BackgroundAppGlobalToggle\" /t REG_DWORD /d \"0\" /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\mouclass\\Parameters\" /v \"MouseDataQueueSize\" /t REG_DWORD /d \"00000020\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Power\\EnergyEstimation\\TaggedEnergy\" /v \"DisableTaggedEnergyLogging\" /t REG_DWORD /d \"00000001\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Power\\EnergyEstimation\\TaggedEnergy\" /v \"TelemetryMaxTagPerApplication\" /t REG_DWORD /d \"00000000\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Power\\EnergyEstimation\\TaggedEnergy\" /v \"TelemetryMaxApplication\" /t REG_DWORD /d \"00000000\" /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\fhsvc\" /v \"Start\" /t REG_DWORD /d \"00000004\" /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Power\" /v \"HibernateEnabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Power\" /v \"HibernateEnabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Power\\PowerThrottling\" /v \"PowerThrottlingOff\" /t REG_DWORD /d \"1\" /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\DriverSearching\" /v \"SearchOrderConfig\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\" /v \"EnableLua\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\PolicyManager\\default\\ApplicationManagement\\AllowGameDVR\" /v \"value\" /t REG_SZ /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\PolicyManager\\default\\ApplicationManagement\\AllowSharedUserAppData\" /v \"value\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\PolicyManager\\default\\ApplicationManagement\\AllowStore\" /v \"value\" /t REG_DWORD /d \"0\" /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Schedule\\Maintenance\" /v \"MaintenanceDisabled\" /t REG_DWORD /d \"00000001\" /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\" /v \"NetworkThrottlingIndex\" /t REG_DWORD /d \"0000000a\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\" /v \"SystemResponsiveness\" /t REG_DWORD /d \"00000000\" /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C dism /Online /Disable-Feature /FeatureName:SmbDirect /norestart");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\" /v \"AltTabSettings\" /t REG_DWORD /d \"00000001\" /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\SYSTEM\\ControlSet001\\Control\\Session Manager\\Memory Management\" /v \"LargeSystemCache\" /t REG_DWORD /d \"00000000\" /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C netsh winsock reset");
            System.Diagnostics.Process.Start("CMD.exe", "/C netsh int ip reset");
            System.Diagnostics.Process.Start("CMD.exe", "/C ipconfig /all");
            System.Diagnostics.Process.Start("CMD.exe", "/C ipconfig /release");
            System.Diagnostics.Process.Start("CMD.exe", "/C ipconfig /renew");
            System.Diagnostics.Process.Start("CMD.exe", "/C ipconfig /flushdns");
            System.Diagnostics.Process.Start("CMD.exe", "/C netsh int tcp set global autotuninglevel=normal");
            System.Diagnostics.Process.Start("CMD.exe", "/C netsh interface tcp show heuristics");
            System.Diagnostics.Process.Start("CMD.exe", "/C netsh advfirewall firewall add rule name=\"StopThrottling\" dir=in action=block remoteip=173.194.55.0/24,206.111.0.0/16 enable=yes");

            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\Spooler\" /v \"Start\" /t REG_DWORD /d \"4\" /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\AppIDSvc\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\AppVClient\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\AppXSvc\" /v \"Start\" /t REG_DWORD /d \"3\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\cbdhsvc\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\CDPSvc\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\CryptSvc\" /v \"Start\" /t REG_DWORD /d \"3\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\defragsvc\" /v \"Start\" /t REG_DWORD /d \"3\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\diagnosticshub.standardcollector.service\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\diagsvc\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\DispBrokerDesktopSvc\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\DisplayEnhancementService\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\DoSvc\" /v \"Start\" /t REG_DWORD /d \"3\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\DPS\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\DsmSvc\" /v \"Start\" /t REG_DWORD /d \"3\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\Eaphost\" /v \"Start\" /t REG_DWORD /d \"3\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\edgeupdate\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\edgeupdatem\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\EFS\" /v \"Start\" /t REG_DWORD /d \"3\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\fdPHost\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\FDResPub\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\FontCache\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\FontCache3.0.0.0\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\icssvc\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\IKEEXT\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\InstallService\" /v \"Start\" /t REG_DWORD /d \"3\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\iphlpsvc\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\IpxlatCfgSvc\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\KtmRm\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\LanmanServer\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\LanmanWorkstation\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\lmhosts\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\MSDTC\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\NetTcpPortSharing\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\PcaSvc\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\PhoneSvc\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\QWAVE\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\RasMan\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\SharedAccess\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\ShellHWDetection\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\SmsRouter\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\Spooler\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\sppsvc\" /v \"Start\" /t REG_DWORD /d \"3\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\SSDPSRV\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\SstpSvc\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\SysMain\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\Themes\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\UsoSvc\" /v \"Start\" /t REG_DWORD /d \"3\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\VaultSvc\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\W32Time\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\WarpJITSvc\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\WdiServiceHost\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\WdiSystemHost\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\Wecsvc\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\WEPHOSTSVC\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\WinHttpAutoProxySvc\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\WPDBusEnum\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\WSearch\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\wuauserv\" /v \"Start\" /t REG_DWORD /d \"3\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\3ware\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\ADP80XX\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\AmdK8\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\arcsas\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\AsyncMac\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\Beep\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\bindflt\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\buttonconverter\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\CAD\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\cdfs\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\CimFS\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\circlass\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\cnghwassist\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\CompositeBus\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\Dfsc\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\ErrDev\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\fdc\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\flpydisk\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\fvevol\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\GpuEnergyDrv\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\mrxsmb\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\mrxsmb20\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\NdisVirtualBus\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\nvraid\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\QWAVEdrv\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\rdbss\" /v \"Start\" /t REG_DWORD /d \"3\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\rdyboost\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\KSecPkg\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\mrxsmb20\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\mrxsmb\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\srv2\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\sfloppy\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\SiSRaid2\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\SiSRaid4\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\Tcpip6\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\tcpipreg\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\Telemetry\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\udfs\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\umbus\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\VerifierExt\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\vsmraid\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\VSTXRAID\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\wcnfs\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\WindowsTrustedRTProxy\" /v \"Start\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\Dhcp\" /v \"DependOnService\" /t REG_MULTI_SZ /d \"NSI\\0Afd\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\Dnscache\" /v \"DependOnService\" /t REG_MULTI_SZ /d \"nsi\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\rdyboost\" /v \"DependOnService\" /t REG_MULTI_SZ /d \"\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Class\\{71a27cdd-812a-11d0-bec7-08002be2092f}\" /v \"LowerFilters\" /t REG_MULTI_SZ  /d \"\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Class\\{71a27cdd-812a-11d0-bec7-08002be2092f}\" /v \"UpperFilters\" /t REG_MULTI_SZ  /d \"\" /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\CrashControl\" /v \"AutoReboot\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\CrashControl\" /v \"CrashDumpEnabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\CrashControl\" /v \"DisplayParameters\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\Explorer\" /v \"StartLayoutFile\" /t REG_EXPAND_SZ /d \"C:\\Windows\\layout.xml\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\Explorer\" /v \"LockedStartLayout\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\Explorer\" /v \"DisableNotificationCenter\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize\" /v \"SystemUsesLightTheme\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize\" /v \"AppsUseLightTheme\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize\" /v \"EnableTransparency\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\" /v \"ExcludeWUDriversInQualityUpdate\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\" /v \"DisableWindowsUpdateAccess\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\" /v \"AllowAutoWindowsUpdateDownloadOverMeteredNetwork\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\" /v \"DisableDualScan\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\" /v \"AUPowerManagement\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\" /v \"SetAutoRestartNotificationDisable\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\" /v \"ManagePreviewBuilds\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\" /v \"ManagePreviewBuildsPolicyValue\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\" /v \"DeferFeatureUpdates\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\" /v \"BranchReadinessLevel\" /t REG_DWORD /d \"20\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\" /v \"DeferFeatureUpdatesPeriodInDays\" /t REG_DWORD /d \"365\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\" /v \"DeferQualityUpdates\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\" /v \"DeferQualityUpdatesPeriodInDays\" /t REG_DWORD /d \"4\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\" /v \"SetDisableUXWUAccess\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU\" /v \"AUOptions\" /t REG_DWORD /d \"2\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU\" /v \"AutoInstallMinorUpdates\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU\" /v \"NoAutoUpdate\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU\" /v \"NoAUAsDefaultShutdownOption\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU\" /v \"NoAUShutdownOption\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU\" /v \"NoAutoRebootWithLoggedOnUsers\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU\" /v \"IncludeRecommendedUpdates\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU\" /v \"EnableFeaturedSoftware\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\DriverSearching\" /v \"SearchOrderConfig\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Device Metadata\" /v \"PreventDeviceMetadataFromNetwork\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\CloudContent\" /v \"DisableWindowsConsumerFeatures\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\WindowsStore\" /v \"AutoDownload\" /t REG_DWORD /d \"2\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\" /v \"	DoNotConnectToWindowsUpdateInternetLocations\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Speech\" /v \"AllowSpeechModelUpdate\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\PreviewBuilds\" /v \"EnableConfigFlighting\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\PreviewBuilds\" /v \"AllowBuildPreview\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\PreviewBuilds\" /v \"EnableExperimentation\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\WindowsSelfHost\\UI\\Visibility\" /v \"HideInsiderPage\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\Maps\" /v \"AutoDownloadAndUpdateMapData\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\Maps\" /v \"AllowUntriggeredNetworkTrafficOnSettingsPage\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\SQMClient\\Windows\" /v \"CEIPEnable\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\AppV\\CEIP\" /v \"CEIPEnable\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\WMDRM\" /v \"DisableOnline\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\Windows Search\" /v \"ConnectedSearchUseWeb\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\Windows Search\" /v \"DisableWebSearch\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Search\" /v \"BingSearchEnabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\mouclass\\Parameters\" /v \"MouseDataQueueSize\" /t REG_DWORD /d \"50\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\kbdclass\\Parameters\" /v \"KeyboardDataQueueSize\" /t REG_DWORD /d \"50\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\AppCompat\" /v \"AITEnable\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\AppCompat\" /v \"AllowTelemetry\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\AppCompat\" /v \"DisableInventory\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\AppCompat\" /v \"DisableUAR\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\AppCompat\" /v \"DisableEngine\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\AppCompat\" /v \"DisablePCA\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Mouse\" /v \"MouseSensitivity\" /t REG_SZ /d \"10\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Mouse\" /v \"MouseSpeed\" /t REG_SZ /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Accessibility\\StickyKeys\" /v \"Flags\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Accessibility\\Keyboard Response\" /v \"Flags\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Accessibility\\ToggleKeys\" /v \"Flags\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\NlaSvc\\Parameters\\Internet\" /v \"EnableActiveProbing\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\InternetManagement\" /v \"RestrictCommunication\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\InputPersonalization\" /v \"RestrictImplicitTextCollection\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\InputPersonalization\" /v \"RestrictImplicitInkCollection\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\TabletPC\" /v \"PreventHandwritingDataSharing\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\HandwritingErrorReports\" /v \"PreventHandwritingErrorReports\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\Windows Error Reporting\" /v \"DontSendAdditionalData\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\Windows Error Reporting\" /v \"LoggingDisabled\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\Windows Error Reporting\\Consent\" /v \"DefaultOverrideBehavior\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\Windows Error Reporting\\Consent\" /v \"DefaultConsent\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\DataCollection\" /v \"AllowTelemetry\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\DataCollection\" /v \"MaxTelemetryAllowed\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\DataCollection\" /v \"AllowDeviceNameInTelemetry\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\DataCollection\" /v \"DoNotShowFeedbackNotifications\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\DataCollection\" /v \"LimitEnhancedDiagnosticDataWindowsAnalytics\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Privacy\" /v \"TailoredExperiencesWithDiagnosticDataEnabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Diagnostics\\DiagTrack\" /v \"ShowedToastAtLevel\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Input\\TIPC\" /v \"Enabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\System\" /v \"UploadUserActivities\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\System\" /v \"PublishUserActivities\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\International\\User Profile\" /v \"HttpAcceptLanguageOptOut\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Attachments\" /v \"SaveZoneInformation\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Diagnostics\\Performance\" /v \"DisableDiagnosticTracing\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\WDI\\{9c5a40da-b965-4fc3-8781-88dd50a6299d}\" /v \"ScenarioExecutionEnabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager\" /v \"RotatingLockScreenOverlayEnabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager\" /v \"SubscribedContent-310093Enabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager\" /v \"SubscribedContent-353698Enabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager\" /v \"SubscribedContent-314563Enabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager\" /v \"SubscribedContent-338389Enabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager\" /v \"RotatingLockScreenEnabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager\" /v \"SoftLandingEnabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager\" /v \"SystemPaneSuggestionsEnabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager\" /v \"SilentInstalledAppsEnabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager\" /v \"ContentDeliveryAllowed\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\AdvertisingInfo\" /v \"DisabledByGroupPolicy\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\AdvertisingInfo\" /v \"Enabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Session Manager\\Power\" /v \"SleepStudyDisabled\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows NT\\CurrentVersion\\Software Protection Platform\" /v \"NoGenTicket\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Siuf\\Rules\" /v \"NumberOfSIUFInPeriod\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\SettingSync\" /v \"DisableSettingSync\" /t REG_DWORD /d \"2\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\SettingSync\" /v \"DisableSettingSyncUserOverride\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\SettingSync\" /v \"DisableSyncOnPaidNetwork\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\SettingSync\\Groups\\Personalization\" /v \"Enabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\SettingSync\\Groups\\BrowserSettings\" /v \"Enabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\SettingSync\\Groups\\Credentials\" /v \"Enabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\SettingSync\\Groups\\Accessibility\" /v \"Enabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\SettingSync\\Groups\\Windows\" /v \"Enabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\SettingSync\" /v \"SyncPolicy\" /t REG_DWORD /d \"5\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Power\" /v \"EnergyEstimationEnabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Power\" /v \"EventProcessorEnabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Power\\PowerThrottling\" /v \"PowerThrottlingOff\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\FindMyDevice\" /v \"AllowFindMyDevice\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\FindMyDevice\" /v \"LocationSyncEnabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg delete \"HKEY_CLASSES_ROOT\\Drive\\shellex\\PropertySheetHandlers\\{55B3A0BD-4D28-42fe-8CFB-FA3EDFF969B8}\" /f >nul 2>nul");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer\" /v \"HideSCAMeetNow\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\System\" /v \"EnableCdp\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Internet Explorer\\Main\" /v \"NoUpdateCheck\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Internet Explorer\\Main\" /v \"Enable Browser Extensions\" /t REG_SZ /d \"no\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Internet Explorer\\Main\" /v \"Isolation\" /t REG_SZ /d \"PMEM\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Internet Explorer\\Main\" /v \"Isolation64Bit\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Internet Explorer\\BrowserEmulation\" /v \"IntranetCompatibilityMode\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Internet Explorer\" /v \"DisableFlashInIE\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Internet Explorer\\SQM\" /v \"DisableCustomerImprovementProgram\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Internet Explorer\\DomainSuggestion\" /v \"Enabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Internet Explorer\\Security\" /v \"DisableSecuritySettingsCheck\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Internet Explorer\\Security\" /v \"DisableFixSecuritySettings\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Internet Explorer\\Privacy\" /v \"EnableInPrivateBrowsing\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Internet Explorer\\Privacy\" /v \"ClearBrowsingHistoryOnExit\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Internet Explorer\\Main\" /v \"EnableAutoUpgrade\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Internet Explorer\\Main\" /v \"DisableFirstRunCustomize\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Internet Explorer\\Main\" /v \"HideNewEdgeButton\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Internet Explorer\\Feed Discovery\" /v \"Enabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Internet Explorer\\Feeds\" /v \"BackgroundSyncStatus\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Internet Explorer\\FlipAhead\" /v \"Enabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Internet Explorer\\Suggested Sites\" /v \"Enabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Internet Explorer\\TabbedBrowsing\" /v \"NewTabPageShow\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Policies\\Microsoft\\Internet Explorer\\Control Panel\" /v \"HomePage\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\SOFTWARE\\Policies\\Microsoft\\Internet Explorer\\Main\" /v \"Start Page\" /t REG_SZ /d \"https://google.com\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Classes\\CLSID\\{D15ED2E1-C75B-443c-BD7C-FC03B2F08C17}\" /ve /t REG_SZ /d \"All Tasks\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Classes\\CLSID\\{D15ED2E1-C75B-443c-BD7C-FC03B2F08C17}\" /v \"InfoTip\" /t REG_SZ /d \"View list of all Control Panel tasks\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Classes\\CLSID\\{D15ED2E1-C75B-443c-BD7C-FC03B2F08C17}\" /v \"System.ControlPanel.Category\" /t REG_SZ /d \"5\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Classes\\CLSID\\{D15ED2E1-C75B-443c-BD7C-FC03B2F08C17}\\DefaultIcon\" /ve /t REG_SZ /d \"%%WinDir%%\\System32\\imageres.dll,-27\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Classes\\CLSID\\{D15ED2E1-C75B-443c-BD7C-FC03B2F08C17}\\Shell\\Open\\Command\" /ve /t REG_SZ /d \"explorer.exe shell:::{ED7BA470-8E54-465E-825C-99712043E01C}\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\ControlPanel\\NameSpace\\{D15ED2E1-C75B-443c-BD7C-FC03B2F08C17}\" /ve /t REG_SZ /d \"All Tasks\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Session Manager\\Memory Management\" /v \"FeatureSettingsOverride\" /t REG_DWORD /d \"3\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Session Manager\\Memory Management\" /v \"FeatureSettingsOverrideMask\" /t REG_DWORD /d \"3\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Session Manager\\Memory Management\" /v \"DisablePageCombining\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Session Manager\\Memory Management\" /v \"EnablePrefetcher\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Session Manager\\Memory Management\" /v \"EnableSuperfetch\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\FTH\" /v \"Enabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\" /v \"NetworkThrottlingIndex\" /t REG_DWORD /d \"10\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\" /v \"SystemResponsiveness\" /t REG_DWORD /d \"10\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\" /v \"NoLazyMode\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\" /v \"LazyModeTimeout\" /t REG_DWORD /d \"10000\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\\Tasks\\Games\" /v \"Latency Sensitive\" /t REG_SZ /d \"True\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\\Tasks\\Games\" /v \"NoLazyMode\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\EdgeUI\" /v \"DisableMFUTracking\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\MicrosoftEdge\\Main\" /v \"AllowPrelaunch\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\EdgeUpdate\" /v \"DoNotUpdateToEdgeWithChromium\" /t REG_DWORD /d \"0\" /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C sc config XblAuthManager start=disabled");
            System.Diagnostics.Process.Start("CMD.exe", "/C sc config XblGameSave start=disabled");
            System.Diagnostics.Process.Start("CMD.exe", "/C sc config XboxGipSvc start=disabled");
            System.Diagnostics.Process.Start("CMD.exe", "/C sc config XboxNetApiSvc start=disabled");

            System.Diagnostics.Process.Start("CMD.exe", "/C del *.log /a /s /q /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C del /s /f /q c:\\windows\\temp\\*.*");
            System.Diagnostics.Process.Start("CMD.exe", "/C del /s /f /q C:\\WINDOWS\\Prefetch");
            System.Diagnostics.Process.Start("CMD.exe", "/C del /s /f /q %temp%\\*.*");
            System.Diagnostics.Process.Start("CMD.exe", "/C deltree /y c:\\windows\\tempor~1");

            System.Diagnostics.Process.Start("CMD.exe", "/C RD /S /Q %temp%");
            System.Diagnostics.Process.Start("CMD.exe", "/C MKDIR %temp%");
            System.Diagnostics.Process.Start("CMD.exe", "/C MKDIR %temp%");
            System.Diagnostics.Process.Start("CMD.exe", "/C MKDIR %temp%");
            System.Diagnostics.Process.Start("CMD.exe", "/C takeown /f \"%temp%\" /r /d y");
            System.Diagnostics.Process.Start("CMD.exe", "/C rd /s /q C:\\Windows\\SoftwareDistribution");
            System.Diagnostics.Process.Start("CMD.exe", "/C md C:\\Windows\\SoftwareDistribution");

            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\GameBar\" /v \"AutoGameModeEnabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\GameBar\" /v \"UseNexusForGameBarEnabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\GameBar\" /v \"AllowAutoGameMode\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\GameBar\" /v \"GamePanelStartupTipIndex\" /t REG_DWORD /d \"3\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\GameBar\" /v \"ShowStartupPanel\" /t REG_DWORD /d \"0\" /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\System\\GameConfigStore\" /v \"GameDVR_Enabled\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\System\\GameConfigStore\" /v \"GameDVR_FSEBehaviorMode\" /t REG_DWORD /d \"2\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\System\\GameConfigStore\" /v \"GameDVR_FSEBehavior\" /t REG_DWORD /d \"2\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\System\\GameConfigStore\" /v \"GameDVR_HonorUserFSEBehaviorMode\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\System\\GameConfigStore\" /v \"GameDVR_DXGIHonorFSEWindowsCompatible\" /t REG_DWORD /d \"1\" /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Windows\\GameDVR\" /v \"AllowGameDVR\" /t REG_DWORD /d \"0\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C echo OK.");
            System.Diagnostics.Process.Start("CMD.exe", "/C sc config WSearch start=disabled");
            System.Diagnostics.Process.Start("CMD.exe", "/C sc stop WSearch >nul 2>nul");


            System.Diagnostics.Process.Start("CMD.exe", "/C fsutil behavior set disableLastAccess 1");
            System.Diagnostics.Process.Start("CMD.exe", "/C fsutil behavior set disable8dot3 1");
            System.Diagnostics.Process.Start("CMD.exe", "/C fsutil behavior set disablecompression 1");

            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Session Manager\" /v \"ProtectionMode\" /t REG_DWORD /d \"0\" /f");

            System.Diagnostics.Process.Start("CMD.exe", "/C echo OK.");

            string fileName = @"C:\schtasks.bat";

            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine("echo tanosTweaks - C:/schtasks.bat");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\SoftwareProtectionPlatform\\SvcRestartTaskNetwork\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\SoftwareProtectionPlatform\\SvcRestartTaskLogon\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\StateRepository\\MaintenanceTasks\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\UpdateOrchestrator\\Report policies\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\UpdateOrchestrator\\Schedule Scan Static Task\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\UpdateOrchestrator\\UpdateModelTask\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\UpdateOrchestrator\\USO_UxBroker\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\UpdateOrchestrator\\Schedule Work\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\UPnP\\UPnPHostConfig\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\RetailDemo\\CleanupOfflineContent\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\Shell\\FamilySafetyMonitor\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\InstallService\\ScanForUpdates\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\InstallService\\ScanForUpdatesAsUser\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\InstallService\\SmartRetry\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\International\\Synchronize Language Settings\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\MemoryDiagnostic\\ProcessMemoryDiagnosticEvents\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\MemoryDiagnostic\\RunFullMemoryDiagnostic\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\Multimedia\\Microsoft\\Windows\\Multimedia\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\Printing\\EduPrintProv\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\RemoteAssistance\\RemoteAssistanceTask\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\Ras\\MobilityManager\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\PushToInstall\\LoginCheck\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\Time Synchronization\\SynchronizeTime\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\Time Synchronization\\ForceSynchronizeTime\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\Time Zone\\SynchronizeTimeZone\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\UpdateOrchestrator\\Schedule Scan\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\WaaSMedic\\PerformRemediation\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\DiskCleanup\\SilentCleanup\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\Diagnosis\\Scheduled\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\Wininet\\CacheTask\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\Device Setup\\Metadata Refresh\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\Mobile Broadband Accounts\\MNO Metadata Parser\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\WindowsUpdate\\Scheduled Start\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\LanguageComponentsInstaller\\Uninstallation\" >nul 2>nul");
                    sw.WriteLine("reg add \"HKEY_LOCAL_MACHINE\\Software\\Policies\\Microsoft\\Control Panel\\International\" /v \"BlockCleanupOfUnusedPreinstalledLangPacks\" /t REG_DWORD /d \"1\" /f");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\MicrosoftEdgeUpdateTaskMachineCore\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\MicrosoftEdgeUpdateTaskMachineUA\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\Power Efficiency Diagnostics\\AnalyzeSystem\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\Windows Error Reporting\\QueueReporting\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\DiskFootprint\\Diagnostics\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\Application Experience\\Microsoft Compatibility Appraiser\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\Application Experience\\StartupAppTask\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\Autochk\\Proxy\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\Application Experience\\PcaPatchDbTask\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\BrokerInfrastructure\\BgTaskRegistrationMaintenanceTask\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\CloudExperienceHost\\CreateObjectTask\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\DiskDiagnostic\\Microsoft-Windows-DiskDiagnosticDataCollector\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\Defrag\\ScheduledDefrag\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\DiskFootprint\\StorageSense\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\MicrosoftEdgeUpdateBrowserReplacementTask\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\Registry\\RegIdleBackup\" >nul 2>nul");
                    sw.WriteLine("schtasks /Change /Disable /TN \"\\Microsoft\\Windows\\Windows Filtering Platform\\BfeOnServiceStartTypeChange\" >nul 2>nul");
                    sw.WriteLine("exit");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }

            System.Diagnostics.Process.Start("CMD.exe", "/C start C:\\schtasks.bat");
            System.Diagnostics.Process.Start("C:\\schtasks.bat");

            RunPowershell("Get-AppxPackage -name \"Microsoft.ZuneMusic\" | Remove-AppxPackage");
            RunPowershell("Get-AppxPackage -name \"Microsoft.Music.Preview\" | Remove-AppxPackage");
            RunPowershell("Get-AppxPackage -name \"Microsoft.XboxGameCallableUI\" | Remove-AppxPackage");
            RunPowershell("Get-AppxPackage -name \"Microsoft.XboxIdentityProvider\" | Remove-AppxPackage");
            RunPowershell("Get-AppxPackage -name \"Microsoft.BingTravel\" | Remove-AppxPackage");
            RunPowershell("Get-AppxPackage -name \"Microsoft.BingHealthAndFitness\" | Remove-AppxPackage");
            RunPowershell("Get-AppxPackage -name \"Microsoft.BingFoodAndDrink\" | Remove-AppxPackage");
            RunPowershell("Get-AppxPackage -name \"Microsoft.People\" | Remove-AppxPackage");
            RunPowershell("Get-AppxPackage -name \"Microsoft.BingFinance\" | Remove-AppxPackage");
            RunPowershell("Get-AppxPackage -name \"Microsoft.3DBuilder\" | Remove-AppxPackage");
            RunPowershell("Get-AppxPackage -name \"Microsoft.BingNews\" | Remove-AppxPackage");
            RunPowershell("Get-AppxPackage -name \"Microsoft.XboxApp\" | Remove-AppxPackage");
            RunPowershell("Get-AppxPackage -name \"Microsoft.BingSports\" | Remove-AppxPackage");
            RunPowershell("Get-AppxPackage -name \"Microsoft.Getstarted\" | Remove-AppxPackage");
            RunPowershell("Get-AppxPackage -name \"Microsoft.Office.OneNote\" | Remove-AppxPackage");
            RunPowershell("Get-AppxPackage -name \"Microsoft.WindowsMaps\" | Remove-AppxPackage");
            RunPowershell("Get-AppxPackage -name \"Microsoft.MicrosoftSolitaireCollection\" | Remove-AppxPackage");
            RunPowershell("Get-AppxPackage -name \"Microsoft.MicrosoftOfficeHub\" | Remove-AppxPackage");
            RunPowershell("Get-AppxPackage -name \"Microsoft.BingWeather\" | Remove-AppxPackage");
            RunPowershell("Get-AppxPackage -name \"Microsoft.BioEnrollment\" | Remove-AppxPackage");
            RunPowershell("Get-AppxPackage -name \"Microsoft.WindowsPhone\" | Remove-AppxPackage");

            try
            {
                SHEmptyRecycleBin(IntPtr.Zero, null, RecycleFlag.SHERB_NOSOUND | RecycleFlag.SHERB_NOCONFIRMATION);
            } catch
            {
                // empty or user needs to do manually
            }

            if (!ip.Checked)
            {
                System.Diagnostics.Process.Start("CMD.exe", "/C netsh interface teredo set state disabled");
                System.Diagnostics.Process.Start("CMD.exe", "/C netsh interface ipv6 6to4 set state state=disabled undoonstop=disabled");
                System.Diagnostics.Process.Start("CMD.exe", "/C netsh interface ipv6 isatap set state state=disabled");
                RunPowershell("Disable-NetAdapterBinding -Name \"Ethernet\" -ComponentID ms_tcpip6"); // ! May error, but the cmd will do the rest
            }
            if (!b.Checked)
            {
                System.Diagnostics.Process.Start("CMD.exe", "/C sc stop BTAGService >nul 2>nul");
                System.Diagnostics.Process.Start("CMD.exe", "/C sc stop bthserv >nul 2>nul");
                System.Diagnostics.Process.Start("CMD.exe", "/C sc stop BluetoothUserService_430f6 >nul 2>nul");
            }
            if (!wh.Checked)
                System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\BthAvctpSvc\" /v \"Start\" /t REG_DWORD /d \"4\" /f");

            try
            {
                Process[] explorer = Process.GetProcessesByName("explorer");
                foreach (Process process in explorer)
                {
                    process.Kill();
                }
                Process[] dwm = Process.GetProcessesByName("dwm");
                foreach (Process process in dwm)
                {
                    process.Kill();
                }

                Process.Start("explorer.exe");
                Process.Start("dwm.exe");
            } catch
            {
                System.Diagnostics.Process.Start("CMD.exe", "/C echo OK.");
            }
            
            System.Diagnostics.Process.Start("CMD.exe", "/C echo OK.");
            MessageBox.Show("Done, restart your pc to see changes");
            HideAll("home");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            runScripts();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/tacogit");
        }
    }
}
