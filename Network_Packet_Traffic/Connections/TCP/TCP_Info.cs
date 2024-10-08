using System;
using System.Runtime.InteropServices;
using static Network_Packet_Analyzer.Connections.NetHelper;

namespace Network_Packet_Analyzer.Connections.TCP
{
    public class TCP_Info
    {
        [DllImport("iphlpapi.dll", SetLastError = true)]
        static extern int GetTcpTable(byte[] pTcpTable, out int pdwSize, bool bOrder);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        static extern int GetExtendedTcpTable(IntPtr pTcpTable, ref int pdwSize, bool bOrder, int ulAf, TCP_TABLE_CLASS TableClass, uint reserved);

        [DllImport("iphlpapi.dll")]
        extern static int GetTcpStatistics(ref MIB_TCPSTATS pStats);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        extern static int AllocateAndGetTcpExTableFromStack(ref IntPtr pTable, bool bOrder, IntPtr heap, int zero, int flags);

        public static void GetTcp()
        {
            MIB_TCPTABLE_OWNER_PID tcpTable = GetTcpTable();

            Console.WriteLine("TCP Table:");
            Console.WriteLine($"Number of entries: {tcpTable.dwNumEntries}");

            foreach (MIB_TCPROW_OWNER_PID tcpRow in tcpTable.table)
            {
                Console.WriteLine($"State: {GetState((int)tcpRow.dwState)}");
                Console.WriteLine($"Local Address: {ConvertIpAddress((int)tcpRow.dwLocalAddr)}:{tcpRow.dwLocalPort}");
                Console.WriteLine($"Remote Address: {ConvertIpAddress((int)tcpRow.dwRemoteAddr)}:{tcpRow.dwRemotePort}");
                Console.WriteLine($"Process ID: {tcpRow.dwOwningPid}");
                Console.WriteLine();
            }
        }

        public static MIB_TCPTABLE_OWNER_PID GetTcpTable()
        {
            MIB_TCPTABLE_OWNER_PID tcpTable = new MIB_TCPTABLE_OWNER_PID();
            int buffSize = 0;
            int ret = GetExtendedTcpTable(IntPtr.Zero, ref buffSize, true, 2, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0);
            IntPtr buffTable = Marshal.AllocHGlobal(buffSize);

            try
            {
                ret = GetExtendedTcpTable(buffTable, ref buffSize, true, 2, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0);
                if (ret != NO_ERROR)
                {
                    return tcpTable;
                }

                tcpTable.dwNumEntries = (uint)Marshal.ReadIntPtr(buffTable);
                IntPtr buffTablePointer = (IntPtr)((long)buffTable + Marshal.SizeOf(typeof(uint)));
                MIB_TCPROW_OWNER_PID[] tcpRows = new MIB_TCPROW_OWNER_PID[tcpTable.dwNumEntries];

                for (int i = 0; i < tcpTable.dwNumEntries; i++)
                {
                    tcpRows[i] = (MIB_TCPROW_OWNER_PID)Marshal.PtrToStructure(buffTablePointer, typeof(MIB_TCPROW_OWNER_PID));
                    buffTablePointer += Marshal.SizeOf(tcpRows[i]);
                }

                tcpTable.table = tcpRows;
            }
            finally
            {
                Marshal.FreeHGlobal(buffTable);
            }

            return tcpTable;
        }
    }
}
