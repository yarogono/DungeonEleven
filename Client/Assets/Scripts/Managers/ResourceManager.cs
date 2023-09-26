using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class ResourceManager : MonoBehaviour
    {
        private static ResourceManager _instance;
        public static ResourceManager GetInstance { get { Init(); return _instance; } }

        void Start()
        {
            Init();
        }

        static void Init()
        {
            if (_instance == null)
            {
                _instance = (ResourceManager)FindObjectOfType(typeof(ResourceManager));

                if (_instance == null)
                {
                    ResourceManager gameObject = new ResourceManager { name = "@ResourceManager" };
                    _instance = gameObject.AddComponent<ResourceManager>();
                    DontDestroyOnLoad(gameObject);
                }
            }
        }

        public T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }

        public GameObject Instantiate(string path, Transform parent = null)
        {
            GameObject original = Load<GameObject>($"Prefabs/{path}");
            if (original == null)
            {
                Debug.Log($"Failed to load prefab : {path}");
                return null;
            }

            GameObject go = Object.Instantiate(original, parent);
            go.name = original.name;
            return go;
        }

        public void Destroy(GameObject go)
        {
            if (go == null)
                return;

            Object.Destroy(go);
        }
    }
}
