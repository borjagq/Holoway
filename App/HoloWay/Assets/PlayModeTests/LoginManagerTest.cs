using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginManagerTest : MonoBehaviour
{
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene(12);
    }

    [UnityTest]
    public IEnumerator MoveToScene()
    {
        yield return null; //Skip a frame
        //GameObject Canvas = GameObject.Find("UICanvas");
        //LoginManager script = Canvas.GetComponent<LoginManager>();
        //script.MoveToScene();
        yield return new WaitForSeconds(3.5f);
        Assert.AreEqual(1,SceneManager.GetActiveScene().buildIndex);
    }
}
