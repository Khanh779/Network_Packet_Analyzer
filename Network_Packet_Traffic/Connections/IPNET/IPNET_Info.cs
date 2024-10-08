using System;
using System.Runtime.InteropServices;

namespace Network_Packet_Analyzer.Connections.IPNET
{
    public class IPNET_Info
    {

        [DllImport("iphlpapi.dll")]
        extern static int GetIpStatistics(ref MIB_IPSTATS pStats);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        extern static int GetIpNetTable(ref MIB_IPNETTABLE pTable, long PULONG, bool bOrder);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        static extern int GetIpNetTable(IntPtr pIpNetTable, ref int pdwSize, bool bOrder);

        public static MIB_IPNETTABLE GetIpNetTable()
        {
            MIB_IPNETTABLE arpTable = new MIB_IPNETTABLE();
            int bufferSize = 0;
            int result = GetIpNetTable(IntPtr.Zero, ref bufferSize, false);
            IntPtr buffer = Marshal.AllocHGlobal(bufferSize);
            try
            {
                result = GetIpNetTable(buffer, ref bufferSize, false);
                if (result != 0) throw new System.ComponentModel.Win32Exception(result); // 0 is NO_ERROR

                arpTable.dwNumEntries = (uint)Marshal.ReadIntPtr(buffer); // Get the number of entries in the table
                IntPtr currentBuffer = (IntPtr)((long)buffer + Marshal.SizeOf(typeof(uint)));

                var temp = new MIB_IPNETROW[arpTable.dwNumEntries];

                // Lặp qua từng dòng trong bảng ARP
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = (MIB_IPNETROW)Marshal.PtrToStructure(currentBuffer, typeof(MIB_IPNETROW));

                    currentBuffer += Marshal.SizeOf(temp[i]);
                }

                arpTable.table = temp;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }

            return arpTable;
        }


    }
}
