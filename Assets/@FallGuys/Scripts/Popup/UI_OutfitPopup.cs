using UnityEngine;
using UnityEngine.UI;

public class UI_OutfitPopup : UI_Popup
{
    private UI_CustomizationPopup _customizationPopup;

    [Header("토글")]
    public ToggleGroup customToggleGroup;
    public Toggle ColorToggle;
    public Toggle WeaponToggle;
    public Toggle FaceToggle;
    public Toggle BodyToggle;
    public Toggle HeadPartToggle;
    public Toggle TailToggle;

    [Header("온오프 컨텐츠")]
    public GameObject ColorContents;
    public GameObject WeaponContents;
    public GameObject FaceContents;
    public GameObject BodyContents;
    public GameObject HeadPartContents;
    public GameObject TailContents;





    private void OnEnable()
    {
        PopupAnimation(ContentsObject);


        ColorToggle.onValueChanged.AddListener(OnClickColorToggle);
        WeaponToggle.onValueChanged.AddListener(OnClickWeaponToggle);
        FaceToggle.onValueChanged.AddListener(OnClickFaceToggle);
        BodyToggle.onValueChanged.AddListener(OnClickBodyToggle);
        HeadPartToggle.onValueChanged.AddListener(OnClickHeadPartToggle);
        TailToggle.onValueChanged.AddListener(OnClickTailToggle);


    }
    private void OnDisable()
    {
        ColorToggle.onValueChanged.RemoveListener(OnClickColorToggle);
        WeaponToggle.onValueChanged.RemoveListener(OnClickWeaponToggle);
        FaceToggle.onValueChanged.RemoveListener(OnClickFaceToggle);
        BodyToggle.onValueChanged.RemoveListener(OnClickBodyToggle);
        HeadPartToggle.onValueChanged.RemoveListener(OnClickHeadPartToggle);
        TailToggle.onValueChanged.RemoveListener(OnClickTailToggle);
    }


    public override void Init()
    {
        _customizationPopup = FindObjectOfType<UI_CustomizationPopup>();

        ToggleInit();
    }

    void ToggleInit()
    {
        ColorToggle.group = customToggleGroup;
        WeaponToggle.group = customToggleGroup;
        FaceToggle.group = customToggleGroup;
        BodyToggle.group = customToggleGroup;
        HeadPartToggle.group = customToggleGroup;
        TailToggle.group = customToggleGroup;

        ColorToggle.isOn = true;
        WeaponToggle.isOn = false;
        FaceToggle.isOn = false;
        BodyToggle.isOn = false;
        HeadPartToggle.isOn = false;
        TailToggle.isOn = false;

        ColorContents.SetActive(true);
        WeaponContents.SetActive(false);
        FaceContents.SetActive(false);
        BodyContents.SetActive(false);
        HeadPartContents.SetActive(false);
        TailContents.SetActive(false);

    }

    private void Update()
    {   // event?
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ClosePopupUI();
            _customizationPopup.ContentsObject.SetActive(true);
        }
    }

    void SetActiveOnly(GameObject contentToActivate)
    {
        GameObject[] allContents = new GameObject[]
        {
        ColorContents,
        WeaponContents,
        FaceContents,
        BodyContents,
        HeadPartContents,
        TailContents
        };

        foreach (GameObject content in allContents)
        {
            if (content == contentToActivate)
            {
                content.SetActive(true);
            }
            else
            {
                content.SetActive(false);
            }
        }
    }

    void OnClickColorToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(ColorContents);
    }

    void OnClickWeaponToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(WeaponContents);
    }

    void OnClickFaceToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(FaceContents);
    }

    void OnClickBodyToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(BodyContents);
    }

    void OnClickHeadPartToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(HeadPartContents);
    }

    void OnClickTailToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(TailContents);
    }


}
