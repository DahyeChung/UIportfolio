using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 각 파츠 타입에 필요한 UI 요소들을 묶어서 관리하기 위한 클래스
[System.Serializable]
public class PartsCategory
{
    public CharacterPartType type;
    public Toggle toggle;
    public GameObject contentPanel;
    public Transform buttonContainer;
    public string categoryName;
}

public class UI_OutfitPopup : UI_Popup
{
    private UI_CustomizationPopup _customizationPopup;

    [Header("공통 UI")]
    [SerializeField] private ToggleGroup customToggleGroup;
    [SerializeField] private TextMeshProUGUI selectedToggleText;
    [SerializeField] private GameObject partButtonPrefab;

    [Header("색상 UI (별도 관리)")]
    [SerializeField] private Toggle ColorToggle;
    [SerializeField] private GameObject ColorContents;

    [Header("파츠 UI 목록")]
    [Tooltip("여기에 각 파츠 타입별 UI 요소들을 연결해주세요.")]
    [SerializeField] private List<PartsCategory> uiPartLinks;

    [Header("커스텀 매니저")]
    public CharacterCustomSelection customManager;

    private void OnEnable()
    {
        if (ContentsObject != null)
            PopupAnimation(ContentsObject);

        // 리스너 등록
        ColorToggle.onValueChanged.AddListener(OnClickColorToggle);
        foreach (var link in uiPartLinks)
        {
            PartsCategory currentLink = link;
            link.toggle.onValueChanged.AddListener((isOn) => OnPartToggleChanged(isOn, currentLink));
        }
    }

    private void OnDisable()
    {
        ColorToggle.onValueChanged.RemoveListener(OnClickColorToggle);
        foreach (var link in uiPartLinks)
        {
            link.toggle.onValueChanged.RemoveAllListeners();
        }
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
        // 토글 그룹 설정
        ColorToggle.group = customToggleGroup;
        foreach (var link in uiPartLinks)
        {
            link.toggle.group = customToggleGroup;
        }

        // 기본으로 색상 토글을 선택
        if (ColorToggle != null)
        {
            ColorToggle.isOn = true;
            OnClickColorToggle(true); // 활성화 시 컨텐츠도 바로 보이도록 호출
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

    // 하나의 콘텐츠 패널만 활성화하는 함수
    void SetActiveOnly(GameObject contentToActivate)
    {
        // 모든 콘텐츠 패널 목록을 동적으로 생성
        var allContents = uiPartLinks.Select(link => link.contentPanel).ToList();
        allContents.Add(ColorContents);

        foreach (GameObject content in allContents)
        {
            if (content != null)
                content.SetActive(content == contentToActivate);
        }
    }

    // 색상 토글 전용 이벤트 핸들러
    void OnClickColorToggle(bool isOn)
    {
        if (!isOn) return;
        SetActiveOnly(ColorContents);
        selectedToggleText.text = "COLOR";
    }

    // 모든 파츠 토글을 처리하는 제네릭 이벤트 핸들러
    void OnPartToggleChanged(bool isOn, PartsCategory link)
    {
        if (!isOn) return;

        SetActiveOnly(link.contentPanel);
        GeneratePartButtons(link.type, link.buttonContainer);
        selectedToggleText.text = link.categoryName;
    }

    #endregion

    #region Custom Parts Event

    private void GeneratePartButtons(CharacterPartType type, Transform container)
    {
        if (container == null || customManager == null || partButtonPrefab == null) return;


        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        int partCount = customManager.GetPartsCount(type);

        for (int i = 0; i < partCount; i++)
        {
            GameObject buttonObj = Instantiate(partButtonPrefab, container);

            // Text 대신 TextMeshProUGUI 사용을 권장합니다.
            TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = customManager.GetPartsName(type, i);
            }

            Button button = buttonObj.GetComponent<Button>();
            int itemIndex = i;

            button.onClick.AddListener(() =>
            {
                customManager.ChangePart(type, itemIndex);
            });
        }
    }

    #endregion
}