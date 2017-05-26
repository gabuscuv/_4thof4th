using System;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;

namespace _4thof4th{
	//NEIO - No-Ethical Input and Output
	public class NEIO{
        // No Implmentado en esta version
		public void VoiceGamePadInput(){}
		
		[DllImport("user32.dll")]
		private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags,UIntPtr dwExtraInfo);
		private const int KEYEVENTF_EXTENDEDKEY = 0x1;
	    private const int KEYEVENTF_KEYUP = 0x2;
		private byte tmp;
		public void KeyboardLedsController(int s){
			switch(s){
				//Num Lock
				case 0:tmp=0x90;break;
				//Caps Lock
				case 1:tmp=0x14;break;
				//Scroll Lock
				case 2:tmp=0x91;break;
			}
	        keybd_event(tmp, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
            keybd_event(tmp, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP,(UIntPtr)0);
		}

        public String[] getSystemInfoFormatted() {
            String[] tmp = getSystemInfo();
            tmp[2] = tmp[2].Substring(0, 4);
            tmp[5] = tmp[5].Substring(0,tmp[5].Length-1);
            tmp[6] = (Int64.Parse(tmp[6])/(1024*1024)).ToString();

        


            return tmp;
        }
        private String[] getSystemInfo()
        {
            String[,] info = { { "Win32_BIOS", "Version", "Manufacturer", "ReleaseDate" },
                               { "Win32_Processor", "Name", "NumberOfCores", "MaxClockSpeed" },
                               {"Win32_ComputerSystem", "TotalPhysicalMemory", "Manufacturer", "Model" },
                               {"Win32_PhysicalMemory","MemoryType","","" }};
            ManagementObjectSearcher searcher;
            int s_count=0;
			String[] s = new String[(info.GetLength(1)*info.GetLength(0))-info.GetLength(0)];
			for (int i = 0; i < info.GetLength(0); i++){
                for (int j = 1; j < info.GetLength(1);j++){
					if(!info[i,j].Equals("")){
                    searcher = new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM " + info[i, 0]);
                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        s[s_count] = queryObj[info[i,j]].ToString();
						
					}
                        s_count++;
                    }
                }
            }
			
            return s;
        }

		public String[] getRandomNameFiles(int z){
            String[] files= {""};
            switch (Environment.OSVersion.Version.Major) {
                case 5: files= Directory.GetFiles(Environment.GetEnvironmentVariable("HOMEPATH") + "\\Escritorio");break;
                case 6: files= Directory.GetFiles(Environment.GetEnvironmentVariable("HOMEPATH") + "\\Desktop");break;
            }
            Random tmp =new Random();
            String[] finish = new String[z];
            String tmp_string;
            for (int i = 0; i < z; i++) {
                tmp_string=files[tmp.Next(0, files.Length)];
                finish[i] = tmp_string.Substring(tmp_string.LastIndexOf("\\")+2);
            }

            return finish;
        }
    }
	
		  
}
