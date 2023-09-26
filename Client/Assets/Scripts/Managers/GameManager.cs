using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager s_instance;
    static GameManager Instance { get { Init(); return s_instance; } }


    void Start()
    {
        Init();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            s_instance = (GameManager)FindObjectOfType(typeof(GameManager));

            if (s_instance == null)
            {
                GameObject gameObject = new GameObject { name = "@GameManager" };
                s_instance = gameObject.AddComponent<GameManager>();
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    public static void Clear()
    {
        MapManager.Instance.LoadNextMap();
    }
}
