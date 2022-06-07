using System;
using System.Net;
using UnityEngine;

public static class IpStatic
{
    public static string Get_IPv6_Local_IP()
    {
        try
        {
            string strHostName = System.Net.Dns.GetHostName(); ;
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            if (addr[0].AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                return addr[0].ToString();//ipv6
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
