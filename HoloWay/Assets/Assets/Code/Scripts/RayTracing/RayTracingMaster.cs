using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// http://blog.three-eyed-games.com/2018/05/03/gpu-ray-tracing-in-unity-part-1/
// http://three-eyed-games.com/2018/05/12/gpu-path-tracing-in-unity-part-2/
// http://three-eyed-games.com/2019/03/18/gpu-path-tracing-in-unity-part-3/

    public class RayTracingMaster : MonoBehaviour
    {
        public ComputeShader RayTracingShader;
        private RenderTexture _target;
        private Camera _camera;
        public Texture SkyboxTexture;
        private uint _currentSample = 0;
        private Material _addMaterial;
        public Light DirectionalLight;
        public Vector2 SphereRadius = new Vector2(3.0f, 8.0f);
        public uint SpheresMax = 100;
        public float SpherePlacementRadius = 100.0f;
        private ComputeBuffer _sphereBuffer;
        private RenderTexture _converged;
        public int SphereSeed;


        struct Sphere
        {
            public Vector3 position;
            public float radius;
            public Vector3 albedo;
            public Vector3 specular;
            public float smoothness;
            public Vector3 emission;
        }

        private static bool _meshObjectsNeedRebuilding = false;
        private static List<RayTracingObject> _rayTracingObjects = new List<RayTracingObject>();
        public static void RegisterObject(RayTracingObject obj)
        {
            _rayTracingObjects.Add(obj);
            _meshObjectsNeedRebuilding = true;
        }
        public static void UnregisterObject(RayTracingObject obj)
        {
            _rayTracingObjects.Remove(obj);
            _meshObjectsNeedRebuilding = true;
        }
        struct MeshObject
        {
            public Matrix4x4 localToWorldMatrix;
            public int indices_offset;
            public int indices_count;
            public Vector3 albedo;
            public Vector3 specular;
            public float smoothness;
            public Vector3 emission;
        }

        private static List<MeshObject> _meshObjects = new List<MeshObject>();
        private static List<Vector3> _vertices = new List<Vector3>();
        private static List<int> _indices = new List<int>();

        private ComputeBuffer _meshObjectBuffer;
        private ComputeBuffer _vertexBuffer;
        private ComputeBuffer _indexBuffer;

        private static void CreateComputeBuffer<T>(ref ComputeBuffer buffer, List<T> data, int stride)
            where T : struct
        {
            // Do we already have a compute buffer?
            if (buffer != null)
            {
                // If no data or buffer doesn't match the given criteria, release it
                if (data.Count == 0 || buffer.count != data.Count || buffer.stride != stride)
                {
                    buffer.Release();
                    buffer = null;
                }
            }
            if (data.Count != 0)
            {
                // If the buffer has been released or wasn't there to
                // begin with, create it
                if (buffer == null)
                {
                    buffer = new ComputeBuffer(data.Count, stride);
                }
                // Set data on the buffer
                buffer.SetData(data);
            }
        }
        private void SetComputeBuffer(string name, ComputeBuffer buffer)
        {
            if (buffer != null)
            {
                RayTracingShader.SetBuffer(0, name, buffer);
            }
        }

        private void RebuildMeshObjectBuffers()
        {
            if (!_meshObjectsNeedRebuilding)
            {
                return;
            }
            _meshObjectsNeedRebuilding = false;
            _currentSample = 0;
            // Clear all lists
            _meshObjects.Clear();
            _vertices.Clear();
            _indices.Clear();
            // Loop over all objects and gather their data
            foreach (RayTracingObject obj in _rayTracingObjects)
            {
                Mesh mesh = obj.GetComponent<MeshFilter>().sharedMesh;
                Material material = obj.GetComponent<Renderer>().material;
                Debug.Log(new Vector3(material.GetColor("_Color").r,material.GetColor("_Color").g,material.GetColor("_Color").b));
                // Add vertex data
                int firstVertex = _vertices.Count;
                _vertices.AddRange(mesh.vertices);
                // Add index data - if the vertex buffer wasn't empty before, the
                // indices need to be offset
                int firstIndex = _indices.Count;
                var indices = mesh.GetIndices(0);
                _indices.AddRange(indices.Select(index => index + firstVertex));
                //float H, S, V;
                //Color.RGBToHSV(material.color, out H, out S, out V);
                //Color color = new Color (H, S, V);
                //bool metal = material.GetFloat("_Metallic") < 0.5f;
                Color color = Random.ColorHSV();
                bool metal = Random.value < 0.5f;
                
                // Add the object itself
                //Vector3 _albedo = metal ? Vector3.zero : new Vector3(color.r, color.g, color.b);
                //Vector3 _specular = metal ? new Vector3(color.r, color.g, color.b) : Vector3.one * 0.04f;
                //float _smoothness = material.GetFloat("_Glossiness");
                //Vector3 _emission = new Vector3(color.r, color.g, color.b);
                Vector3 _albedo = metal ? Vector3.zero : new Vector3(color.r, color.g, color.b);
                Vector3 _specular  = metal ? new Vector3(color.r, color.g, color.b) : Vector3.one * 0.04f;
                float _smoothness = Random.Range(0.0f,5.0f);
                Vector3 _emission = new Vector3(color.r, color.g, color.b) * Random.Range(0.0f,5.0f);
                _meshObjects.Add(new MeshObject()
                {
                    localToWorldMatrix = obj.transform.localToWorldMatrix,
                    indices_offset = firstIndex,
                    indices_count = indices.Length,
                    albedo = _albedo,
                    specular = _specular,
                    smoothness = _smoothness,
                    emission = _emission
                });
            }
            CreateComputeBuffer(ref _meshObjectBuffer, _meshObjects, 112);
            CreateComputeBuffer(ref _vertexBuffer, _vertices, 12);
            CreateComputeBuffer(ref _indexBuffer, _indices, 4);
        }
        private void SetUpScene()
        {
            Random.InitState(SphereSeed);
            List<Sphere> spheres = new List<Sphere>();
            
            // Add a number of random spheres
            for (int i = 0; i < SpheresMax; i++)
            {
                Sphere sphere = new Sphere();
                // Radius and radius
                sphere.radius = SphereRadius.x + Random.value * (SphereRadius.y - SphereRadius.x);
                Vector2 randomPos = Random.insideUnitCircle * SpherePlacementRadius;
                sphere.position = new Vector3(randomPos.x, sphere.radius, randomPos.y);
                // Reject spheres that are intersecting others
                foreach (Sphere other in spheres)
                {
                    float minDist = sphere.radius + other.radius;
                    if (Vector3.SqrMagnitude(sphere.position - other.position) < minDist * minDist)
                        goto SkipSphere;
                }
                // Albedo and specular color
                Color color = Random.ColorHSV();
                bool metal = Random.value < 0.5f;
                sphere.albedo = metal ? Vector3.zero : new Vector3(color.r, color.g, color.b);
                sphere.specular = metal ? new Vector3(color.r, color.g, color.b) : Vector3.one * 0.04f;
                sphere.smoothness = Random.Range(0.0f,5.0f);
                sphere.emission = new Vector3(color.r, color.g, color.b) * Random.Range(0.0f,5.0f);
                // Add the sphere to the list
                spheres.Add(sphere);
            SkipSphere:
                continue;
            }
            // Assign to compute buffer
            _sphereBuffer = new ComputeBuffer(spheres.Count, 56);
            _sphereBuffer.SetData(spheres);
        }
        private void OnEnable()
        {
            _currentSample = 0;
            SetUpScene();
        }
        private void OnDisable()
        {
            if (_sphereBuffer != null)
                _sphereBuffer.Release();
            if (_meshObjectBuffer != null)
                _meshObjectBuffer.Release();
                if (_vertexBuffer != null)
                _vertexBuffer.Release();
                if (_indexBuffer != null)
                _indexBuffer.Release();
        }
        
        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }
        private void Update()
        {
            if (transform.hasChanged)
            {
                _currentSample = 0;
                transform.hasChanged = false;
            }
        }
        private void SetShaderParameters()
        {
            RayTracingShader.SetMatrix("_CameraToWorld", _camera.cameraToWorldMatrix);
            RayTracingShader.SetMatrix("_CameraInverseProjection", _camera.projectionMatrix.inverse);
            RayTracingShader.SetTexture(0, "_SkyboxTexture", SkyboxTexture);
            RayTracingShader.SetVector("_PixelOffset", new Vector2(Random.value, Random.value));
            Vector3 l = DirectionalLight.transform.forward;
            RayTracingShader.SetVector("_DirectionalLight", new Vector4(l.x, l.y, l.z, DirectionalLight.intensity));
            RayTracingShader.SetBuffer(0, "_Spheres", _sphereBuffer);
            RayTracingShader.SetFloat("_Seed", Random.value);
            SetComputeBuffer("_Spheres", _sphereBuffer);
            SetComputeBuffer("_MeshObjects", _meshObjectBuffer);
            SetComputeBuffer("_Vertices", _vertexBuffer);
            SetComputeBuffer("_Indices", _indexBuffer);
        }


        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            RebuildMeshObjectBuffers();
            SetShaderParameters();
            Render(destination);
        }
        private void InitRenderTexture()
        {
            if (_target == null || _target.width != Screen.width || _target.height != Screen.height)
            {
                _currentSample = 0;
                // Release render texture if we already have one
                if (_target != null)
                    _target.Release();
                // Get a render target for Ray Tracing
                _target = new RenderTexture(Screen.width, Screen.height, 0,
                    RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
                _target.enableRandomWrite = true;
                _target.Create();
            }
        }
        private void Render(RenderTexture destination)
        {
            
            // Make sure we have a current render target
            InitRenderTexture();
            // Set the target and dispatch the compute shader
            RayTracingShader.SetTexture(0, "Result", _target);
            int threadGroupsX = Mathf.CeilToInt(Screen.width / 8.0f);
            int threadGroupsY = Mathf.CeilToInt(Screen.height / 8.0f);
            RayTracingShader.Dispatch(0, threadGroupsX, threadGroupsY, 1);
            // Blit the result texture to the screen
            if (_addMaterial == null)
                _addMaterial = new Material(Shader.Find("Hidden/AddShader"));
            _addMaterial.SetFloat("_Sample", _currentSample);
            Graphics.Blit(_target, _converged, _addMaterial);
            Graphics.Blit(_converged, destination);
            _currentSample++;
        }
        
    }

    