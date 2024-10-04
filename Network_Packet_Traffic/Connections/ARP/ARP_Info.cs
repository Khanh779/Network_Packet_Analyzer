using Network_Packet_Traffic.Connections.Enums;
using Network_Packet_Traffic.Connections.IPNET;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static Network_Packet_Traffic.Connections.NetHelper;

namespace Network_Packet_Traffic.Connections.ARP
{
    public class ARP_Info
    {

        [DllImport("iphlpapi.dll", SetLastError = true)]
        static extern int SendARP(int DestIP, int SrcIP, byte[] pMacAddr, ref uint PhyAddrLen);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        static extern int GetIpNetTable(IntPtr pIpNetTable, ref int pdwSize, bool bOrder);
        [DllImport("iphlpapi.dll")]
        extern static int GetIpStatistics(ref MIB_IPSTATS pStats);

        const int AF_INET = 2;    // IP

        public static string GetMacAddress(string ipAddress)
        {
            string macAddress = string.Empty;
            try
            {
                int remoteMachine = BitConverter.ToInt32(System.Net.IPAddress.Parse(ipAddress).GetAddressBytes(), 0);
                byte[] macAddr = new byte[6];
                uint macAddrLen = (uint)macAddr.Length;
                if (SendARP(remoteMachine, 0, macAddr, ref macAddrLen) == 0)
                {
                    string[] str = new string[(int)macAddrLen];
                    for (int i = 0; i < macAddrLen; i++)
                        str[i] = macAddr[i].ToString("x2");
                    macAddress = string.Join(":", str);
                }
            }
            catch (Exception ex)
            {
                macAddress = "Error: " + ex.Message;
            }
            return macAddress;
        }

        MIB_IPSTATS iB_IPSTATS;
        MIB_IPNETTABLE mIB_IPNETTABLE;

        public ARP_Info()
        {
            iB_IPSTATS = GetIpStas();
            mIB_IPNETTABLE = GetArpTable();
        }

        public ARP.MIB_ARPSTATS GetARP()
        {

            ARP.MIB_ARPSTATS a = new ARP.MIB_ARPSTATS();
            a.DefaultTTL = iB_IPSTATS.dwDefaultTTL;
            a.ForwardedDatagrams = iB_IPSTATS.dwForwDatagrams;
            a.Forwarding = (ForwardingStatus)iB_IPSTATS.dwForwarding;
            a.FragmentCreates = iB_IPSTATS.dwFragCreates;
            a.FragmentFails = iB_IPSTATS.dwFragFails;
            a.FragmentOks = iB_IPSTATS.dwFragOks;
            a.InAddressErrors = iB_IPSTATS.dwInAddrErrors;
            a.InDelivers = iB_IPSTATS.dwInDelivers;
            a.InDiscards = iB_IPSTATS.dwInDiscards;
            a.InHeaderErrors = iB_IPSTATS.dwInHdrErrors;
            a.InReceives = iB_IPSTATS.dwInReceives;
            a.InUnknownProtocols = iB_IPSTATS.dwInUnknownProtos;
            a.NumberOfAddresses = iB_IPSTATS.dwNumAddr;
            a.NumberOfInterfaces = iB_IPSTATS.dwNumIf;
            a.NumberOfRoutes = iB_IPSTATS.dwNumRoutes;
            a.OutDiscards = iB_IPSTATS.dwOutDiscards;
            a.OutNoRoutes = iB_IPSTATS.dwOutNoRoutes;
            a.OutRequests = (RequestsStatus)iB_IPSTATS.dwOutRequests;
            a.ReassemblyFails = iB_IPSTATS.dwReasmFails;
            a.ReassemblyOks = iB_IPSTATS.dwReasmOks;
            a.ReassemblyRequests = (RequestsStatus)iB_IPSTATS.dwReasmReqds;
            a.ReassemblyTimeout = iB_IPSTATS.dwReasmTimeout;
            a.RoutingDiscards = iB_IPSTATS.dwRoutingDiscards;
            return a;

        }

        public static ARP.MIB_ARPTABLE GetARPTable()
        {
            // Khởi tạo bảng ARP
            ARP.MIB_ARPTABLE arpTable = new ARP.MIB_ARPTABLE();

            // Lấy bảng IP Net
            MIB_IPNETTABLE ipNetTable = IPNET.IPNET_Info.GetIpNetable();

            // Thiết lập số lượng mục trong bảng ARP
            arpTable.NumberOfEntries = ipNetTable.dwNumEntries;
            arpTable.ARPConnections = new ARP.MIB_ARPROW[arpTable.NumberOfEntries];

            // Lặp qua từng dòng trong bảng ARP
            for (int i = 0; i < arpTable.NumberOfEntries; i++)
            {
                arpTable.ARPConnections[i] = new ARP.MIB_ARPROW();

                // Gán thông tin cho từng hàng trong bảng ARP
                arpTable.ARPConnections[i].Index = ipNetTable.table[i].dwIndex;
                arpTable.ARPConnections[i].PhysicalAddressLength = ipNetTable.table[i].dwPhysAddrLen;

                // Gán địa chỉ vật lý
                arpTable.ARPConnections[i].PhysicalAddress = new byte[ipNetTable.table[i].dwPhysAddrLen];
                Array.Copy(ipNetTable.table[i].bPhysAddr, arpTable.ARPConnections[i].PhysicalAddress, ipNetTable.table[i].dwPhysAddrLen);

                // Chuyển đổi địa chỉ IP
                arpTable.ARPConnections[i].Address = ConvertIpAddress(ipNetTable.table[i].dwAddr);

                // Gán loại
                arpTable.ARPConnections[i].Type = (MIB_IPNET_TYPE)ipNetTable.table[i].dwType;
            }

            return arpTable;
        }




