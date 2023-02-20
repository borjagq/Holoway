using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Database;
using System;


public class DatabaseManager : MonoBehaviour
{
    public TMP_InputField Name;
    public TMP_InputField Gold;

    public TMP_Text NameText;
    public TMP_Text GoldText;

    private string userID { get; set; }
    private DatabaseReference dbReference { get; set; }

    public string valNameReturned;
    public string valGoldReturned;

    public DatabaseManager()
    {

    }

    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        GetUserInfo();
    }

    public void CreateUser()
    {
        User newUser = new User(Name.text, int.Parse(Gold.text));
        string json = JsonUtility.ToJson(newUser);
        Debug.Log(json);
        dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);

        //CreateUserButton = !CreateUserButton;
    }

    
    public IEnumerator GetName(Action<string> onCallback)
    {
        var userNameData = dbReference.Child("users").Child(userID).Child("name").GetValueAsync();

        yield return new WaitUntil(predicate:()=>userNameData.IsCompleted);

        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;

            onCallback.Invoke(snapshot.Value.ToString());

            valNameReturned = snapshot.Value.ToString();
            //Debug.Log(valNameReturned);
        }
    }

    public IEnumerator GetGold(Action<int> onCallback)
    {
        var userGoldData = dbReference.Child("users").Child(userID).Child("gold").GetValueAsync();

        yield return new WaitUntil(predicate: () => userGoldData.IsCompleted);

        if (userGoldData != null)
        {
            DataSnapshot snapshot = userGoldData.Result;

            onCallback.Invoke(int.Parse(snapshot.Value.ToString()));

            valGoldReturned = snapshot.Value.ToString();
            //Debug.Log(int.Parse(valGoldReturned));
        }
    }

    public void GetUserInfo()
    {
        StartCoroutine(GetName((string name) => {
            NameText.text = name;
        }));
        StartCoroutine(GetGold((int  gold) => {
            GoldText.text = gold.ToString();
        }));
    }


    public string GetReturnedName()
    {
        GetUserInfo();
        return valNameReturned;
    }

    public string GetReturnedGold()
    {
        GetUserInfo();
        return valGoldReturned;
    }

}
