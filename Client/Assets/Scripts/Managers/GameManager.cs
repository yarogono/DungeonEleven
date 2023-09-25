using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager s_instance;
    static GameManager Instance { get { Init(); return s_instance; } }

    #region Managers

    #endregion

    void Start()
    {
        Init();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject gameObject = GameObject.Find("@GameManager");
            if (gameObject == null)
            {
                gameObject = new GameObject { name = "@GameManager" };
                gameObject.AddComponent<GameManager>();
            }

            DontDestroyOnLoad(gameObject);
            s_instance = gameObject.GetComponent<GameManager>();

        }
    }

    public static void Clear()
    {

    }
}
