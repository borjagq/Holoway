using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastRay : MonoBehaviour
{
    
    static public bool hasHit(string TargetName, Camera cam) {
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {   
            if(hit.collider.name == TargetName) {
                return true;
            } else {
                return false;
            }
        }else {
            return false;
        }
    }
}
