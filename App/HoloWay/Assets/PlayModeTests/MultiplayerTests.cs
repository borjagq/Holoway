using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

public class MultiplayerTests
{ 
    // Start is called before the first frame update
    //[SetUp]
    //void Start()
    //{
    //SceneManager.LoadScene(7);
        
    //}
    
    //
    [UnityTest]
    public IEnumerator Test_ConnectionForOnePlayer()
    {
        yield return null;
        var num_of_players_connected = 1;
        var connectPlayer = new ConnectPlayer();
        ConnectPlayer.connect();
        Assert.AreEqual(num_of_players_connected, ConnectPlayer.num_of_players_connected);
    }


}
