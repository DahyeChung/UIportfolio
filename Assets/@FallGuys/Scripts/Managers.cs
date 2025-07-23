using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _instance;
    static Managers Instance { get { Init(); return _instance; } }

    UI_Manager _ui = new UI_Manager();
    public static UI_Manager UI { get { return Instance?._ui; } }

    public static void Init()
    {
        if (_instance != null)
            return;

        Debug.Log("Managers Init called.");
        GameObject go = GameObject.Find("@Managers");
        if (go == null)
        {
            go = new GameObject { name = "@Managers" };
        }
        go.AddComponent<Managers>();
        DontDestroyOnLoad(go);
        _instance = go.GetComponent<Managers>();
    }


}
