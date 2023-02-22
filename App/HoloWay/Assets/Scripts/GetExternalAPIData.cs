using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetExternalAPIData : MonoBehaviour
{
    public TMP_Text externalAPIDataText;
    
    public void GetExternalData()
    {
        var externalAPIData = APIHelper.getNewExternalAPIData();
        externalAPIDataText.text = externalAPIData.value;
        //externalAPIDataText.text = "Hello World";
    }
}
