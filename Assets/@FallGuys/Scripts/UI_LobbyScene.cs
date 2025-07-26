using DG.Tweening;
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
    // public Toggle SettingToggle;
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

    [Header("임시")]
    public AudioSource audioSource;
    public AudioClip popupSfx;

    private void Awake()
    {
        Debug.Log("Awake");
        Init();
    }

    private void OnEnable()
    {
        // SettingToggle.onValueChanged.AddListener(OnClickSettingButton);
        HomeToggle.onValueChanged.AddListener(OnClickHomeToggle);
        CustomizeToggle.onValueChanged.AddListener(OnClickCustomizeToggle);
        CreativeToggle.onValueChanged.AddListener(OnClickCreativeToggle);
        FamePassToggle.onValueChanged.AddListener(OnClickFamePassToggle);
        ShopToggle.onValueChanged.AddListener(OnClickShopToggle);

    }
    private void OnDisable()
    {
        // SettingToggle.onValueChanged.RemoveListener(OnClickSettingButton);
        HomeToggle.onValueChanged.RemoveListener(OnClickHomeToggle);
        CustomizeToggle.onValueChanged.RemoveListener(OnClickCustomizeToggle);
        CreativeToggle.onValueChanged.RemoveListener(OnClickCreativeToggle);
        FamePassToggle.onValueChanged.RemoveListener(OnClickFamePassToggle);
        ShopToggle.onValueChanged.RemoveListener(OnClickShopToggle);
    }

    private void Start()
    {

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
        // SettingToggle.isOn = false;

        _homePopup.gameObject.SetActive(true);
        _settingPopup.gameObject.SetActive(false);
        _customizePopup.gameObject.SetActive(false);
        _creativePopup.gameObject.SetActive(false);
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
        if (!isOn) return;
        SetActiveOnly(_settingPopup);
        // ToggleEffect(SettingToggle.gameObject);
    }
    private void OnClickHomeToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(_homePopup);
        ToggleEffect(HomeToggle.gameObject);
        // 클릭 시 이미지 색상 바꾸기 
        // HomeToggle.GetComponent<Image>().color = isOn ? Color.white : Color.gray;
        // 이미지 경로를 enum으로 pint blue 선언 후 enum으로 쓰는 게 좋을 것 같은데
        // 아니면 컬러코드로 사용?
    }
    private void OnClickCustomizeToggle(bool isOn)
    {
        if (!isOn) return;

        SetActiveOnly(_customizePopup);
        ToggleEffect(CustomizeToggle.gameObject);
    }
    private void OnClickCreativeToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(_creativePopup);
        ToggleEffect(CreativeToggle.gameObject);

    }
    private void OnClickFamePassToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(_famePopup);
        ToggleEffect(FamePassToggle.gameObject);
    }
    private void OnClickShopToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(_shopPopup);
        ToggleEffect(ShopToggle.gameObject);
    }
    private void SetActiveOnly(MonoBehaviour popupToActivate)
    {
        _homePopup?.gameObject.SetActive(_homePopup == popupToActivate);
        _customizePopup?.gameObject.SetActive(_customizePopup == popupToActivate);
        _creativePopup?.gameObject.SetActive(_creativePopup == popupToActivate);
        _famePopup?.gameObject.SetActive(_famePopup == popupToActivate);
        _shopPopup?.gameObject.SetActive(_shopPopup == popupToActivate);
    }

    private void ToggleEffect(GameObject toggle)
    {
        toggle.transform.DOKill();
        toggle.transform.localScale = Vector3.one;
        toggle.transform.DOPunchScale(Vector3.one * 0.2f, 0.3f, 10, 1);
        audioSource.PlayOneShot(popupSfx);


    }

    #endregion
}
