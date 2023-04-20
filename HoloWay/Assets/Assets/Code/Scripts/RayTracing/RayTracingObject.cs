    using UnityEngine;
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]

    // http://blog.three-eyed-games.com/2018/05/03/gpu-ray-tracing-in-unity-part-1/
    // http://three-eyed-games.com/2018/05/12/gpu-path-tracing-in-unity-part-2/
    // http://three-eyed-games.com/2019/03/18/gpu-path-tracing-in-unity-part-3/
    
    public class RayTracingObject : MonoBehaviour
    {
        private void OnEnable()
        {
            RayTracingMaster.RegisterObject(this);
        }
        private void OnDisable()
        {
            RayTracingMaster.UnregisterObject(this);
        }
    }