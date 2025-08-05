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

    // OnEnable/OnDisable 부분은 그대로 유지합니다. 좋은 습관입니다.
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

        // ★★★ 변경점 1: 초기화 시 첫 토글을 켜서 이벤트가 발생하도록 유도 ★★★
        // 이렇게 하면 게임 시작 시점에 첫 카테고리의 버튼들이 자동으로 생성됩니다.
        if (ColorToggle != null)
        {
            ColorToggle.isOn = true;
        }
    }

    // Update 함수는 그대로 유지
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
    // SetActiveOnly 함수는 그대로 유지
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

    // ★★★ 변경점 2: 모든 토글 이벤트 함수들이 버튼 생성 함수를 호출하도록 수정 ★★★

    void OnClickColorToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(ColorContents);
        // 색상 탭은 버튼 생성이 필요 없으므로 아무것도 하지 않음.
    }

    void OnClickWeaponToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(WeaponContents);
        // Weapon 토글이 켜지면, 즉시 Weapon 버튼들을 생성!
        GeneratePartButtons(CharacterPartType.Weapon, weaponButtonsContainer);
    }

    void OnClickFaceToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(FaceContents);
        GeneratePartButtons(CharacterPartType.Face, faceButtonsContainer);
    }

    void OnClickBodyToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(BodyContents);
        GeneratePartButtons(CharacterPartType.Body, bodyButtonsContainer);
    }

    void OnClickHeadPartToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(HeadPartContents);
        GeneratePartButtons(CharacterPartType.Head, headButtonsContainer);
    }

    void OnClickTailToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(TailContents);
        GeneratePartButtons(CharacterPartType.Tail, tailButtonsContainer);
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

    // ★★★ 변경점 3: 아래의 공개 API 함수들은 이제 필요 없습니다. ★★★
    // OnClickWeaponButton, OnClickFaceButton 등은 토글 이벤트가 그 역할을 대신하므로
    // 삭제하거나, 다른 곳에서 사용하지 않는다면 주석 처리해도 됩니다.
    /* public void OnClickWeaponButton()
    {
        GeneratePartButtons(CharacterPartType.Weapon, weaponButtonsContainer);
    }
    // ... 나머지 OnClick...Button 함수들 ...
    */
    #endregion
}