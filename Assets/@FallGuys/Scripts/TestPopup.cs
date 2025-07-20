using UnityEngine;

public class TestPopup : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Managers.UI.ShowPopupUI<UI_HomePopup>();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Managers.UI.ShowPopupUI<UI_CustomizationPopup>();
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Managers.UI.ShowPopupUI<UI_FamePassPopup>();
        if (Input.GetKeyDown(KeyCode.Alpha4))
            Managers.UI.ShowPopupUI<UI_ShopPopup>();
        if (Input.GetKeyDown(KeyCode.Alpha5))
            Managers.UI.ShowPopupUI<UI_CreativePopup>();

    }
}
