using NUnit.Framework;
using System.Text.RegularExpressions;

public class StaticTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestGetEnumDescription()
    {
        // Use the Assert class to test conditions
        Assert.AreEqual(EnumStatic.GetEnumDescription(LocationEnum.Artefact), "Artefact");
        Assert.AreEqual(EnumStatic.GetEnumDescription(Round.Survior), "Phase 1");
    }

    // A Test behaves as an ordinary method
    [Test]
    public void TestNext()
    {
        // Use the Assert class to test conditions
        Assert.AreEqual(EnumStatic.Next(Round.Reset), Round.Survior);
        Assert.AreEqual(EnumStatic.Next(Round.Survior), Round.Monster);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void TestGet_IPv6_Local_IP()
    {
        // Use the Assert class to test conditions
        if (string.IsNullOrEmpty(IpStatic.Get_IPv6_Local_IP()))
        {
            return;
        }
        //Check if method return a correct IPv6 string, with IPv6 regex
        Assert.IsTrue(Regex.IsMatch(IpStatic.Get_IPv6_Local_IP(), @"(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))"));
    }
}
