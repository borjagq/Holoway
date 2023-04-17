using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    
        private Camera cam;
        
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindWithTag("Whiteboard").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnMouseDrag()
     {
         float distance_to_screen = cam.WorldToScreenPoint(gameObject.transform.position).z;
         transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen ));
      
     }
}
