using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { Init(); return _instance; } }


    void Start()
    {
        Init();
    }

    static void Init()
    {
        if (_instance == null)
        {
            _instance = (GameManager)FindObjectOfType(typeof(GameManager));

            if (_instance == null)
            {
                GameObject gameObject = new GameObject { name = "@GameManager" };
                _instance = gameObject.AddComponent<GameManager>();
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
