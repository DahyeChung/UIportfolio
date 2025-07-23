using UnityEngine;
using UnityEngine.UI;

public class UI_LobbyScene : UI_Base
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

    [Header("메뉴 토글")]
    public ToggleGroup menuGroup;
    public Toggle HomeToggle;
    public Toggle CustomizeToggle;
    public Toggle CreativeToggle;
    public Toggle FamePassToggle;
    public Toggle ShopToggle;

    private UI_SettingPopup _settingPopup;
    private UI_HomePopup _homePopup;
    private UI_CustomizationPopup _customizePopup;
    private UI_FamePassPopup _famePopup;
    private UI_ShopPopup _shopPopup;
    private UI_CreativePopup _creativePopup;

    private void Awake()
    {
        Debug.Log("Awake");
        Init();
    }

    private void OnEnable()
    {
        HomeToggle.onValueChanged.AddListener(OnClickHomeToggle);
        CustomizeToggle.onValueChanged.AddListener(OnClickCustomizeToggle);
        CreativeToggle.onValueChanged.AddListener(OnClickCreativeToggle);
        FamePassToggle.onValueChanged.AddListener(OnClickFamePassToggle);
        ShopToggle.onValueChanged.AddListener(OnClickShopToggle);

    }
    private void OnDisable()
    {
        HomeToggle.onValueChanged.RemoveListener(OnClickHomeToggle);
        CustomizeToggle.onValueChanged.RemoveListener(OnClickCustomizeToggle);
        CreativeToggle.onValueChanged.RemoveListener(OnClickCreativeToggle);
        FamePassToggle.onValueChanged.RemoveListener(OnClickFamePassToggle);
        ShopToggle.onValueChanged.RemoveListener(OnClickShopToggle);
    }

    private void Start()
    {
        Debug.Log("UI_LobbyScene Start");

        Debug.Log("UI_LobbyScene Initialized");
    }
    void Init()
    {

        _homePopup = Managers.UI.ShowPopupUI<UI_HomePopup>();
        _customizePopup = Managers.UI.ShowPopupUI<UI_CustomizationPopup>();
        _famePopup = Managers.UI.ShowPopupUI<UI_FamePassPopup>();
        _shopPopup = Managers.UI.ShowPopupUI<UI_ShopPopup>();
        _settingPopup = Managers.UI.ShowPopupUI<UI_SettingPopup>();
        _creativePopup = Managers.UI.ShowPopupUI<UI_CreativePopup>();

        HomeToggle.group = menuGroup;
        CustomizeToggle.group = menuGroup;
        CreativeToggle.group = menuGroup;
        FamePassToggle.group = menuGroup;
        ShopToggle.group = menuGroup;

        menuGroup.allowSwitchOff = false;
        Debug.Log("Lobby Scene Initialized");
        ToggleInit();




    }


    void ToggleInit()
    {

        HomeToggle.isOn = true;
        CustomizeToggle.isOn = false;
        CreativeToggle.isOn = false;
        FamePassToggle.isOn = false;
        ShopToggle.isOn = false;

        _homePopup.gameObject.SetActive(true);
        _settingPopup.gameObject.SetActive(false);
        _customizePopup.gameObject.SetActive(false);
        _famePopup.gameObject.SetActive(false);
        _shopPopup.gameObject.SetActive(false);
        Debug.Log("Toggle Initialized");
    }



    public void SetInfo()
    {
        // 외부에서 정보 업데이트에 사용 
    }

    #region Click Event
    void OnClickSettingButton(bool isOn)
    {
        if (isOn) SetActiveOnly(_settingPopup);
    }
    private void OnClickHomeToggle(bool isOn)
    {
        if (isOn) SetActiveOnly(_homePopup);

    }
    private void OnClickCustomizeToggle(bool isOn)
    {
        if (isOn) SetActiveOnly(_customizePopup);
    }
    private void OnClickCreativeToggle(bool isOn)
    {
        if (isOn) SetActiveOnly(_creativePopup);
    }
    private void OnClickFamePassToggle(bool isOn)
    {
        if (isOn) SetActiveOnly(_famePopup);
    }
    private void OnClickShopToggle(bool isOn)
    {
        if (isOn) SetActiveOnly(_shopPopup);
    }
    private void SetActiveOnly(MonoBehaviour popupToActivate)
    {
        _homePopup?.gameObject.SetActive(_homePopup == popupToActivate);
        _customizePopup?.gameObject.SetActive(_customizePopup == popupToActivate);
        _creativePopup?.gameObject.SetActive(_creativePopup == popupToActivate);
        _famePopup?.gameObject.SetActive(_famePopup == popupToActivate);
        _shopPopup?.gameObject.SetActive(_shopPopup == popupToActivate);
    }
    #endregion

}
