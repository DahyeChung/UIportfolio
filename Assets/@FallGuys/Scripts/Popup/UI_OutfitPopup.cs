using TMPro;
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

    [Header("커스텀 파츠")]
    public CharacterCustomSelection customManager;
    public GameObject partButtonPrefab;

    // TODO: 플레이어 프리팹과 직접 연결함. 개선 방법 모색 필요
    public Transform weaponButtonsContainer;
    public Transform faceButtonsContainer;
    public Transform bodyButtonsContainer;
    public Transform headButtonsContainer;
    public Transform tailButtonsContainer;

    public TextMeshProUGUI selectedToggleText;

    private void OnEnable()
    {
        if (ContentsObject != null)
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
        if (customManager == null)
            customManager = FindObjectOfType<CharacterCustomSelection>();

        if (customManager == null)
            Debug.LogError("씬에 CharacterCustomSelection 스크립트를 가진 오브젝트가 없습니다!");

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

        if (ColorToggle != null)
        {
            ColorToggle.isOn = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ClosePopupUI();
            if (_customizationPopup != null && _customizationPopup.ContentsObject != null)
                _customizationPopup.ContentsObject.SetActive(true);
        }
    }

    #region Toggle Event

    void SetActiveOnly(GameObject contentToActivate)
    {
        GameObject[] allContents = {
            ColorContents, WeaponContents, FaceContents,
            BodyContents, HeadPartContents, TailContents
        };
        foreach (GameObject content in allContents)
        {
            // null 체크 추가하여 안정성 향상
            if (content != null)
                content.SetActive(content == contentToActivate);
        }
    }


    void OnClickColorToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(ColorContents);
        selectedToggleText.text = "COLOR";
    }

    void OnClickWeaponToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(WeaponContents);
        GeneratePartButtons(CharacterPartType.Weapon, weaponButtonsContainer);
        selectedToggleText.text = "WEAPON";
    }

    void OnClickFaceToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(FaceContents);
        GeneratePartButtons(CharacterPartType.Face, faceButtonsContainer);
        selectedToggleText.text = "FACE";

    }

    void OnClickBodyToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(BodyContents);
        GeneratePartButtons(CharacterPartType.Body, bodyButtonsContainer);
        selectedToggleText.text = "BODY";

    }

    void OnClickHeadPartToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(HeadPartContents);
        GeneratePartButtons(CharacterPartType.Head, headButtonsContainer);
        selectedToggleText.text = "HEAD";

    }

    void OnClickTailToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(TailContents);
        GeneratePartButtons(CharacterPartType.Tail, tailButtonsContainer);
        selectedToggleText.text = "TAIL";

    }
    #endregion

    #region Custom Parts Event

    private void GeneratePartButtons(CharacterPartType type, Transform container)
    {
        // 컨테이너가 설정되지 않았으면 함수를 실행하지 않음 (안정성)
        if (container == null) return;

        // 기존 버튼들 제거
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        int partCount = customManager.GetPartsCount(type);

        for (int i = 0; i < partCount; i++)
        {
            // 버튼 프리팹이 설정되지 않았으면 중단 (안정성)
            if (partButtonPrefab == null) continue;

            GameObject buttonObj = Instantiate(partButtonPrefab, container);

            Text buttonText = buttonObj.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = customManager.GetPartsName(type, i);
            }

            Button button = buttonObj.GetComponent<Button>();
            int itemIndex = i;

            // 불필요한 캐스팅 제거
            button.onClick.AddListener(() =>
            {
                customManager.ChangePart(type, itemIndex);
            });
        }
    }

    #endregion
}