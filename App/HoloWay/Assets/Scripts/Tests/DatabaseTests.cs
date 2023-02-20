using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DatabaseTests
{
    DatabaseManager dbManager = new DatabaseManager();

    [Test]
    public void UserIDMatches()
    {
        string myUserID = "A1644ECE-79F3-514A-9709-8AF5C9F3B3DA";
        string userID = SystemInfo.deviceUniqueIdentifier;

        Assert.AreEqual(myUserID, userID);
    }

    [Test]
    public void CheckCreateUserJson()
    {
        string Name = "hamza";
        string Gold = "101";
        string jsonString = "{\"name\":\"" + Name + "\",\"gold\":" + Gold + "}";

        User newUser = new User(Name, int.Parse(Gold));
        string json = JsonUtility.ToJson(newUser);

        Assert.AreEqual(json, jsonString);
    }

    [Test]
    public void CheckValuesReturned()
    {
        dbManager.GetUserInfo();
        
        string Name = "hamza";
        string Gold = "101";

        Debug.Log(dbManager.GetReturnedName());

        string nameReturned = dbManager.GetReturnedName();
        string goldReturned = dbManager.GetReturnedGold();


        Assert.AreEqual(Name, nameReturned);
        Assert.AreEqual(Gold, goldReturned);
    }
}
