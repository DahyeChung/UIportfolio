//using UnityEngine;

//public class TestPopup : MonoBehaviour
//{


//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Alpha1)
//            Managers.UI.ShowPopupUI<UI_HomePopup>();
//        if (Input.GetKeyDown(KeyCode.Alpha2))
//            Managers.UI.ShowPopupUI<UI_CustomizationPopup>();
//        if (Input.GetKeyDown(KeyCode.Alpha3))
//            Managers.UI.ShowPopupUI<UI_FamePassPopup>();
//        if (Input.GetKeyDown(KeyCode.Alpha4))
//            Managers.UI.ShowPopupUI<UI_ShopPopup>();
//        if (Input.GetKeyDown(KeyCode.Alpha5))
//            Managers.UI.ShowPopupUI<UI_CreativePopup>();

//    }
//}

using UnityEngine;

public class TestPopup : MonoBehaviour
{
    private UI_SettingPopup _settingPopup;
    private UI_HomePopup _homePopup;
    private UI_CustomizationPopup _customizePopup;
    private UI_FamePassPopup _famePopup;
    private UI_ShopPopup _shopPopup;
    private UI_CreativePopup _creativePopup;

    private void Start()
    {
        _homePopup = Managers.UI.ShowPopupUI<UI_HomePopup>();
        _customizePopup = Managers.UI.ShowPopupUI<UI_CustomizationPopup>();
        _famePopup = Managers.UI.ShowPopupUI<UI_FamePassPopup>();
        _shopPopup = Managers.UI.ShowPopupUI<UI_ShopPopup>();
        _settingPopup = Managers.UI.ShowPopupUI<UI_SettingPopup>();
        _creativePopup = Managers.UI.ShowPopupUI<UI_CreativePopup>();

    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
            _homePopup.gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            _customizePopup.gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            _famePopup.gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            _shopPopup.gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            _settingPopup.gameObject.SetActive(true);

    }
}

/*
 * Update에서는 잘 되는거 보면 순서문제는 맞는것같은디
 * 
 */