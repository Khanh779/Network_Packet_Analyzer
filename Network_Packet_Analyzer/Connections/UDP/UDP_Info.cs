using System;
using System.Runtime.InteropServices;
using static Network_Packet_Analyzer.Connections.NetHelper;

namespace Network_Packet_Analyzer.Connections.UDP
{
    public class UDP_Info
    {
        [DllImport("iphlpapi.dll")]
        extern static int GetUdpStatistics(ref MIB_UDPSTATS pStats);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        static extern int GetExtendedUdpTable(IntPtr pUdpTable, ref int pdwSize, bool bOrder, int ulAf, UDP_TABLE_CLASS TableClass, uint reserved);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        extern static int AllocateAndGetUdpExTableFromStack(ref IntPtr pTable, bool bOrder, IntPtr heap, int zero, int flags);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        static extern int GetUdpTable(byte[] pUdpTable, out int pdwSize, bool bOrder);

        public static void GetUdp()
        {
            MIB_UDPTABLE_OWNER_PID udpTable = GetUdpTable();

            Console.WriteLine("UDP Table:");
            Console.WriteLine($"Number of entries: {udpTable.dwNumEntries}");

            foreach (MIB_UDPROW_OWNER_PID udpRow in udpTable.table)
            {
                Console.WriteLine($"Local Address: {ConvertIpAddress((int)udpRow.dwLocalAddr)}:{udpRow.dwLocalPort}");
                Console.WriteLine($"Process ID: {udpRow.dwOwningPid}");
                Console.WriteLine();
            }
        }



        //Lấy thông tin UDP và TCP có hiện Process ID dùng MIB_TCPROW_OWNER_PID[] và MIB_UDPROW_OWNER_PID[]



        public static MIB_UDPTABLE_OWNER_PID GetUdpTable()
        {
            MIB_UDPTABLE_OWNER_PID udpTable = new MIB_UDPTABLE_OWNER_PID();
            int buffSize = 0;
            int ret = GetExtendedUdpTable(IntPtr.Zero, ref buffSize, true, 2, UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID, 0);
            IntPtr buffTable = Marshal.AllocHGlobal(buffSize);

            try
            {
                ret = GetExtendedUdpTable(buffTable, ref buffSize, true, 2, UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID, 0);
                if (ret != NO_ERROR)
                {
                    return udpTable;
                }

                udpTable.dwNumEntries = (uint)Marshal.ReadIntPtr(buffTable);

                //IntPtr buffTablePointer = buffTable + Marshal.SizeOf(udpTable.dwNumEntries);
                IntPtr buffTablePointer = (IntPtr)((long)buffTable + Marshal.SizeOf(typeof(uint)));
                MIB_UDPROW_OWNER_PID[] udpRows = new MIB_UDPROW_OWNER_PID[udpTable.dwNumEntries];

                for (int i = 0; i < udpTable.dwNumEntries; i++)
                {
                    udpRows[i] = (MIB_UDPROW_OWNER_PID)Marshal.PtrToStructure(buffTablePointer, typeof(MIB_UDPROW_OWNER_PID));
                    buffTablePointer += Marshal.SizeOf(udpRows[i]);
                }

                udpTable.table = udpRows;
            }
            finally
            {
                Marshal.FreeHGlobal(buffTable);
            }

            return udpTable;
        }
    }

}
