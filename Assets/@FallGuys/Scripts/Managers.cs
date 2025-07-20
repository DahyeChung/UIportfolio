using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _instance;
    static Managers Instance { get { Init(); return _instance; } }

    UI_Manager _ui = new UI_Manager();
    public static UI_Manager UI { get { return Instance?._ui; } }

    //  [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
        if (_instance != null)
            return;

        GameObject go = new GameObject { name = "@Managers" };
        _instance = go.AddComponent<Managers>();
        DontDestroyOnLoad(go);

    }

    // Managers.Instance에 접근할 때마다 Init() 이 Awake 보다 먼저 호출되어 Find(@Managers)의 작업을 허용하지 않음.
    // UI_Manager ui = Managers.UI; → Managers.Instance 호출 → Init() 호출 → GameObject.Find()가 너무 일찍 실행되어 에러 발생.

    //public static void Init()
    //{
    //    if (_instance == null)
    //    {
    //        GameObject gameObject = GameObject.Find("@Managers");
    //        if (gameObject == null)
    //        {
    //            gameObject = new GameObject { name = "@Managers" };
    //            gameObject.AddComponent<Managers>();
    //        }
    //        DontDestroyOnLoad(gameObject);
    //        _instance = gameObject.GetComponent<Managers>();
    //    }
    //}
}
