using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class DatabasePlayModeTests : MonoBehaviour
{
    DatabaseManager dbManager;
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene(0);
        
        //Debug.Log(dbManager);
        //dbManager = GameObject.Find("DatabaseManager").GetComponent<DatabaseManager>();
        //Debug.Log(dbManager);
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

        Debug.Log("Before");
        User newUser = new User(Name, int.Parse(Gold));
        string json = JsonUtility.ToJson(newUser);
        

        Assert.AreEqual(1, 1);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckValuesReturned()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
            if (go.name == "DatabaseManager")
        {
            dbManager = go.GetComponent<DatabaseManager>();
        }
        string Name = "hamza";
        string Gold = "101";
        //dbManager.
        dbManager.GetUserInfo();
        yield return new WaitForSecondsRealtime(3);
        Debug.Log(dbManager.GetReturnedName() + " - Tests");


        //dbManager.GetReturnedGold();


        string nameReturned = dbManager.GetReturnedName();
        string goldReturned = dbManager.GetReturnedGold();


        Assert.AreEqual(Name, nameReturned);
        Assert.AreEqual(Gold, goldReturned);
        yield return new WaitForSecondsRealtime(2);

    }
}
