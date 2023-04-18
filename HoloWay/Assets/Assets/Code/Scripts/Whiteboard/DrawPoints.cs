using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPoints : MonoBehaviour
{
    [SerializeField]
        private GameObject point;
    [SerializeField]
        private GameObject interpolatedPointObject;
    [SerializeField]
        private GameObject board;
    [SerializeField]
        private GameObject Parent;
    [SerializeField]
        private Camera cam;
        [SerializeField]
        private GameObject camObj;
    [SerializeField]
        private int stepsPerCurve = 100;
    [SerializeField]
        private Vector3 rotation;
    [SerializeField]
        private Vector3 trans_ControlPoints;
    [SerializeField]
        private Vector3 trans_LerpPoints;
    [SerializeField]
        private int display;

    List<GameObject> points = new List<GameObject>();
    List<List<GameObject>> splines = new List<List<GameObject>>();
    List<GameObject> interpolatedPoints = new List<GameObject>();
    GameObject crntPointMoved = null;
    Vector3 crntPointMovedPosition;
    bool Deletion = false;

    // https://andrewhungblog.wordpress.com/2017/03/03/catmull-rom-splines-in-plain-english/
    public List<Vector3> GenerateSpline(List<GameObject> _points, int stepsPerCurve = 3, float tension = 1)
    {
        List<Vector3> result = new List<Vector3>();
 
        for (int i = 0; i < _points.Count - 1; i++)
        {
            Vector3 prev = i == 0 ? _points[i].transform.position : _points[i - 1].transform.position;
            Vector3 currStart = _points[i].transform.position;
            Vector3 currEnd = _points[i + 1].transform.position;
            Vector3 next = i == _points.Count - 2 ? _points[i + 1].transform.position : _points[i + 2].transform.position;
 
            for (int step = 0; step <= stepsPerCurve; step++)
            {
                float t = (float)step / stepsPerCurve;
                float tSquared = t * t;
                float tCubed = tSquared * t;
 
                Vector3 interpolatedPoint =
                    (-.5f * tension * tCubed + tension * tSquared - .5f * tension * t) * prev +
                    (1 + .5f * tSquared * (tension - 6) + .5f * tCubed * (4 - tension)) * currStart +
                    (.5f * tCubed * (tension - 4) + .5f * tension * t - (tension - 3) * tSquared) * currEnd +
                    (-.5f * tension * tSquared + .5f * tension * tCubed) * next;
 
                result.Add(interpolatedPoint);
            }
        }
  
        return result;
    }
    void Start() {
    }

    void Update () {
        if (Input.GetMouseButtonDown(0)) {
            CastRay(false);
        }       
        if (Input.GetMouseButtonDown(1)) {
            Debug.Log("delete click");
            CastRay(true);
            DrawSplines();
        }
        if (crntPointMoved != null && crntPointMovedPosition != crntPointMoved.transform.position) {
            DrawSplines();
            crntPointMovedPosition = crntPointMoved.transform.position;
        }
        if (Deletion) {
            DrawSplines();
            Deletion = false;
        }
        if (Input.GetKey ("escape")) {
            Camera Maincam = GameObject.Find("WindowsCamera").GetComponent<Camera>();
            camObj.SetActive(false);
            Maincam.enabled  = true;
        }
        
    }
  
    void DrawSplines() {
        splines.Add(new List<GameObject>(points));
            foreach (GameObject _point in interpolatedPoints) {
                Destroy(_point);
            }
            interpolatedPoints.Clear();
            foreach (List<GameObject> Controlpoints in splines){
                List<Vector3> spline = GenerateSpline(Controlpoints,stepsPerCurve);
                foreach (Vector3 splinePoints in spline) {
                    GameObject interpolatedPoint = Instantiate(interpolatedPointObject, new Vector3(splinePoints.x,splinePoints.y,splinePoints.z), Quaternion.identity);
                    interpolatedPoint.transform.parent = Parent.transform;
                    interpolatedPoint.transform.Translate(new Vector3(trans_LerpPoints.x,trans_LerpPoints.y,trans_LerpPoints.z));
                    interpolatedPoint.transform.Rotate(rotation.x,rotation.y,rotation.z);
                    interpolatedPoints.Add(interpolatedPoint);
                }
            }
            points.Clear();
    }
     


    void CastRay(bool delete) {
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {   
            if (delete){

                if(hit.collider.name == "ControlPoint(Clone)") {
                    Debug.Log("delete castray");
                    foreach (List<GameObject> _spline in splines) {
                        bool deleteSplinePoints = false;
                        foreach (GameObject _point in _spline) {
                            if(hit.collider.transform.gameObject.GetInstanceID() == _point.GetInstanceID()) {
                                deleteSplinePoints = true;
                                Destroy(_point);
                                _spline.Remove(_point);
                                Deletion = true;
                            }
                            if (deleteSplinePoints) {
                                Destroy(_point);
                                _spline.Remove(_point);
                            }
                        }
                    }

                }

            }else {
                if(hit.collider.name == board.name) {
                GameObject _point = Instantiate (point, new Vector3(hit.point.x, hit.point.y,hit.point.z), Quaternion.identity);
                _point.transform.parent = Parent.transform;
                _point.transform.Translate(new Vector3(trans_ControlPoints.x,trans_ControlPoints.y,trans_ControlPoints.z));
                _point.transform.Rotate(rotation.x,rotation.y,rotation.z);
                points.Add(_point);
                }
                if(hit.collider.name == "ControlPoint(Clone)") {
                    crntPointMoved = hit.transform.gameObject;
                    crntPointMovedPosition = hit.transform.position;
                }
            }

        }
        
    }

}
