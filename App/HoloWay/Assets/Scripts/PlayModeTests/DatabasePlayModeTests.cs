using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class DatabasePlayModeTests : MonoBehaviour
{
    DatabaseManager dbManager = new DatabaseManager();

    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Before");
        dbManager.GetUserInfo();
        Debug.Log();
    }

    [UnityTest]
    public IEnumerator UserIDMatches()
    {
        string myUserID = "A1644ECE-79F3-514A-9709-8AF5C9F3B3DA";
        string userID = SystemInfo.deviceUniqueIdentifier;

        Assert.AreEqual(myUserID, userID);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckCreateUserJson()
    {
        string Name = "hamza";
        string Gold = "101";
        string jsonString = "{\"name\":\"" + Name + "\",\"gold\":" + Gold + "}";

        User newUser = new User(Name, int.Parse(Gold));
        string json = JsonUtility.ToJson(newUser);

        Assert.AreEqual(json, jsonString);
        yield return null;
    }

    [Test]
    public IEnumerator CheckValuesReturned()
    {
        
        string Name = "hamza";
        string Gold = "101";
        //dbManager.

        Debug.Log(dbManager.GetReturnedName() + " - Tests");

        

        //dbManager.GetReturnedGold();

        //string nameReturned = ;
        //string goldReturned = ;


        //Assert.AreEqual(Name, nameReturned);
        //Assert.AreEqual(Gold, goldReturned);
        yield return null;
    }
}