        MIB_IPSTATS GetIpStas()
        {
            MIB_IPSTATS ipStats = new MIB_IPSTATS();

            int result = GetIpStatistics(ref ipStats);

            if (result != NO_ERROR)
            {
                // Handle the error if needed
                Console.WriteLine("Error getting IP statistics. Error code: " + result);
            }

            return ipStats;
        }

        MIB_IPNETTABLE GetArpTable()
        {
            MIB_IPNETTABLE arpTable = new MIB_IPNETTABLE();

            int buffSize = 0;
            //int result = GetIpNetTable(IntPtr.Zero, ref buffSize, false);
            int result = GetIpNetTable(IntPtr.Zero, ref buffSize, false);
            IntPtr buffTable = Marshal.AllocHGlobal(buffSize);

            try
            {
                //result = GetIpNetTable(buffTable, ref buffSize, false);

                if (result != NO_ERROR)
                {
                    Console.WriteLine("Error getting ARP table. Error code: " + result);
                    return arpTable;
                }

                arpTable = (MIB_IPNETTABLE)Marshal.PtrToStructure(buffTable, typeof(MIB_IPNETTABLE));
                arpTable.table = new MIB_IPNETROW[arpTable.dwNumEntries];

                IntPtr buffTablePointer = buffTable + Marshal.SizeOf(arpTable.dwNumEntries);

                for (int i = 0; i < arpTable.dwNumEntries; i++)
                {
                    arpTable.table[i] = (MIB_IPNETROW)Marshal.PtrToStructure(buffTablePointer, typeof(MIB_IPNETROW));
                    buffTablePointer += Marshal.SizeOf(arpTable.table[i]);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buffTable);
            }

            return arpTable;
        }

        public List<ArpEntry> GetArpEntries()
        {
            List<ArpEntry> arpEntries = new List<ArpEntry>();
            IntPtr pTable = IntPtr.Zero;
            int bufferSize = 0;

            // Lần đầu tiên gọi hàm GetIpNetTable để lấy kích thước của bảng
            int result = GetIpNetTable(IntPtr.Zero, ref bufferSize, false);
            pTable = Marshal.AllocCoTaskMem(bufferSize);

            try
            {
                result = GetIpNetTable(pTable, ref bufferSize, false);
                if (result != 0)
                {
                    throw new System.ComponentModel.Win32Exception(result);
                }


                int numEntries = Marshal.ReadInt32(pTable);
                IntPtr rowPtr = new IntPtr(pTable.ToInt64() + 4); // 4 bytes là kích thước của biến numEntries

                for (int i = 0; i < numEntries; i++)
                {
                    var row = (MIB_IPNETROW)Marshal.PtrToStructure(rowPtr, typeof(MIB_IPNETROW));
                    arpEntries.Add(new ArpEntry
                    {
                        IpAddress = new System.Net.IPAddress(BitConverter.GetBytes(row.dwAddr)).ToString(),
                        MacAddress = string.Format("{0:x2}-{1:x2}-{2:x2}-{3:x2}-{4:x2}-{5:x2}",
                            row.bPhysAddr[0], row.bPhysAddr[1], row.bPhysAddr[2], row.bPhysAddr[3], row.bPhysAddr[4], row.bPhysAddr[5])
                    });
                    rowPtr = new IntPtr(rowPtr.ToInt64() + Marshal.SizeOf(typeof(MIB_IPNETROW)));


                    //MIB_IPNETROW arpEntry = (MIB_IPNETROW)Marshal.PtrToStructure(rowPtr, typeof(MIB_IPNETROW));
                    //ArpEntry entry = new ArpEntry();
                    //entry.IpAddress = new System.Net.IPAddress(BitConverter.GetBytes(arpEntry.dwAddr)).ToString();
                    //entry.MacAddress = string.Format("{0:x2}-{1:x2}-{2:x2}-{3:x2}-{4:x2}-{5:x2}",
                    //    arpEntry.mac0, arpEntry.mac1, arpEntry.mac2, arpEntry.mac3, arpEntry.mac4, arpEntry.mac5);
                    //arpEntries.Add(entry);
                    //rowPtr = new IntPtr(rowPtr.ToInt64() + Marshal.SizeOf(typeof(MIB_IPNETROW)));
                }

                //MIB_IPNETROW[] arpEntriesArray = new MIB_IPNETROW[bufferSize / Marshal.SizeOf(typeof(MIB_IPNETROW))];
                //IntPtr p = pTable;
                //for (int i = 0; i < arpEntriesArray.Length; i++)
                //{
                //    arpEntriesArray[i] = (MIB_IPNETROW)Marshal.PtrToStructure(p, typeof(MIB_IPNETROW));
                //    p += Marshal.SizeOf(typeof(MIB_IPNETROW));
                //}

                //foreach (MIB_IPNETROW arpEntry in arpEntriesArray)
                //{
                //    ArpEntry entry = new ArpEntry();
                //    entry.IpAddress = new System.Net.IPAddress(BitConverter.GetBytes(arpEntry.dwAddr)).ToString();
                //    entry.MacAddress = string.Format("{0:x2}-{1:x2}-{2:x2}-{3:x2}-{4:x2}-{5:x2}",
                //        arpEntry.mac0, arpEntry.mac1, arpEntry.mac2, arpEntry.mac3, arpEntry.mac4, arpEntry.mac5);
                //    arpEntries.Add(entry);
                //}
            }
            catch
            {

            }
            finally
            {
                if (pTable != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(pTable);
                }
            }

            return arpEntries;
        }
    }
}
