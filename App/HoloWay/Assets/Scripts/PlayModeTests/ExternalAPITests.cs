using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

public class ExternalAPITests
{
    [UnityTest]
    public IEnumerator RetrieveCorrectURLInformation()
    {

        var myPictureURL = "https://assets.chucknorris.host/img/avatar/chuck-norris.png";
        var externalAPIData = APIHelper.getNewExternalAPIData();
        var pictureURL = externalAPIData.icon_url;
        Assert.AreEqual(myPictureURL, pictureURL);
        yield return null;
    }

    [UnityTest]
    public IEnumerator RetrieveExternalAPIObject()
    {
        var externalAPIData = APIHelper.getNewExternalAPIData();
        Assert.AreNotEqual(externalAPIData, null);
        yield return null;
    }
}
