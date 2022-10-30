using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
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

            if (except == isAt)
                return;

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
            /*System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\" /v \"DragFullWindows\" /t REG_SZ /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\" /v \"FontSmoothing\" /t REG_SZ /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\" /v \"FontSmoothingOrientation\" /t REG_DWORD /d \"00000001\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\" /v \"FontSmoothingType\" /t REG_DWORD /d \"1\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\" /v \"UserPreferencesMask\" /t REG_HEX /d \"9e,3e,03,80,12,00,00,00\" /f");
            System.Diagnostics.Process.Start("CMD.exe", "/C reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\" /v \"UserPreferencesMask\" /t REG_BINARY /d \"9e,3e,03,80,12,00,00,00\" /f");*/
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

            Process[] explorer = Process.GetProcessesByName("explorer");
            foreach (Process process in explorer)
            {
                process.Kill();
            }

            Process.Start("explorer.exe");

            System.Diagnostics.Process.Start("CMD.exe", "/C echo OK.");
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
    }
}
