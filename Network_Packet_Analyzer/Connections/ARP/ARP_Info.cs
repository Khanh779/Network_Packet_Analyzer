using Network_Packet_Analyzer.Connections.Enums;
using Network_Packet_Analyzer.Connections.IPNET;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Windows.Forms;
using static Network_Packet_Analyzer.Connections.NetHelper;

namespace Network_Packet_Analyzer.Connections.ARP
{
    public class ARP_Info
    {

        [DllImport("iphlpapi.dll", SetLastError = true)]
        static extern int GetIpNetTable(IntPtr pIpNetTable, ref int dwOutBufLen, bool bOrder);

    

        public ARP_Info()
        {

        }

        //MIB_IPSTATS iB_IPSTATS;
        //MIB_IPNETTABLE mIB_IPNETTABLE;
        //public ARP.MIB_ARPSTATS GetARP()
        //{

        //    ARP.MIB_ARPSTATS a = new ARP.MIB_ARPSTATS();
        //    a.DefaultTTL = iB_IPSTATS.dwDefaultTTL;
        //    a.ForwardedDatagrams = iB_IPSTATS.dwForwDatagrams;
        //    a.Forwarding = (ForwardingStatus)iB_IPSTATS.dwForwarding;
        //    a.FragmentCreates = iB_IPSTATS.dwFragCreates;
        //    a.FragmentFails = iB_IPSTATS.dwFragFails;
        //    a.FragmentOks = iB_IPSTATS.dwFragOks;
        //    a.InAddressErrors = iB_IPSTATS.dwInAddrErrors;
        //    a.InDelivers = iB_IPSTATS.dwInDelivers;
        //    a.InDiscards = iB_IPSTATS.dwInDiscards;
        //    a.InHeaderErrors = iB_IPSTATS.dwInHdrErrors;
        //    a.InReceives = iB_IPSTATS.dwInReceives;
        //    a.InUnknownProtocols = iB_IPSTATS.dwInUnknownProtos;
        //    a.NumberOfAddresses = iB_IPSTATS.dwNumAddr;
        //    a.NumberOfInterfaces = iB_IPSTATS.dwNumIf;
        //    a.NumberOfRoutes = iB_IPSTATS.dwNumRoutes;
        //    a.OutDiscards = iB_IPSTATS.dwOutDiscards;
        //    a.OutNoRoutes = iB_IPSTATS.dwOutNoRoutes;
        //    a.OutRequests = (RequestsStatus)iB_IPSTATS.dwOutRequests;
        //    a.ReassemblyFails = iB_IPSTATS.dwReasmFails;
        //    a.ReassemblyOks = iB_IPSTATS.dwReasmOks;
        //    a.ReassemblyRequests = (RequestsStatus)iB_IPSTATS.dwReasmReqds;
        //    a.ReassemblyTimeout = iB_IPSTATS.dwReasmTimeout;
        //    a.RoutingDiscards = iB_IPSTATS.dwRoutingDiscards;
        //    return a;

        //}

        public static MIB_ARPTABLE GetARPTable()
        {
            MIB_ARPTABLE arpTable = new MIB_ARPTABLE();
            int bufferSize = 0;
            int result = GetIpNetTable(IntPtr.Zero, ref bufferSize, false);
            IntPtr pTable = Marshal.AllocHGlobal(bufferSize);
            try
            {
                result = GetIpNetTable(pTable, ref bufferSize, false);
                if (result != NO_ERROR) throw new Exception("Error getting ARP table");

                arpTable.NumberOfEntries = (uint)Marshal.ReadIntPtr(pTable);
                IntPtr buffTablePointer = pTable + Marshal.SizeOf(typeof(uint));
                // Initialize the array to hold the ARP entries
                arpTable.arpTable = new MIB_ARPROW[arpTable.NumberOfEntries];
                // Loop through the ARP table and read each entry
                for (int i = 0; i < arpTable.NumberOfEntries; i++)
                {
                    // Read each MIB_ARPROW from unmanaged memory
                    arpTable.arpTable[i] = (MIB_ARPROW)Marshal.PtrToStructure(buffTablePointer, typeof(MIB_ARPROW));

                    // Move the pointer to the next entry
                    buffTablePointer += Marshal.SizeOf(typeof(MIB_ARPROW));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}"); // Show error message
            }
            finally
            {
                // Free the allocated memory for the ARP table
                Marshal.FreeHGlobal(pTable);
            }

            return arpTable; // Return the ARP table
        }


    }
}
