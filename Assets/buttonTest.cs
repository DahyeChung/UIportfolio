using UnityEngine;
using UnityEngine.UI;

public class buttonTest : MonoBehaviour
{
    private Button btn;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Debug.Log("클릭됨");
    }

}
