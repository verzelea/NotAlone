using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTest
{
    // A UnityTest behaves like a coroutine in Play Mode.
    [UnityTest]
    public IEnumerator Test()
    {
        yield return null;
    }

    // A UnityTest behaves like a coroutine in Play Mode.
    /*[UnityTest]
    public IEnumerator TestGetSet()
    {
        var gameObject = new GameObject();
        var player = gameObject.AddComponent<Player>();

        player.SetLocation(LocationEnum.Antre);
        player.SetIsReady(true);
        player.SetPlayer("Test");

        yield return new WaitForSeconds(1);

        Assert.AreEqual(player.GetLocation(), LocationEnum.Antre);
        Assert.AreEqual(player.GetIsReady(), true);
        Assert.AreEqual(player.GetPlayer(), "Test");
    }*/
}
