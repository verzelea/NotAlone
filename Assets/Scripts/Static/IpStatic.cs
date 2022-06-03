using System;
using System.Net;
using UnityEngine;

public static class IpStatic
{
    public static string get_IPv6_Local_IP()
    {
        try
        {
            string strHostName = System.Net.Dns.GetHostName(); ;
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            if (addr[0].AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                Console.WriteLine("Local Ipv6 IP Address: " + addr[0].ToString()); //ipv6
                return addr[0].ToString();
            }
        }
        catch (Exception) 
        {
            return null;
        }
        return null;
    }

    public static void CopyToClipboard(this string str)
    {
        GUIUtility.systemCopyBuffer = str;
    }
}
