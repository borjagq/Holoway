using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class LoginResponse { 
    public int RESPONSE_CODE { get; set; }
    public RESPONSE RESPONSE { get; set; }
}


public class RESPONSE
{
    public string RESPONSE_DATA { get; set; }
}