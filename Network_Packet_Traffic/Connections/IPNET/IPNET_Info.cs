using System;
using System.Runtime.InteropServices;

namespace Network_Packet_Traffic.Connections.IPNET
{
    public class IPNET_Info
    {

        [DllImport("iphlpapi.dll")]
        extern static int GetIpStatistics(ref MIB_IPSTATS pStats);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        extern static int GetIpNetTable(ref MIB_IPNETTABLE pTable, long PULONG, bool bOrder);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        static extern int GetIpNetTable(IntPtr pIpNetTable, ref int pdwSize, bool bOrder);

        public static MIB_IPNETTABLE GetIpNetable()
        {
            MIB_IPNETTABLE arpTable = new MIB_IPNETTABLE();

            int bufferSize = 0;

            int result = GetIpNetTable(IntPtr.Zero, ref bufferSize, false);
            IntPtr buffer = Marshal.AllocHGlobal(bufferSize);

            try
            {
                // Lấy bảng ARP
                result = GetIpNetTable(buffer, ref bufferSize, false);
                if (result != 0) throw new System.ComponentModel.Win32Exception(result);

                // Lấy số lượng hàng trong bảng ARP
                int entryCount = Marshal.ReadInt32(buffer);
                IntPtr currentBuffer = (IntPtr)((long)buffer + Marshal.SizeOf(typeof(int)));
                arpTable.table = new MIB_IPNETROW[entryCount];

                // Lặp qua từng dòng trong bảng ARP
                for (int i = 0; i < entryCount; i++)
                {
                    arpTable.table[i] = (MIB_IPNETROW)Marshal.PtrToStructure(currentBuffer, typeof(MIB_IPNETROW));

                    currentBuffer += Marshal.SizeOf(typeof(MIB_IPNETROW));
                }


            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }

            return arpTable;
        }


        //public static MIB_IPNETTABLE GetArpTable()
        //{
        //    MIB_IPNETTABLE arpTable = new MIB_IPNETTABLE();

        //    int buffSize = 0;
        //    int result = GetIpNetTable(IntPtr.Zero, ref buffSize, false);
        //    IntPtr buffTable = Marshal.AllocHGlobal(buffSize);

        //    try
        //    {
        //        result = GetIpNetTable(buffTable, ref buffSize, false);

        //        if (result != NO_ERROR)
        //        {
        //            Console.WriteLine("Error getting ARP table. Error code: " + result);
        //            return arpTable;
        //        }

        //        arpTable = (MIB_IPNETTABLE)Marshal.PtrToStructure(buffTable, typeof(MIB_IPNETTABLE));
        //        arpTable.table = new MIB_IPNETROW[arpTable.dwNumEntries];

        //        IntPtr buffTablePointer = buffTable + Marshal.SizeOf(arpTable.dwNumEntries);

        //        for (int i = 0; i < arpTable.dwNumEntries; i++)
        //        {
        //            arpTable.table[i] = (MIB_IPNETROW)Marshal.PtrToStructure(buffTablePointer, typeof(MIB_IPNETROW));
        //            buffTablePointer += Marshal.SizeOf(arpTable.table[i]);
        //        }
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(buffTable);
        //    }

        //    return arpTable;
        //}
    }
}
