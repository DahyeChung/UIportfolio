using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _instance;
    static Managers Instance { get { Init(); return _instance; } }

    UI_Manager _ui = new UI_Manager();

    public static UI_Manager UI { get { return Instance?._ui; } }

    public static void Init()
    {
        if (_instance == null)
        {
            GameObject gameObject = GameObject.Find("@Managers");
            if (gameObject == null)
            {
                gameObject = new GameObject { name = "@Managers" };
                gameObject.AddComponent<Managers>();
            }
            DontDestroyOnLoad(gameObject);
            _instance = gameObject.GetComponent<Managers>();
        }
    }
}
