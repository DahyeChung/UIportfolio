using UnityEngine;
using UnityEngine.UI;

public class UI_LobbyScene : MonoBehaviour
{
    #region Enum
    private enum GameObjects
    {
        MenuToggleGroup,
    }

    private enum Buttons
    {
        SettingButton,
    }
    private enum Toggles
    {
        HomeToggle,
        CustomizeToggle,
        CreativeToggle,
        FamePassToggle,
        ShopToggle,
    }


    #endregion
    public ToggleGroup menuGroup;

    [Header("메뉴 토글")]
    public Toggle HomeToggle;
    public Toggle CustomizeToggle;
    public Toggle CreativeToggle;
    public Toggle FamePassToggle;
    public Toggle ShopToggle;

    UI_SettingPopup settingPopup = Managers.UI.ShowPopupUI<UI_SettingPopup>();
    UI_HomePopup homePopup = Managers.UI.ShowPopupUI<UI_HomePopup>();
    UI_CustomizationPopup customizePopup = Managers.UI.ShowPopupUI<UI_CustomizationPopup>();
    UI_FamePassPopup famePopup = Managers.UI.ShowPopupUI<UI_FamePassPopup>();
    UI_ShopPopup shopPopup = Managers.UI.ShowPopupUI<UI_ShopPopup>();

    private void ToggleInit()
    {
        // menu group 이라는 오브젝트를 찾아서 
        // 하위에 있는 

    }
    protected void Awake()
    {
        HomeToggle.group = menuGroup;
        CustomizeToggle.group = menuGroup;
        CreativeToggle.group = menuGroup;
        FamePassToggle.group = menuGroup;
        ShopToggle.group = menuGroup;

        //
        menuGroup.allowSwitchOff = false;



        // Enum & 함수 바인딩
        CustomizeToggle.onValueChanged.AddListener(OnClickCustomizeToggle);
        CreativeToggle.onValueChanged.AddListener(OnClickCreativeToggle);
        FamePassToggle.onValueChanged.AddListener(OnClickFamePassToggle);
        ShopToggle.onValueChanged.AddListener(OnClickShopToggle);
        HomeToggle.onValueChanged.AddListener(OnClickHomeToggle);



    }
    private void Start()
    {
        HomeToggle.isOn = true;
    }

    private void OnEnable()
    {
        // 이벤트 리스너 구독
    }
    private void OnDisable()
    {
        // 이벤트 리스너 구독 해제
    }

    public void SetInfo()
    {
        // 외부에서 정보 업데이트에 사용 
    }

    #region Click Event
    void OnClickSettingButton(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(settingPopup);
    }



    void OnClickHomeToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(homePopup);
    }
    void OnClickCustomizeToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(customizePopup);
    }
    void OnClickCreativeToggle(bool isOn)
    {
        if (!isOn) return;

    }
    void OnClickFamePassToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(famePopup);
    }
    void OnClickShopToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(shopPopup);
    }
    private void SetActiveOnly(MonoBehaviour popupToActivate)
    {
        homePopup.gameObject.SetActive(homePopup == popupToActivate);
        customizePopup.gameObject.SetActive(customizePopup == popupToActivate);
        famePopup.gameObject.SetActive(famePopup == popupToActivate);
        shopPopup.gameObject.SetActive(shopPopup == popupToActivate);
        // creativePopup 등 추가 팝업이 있다면 여기 추가
    }
    #endregion

    // 모든 토글은 1개가 활성화 되면 나머지는 모두 비활성화 
}
