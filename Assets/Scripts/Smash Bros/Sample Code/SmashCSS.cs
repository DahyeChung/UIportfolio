using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SmashCSS : MonoBehaviour
{

    private GridLayoutGroup gridLayout;
    [HideInInspector]
    public Vector2 slotArtworkSize;


    public static SmashCSS instance;
    [Header("Characters List")]
    public List<Character> characters = new List<Character>();
    [Space]
    [Header("Public References")]
    public GameObject charCellPrefab;
    public GameObject gridBgPrefab;
    public Transform playerSlotsContainer;
    [Space]
    [Header("Current Confirmed Character")]
    public Character confirmedCharacter;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

        gridLayout = GetComponent<GridLayoutGroup>();
        GetComponent<RectTransform>().sizeDelta = new Vector2(gridLayout.cellSize.x * 5, gridLayout.cellSize.y * 2);
        RectTransform gridBG = Instantiate(gridBgPrefab, transform.parent).GetComponent<RectTransform>();
        gridBG.transform.SetSiblingIndex(transform.GetSiblingIndex());
        gridBG.sizeDelta = GetComponent<RectTransform>().sizeDelta;

        slotArtworkSize = playerSlotsContainer.GetChild(0).Find("artwork").GetComponent<RectTransform>().sizeDelta;

        foreach (Character character in characters)
        {
            SpawnCharacterCell(character);
        }

    }

    private void SpawnCharacterCell(Character character)
    {
        GameObject charCell = Instantiate(charCellPrefab, transform);

        charCell.name = character.characterName;

        // 어느 컴포넌트에?
        Image artwork = charCell.transform.Find("artwork").GetComponent<Image>();
        TextMeshProUGUI name = charCell.transform.Find("nameRect").GetComponentInChildren<TextMeshProUGUI>();

        // 어떤 정보를?
        artwork.sprite = character.characterSprite;
        name.text = character.characterName;

        // 어떤 사이즈로?
        artwork.GetComponent<RectTransform>().pivot = uiPivot(artwork.sprite);
        artwork.GetComponent<RectTransform>().sizeDelta *= character.zoom;
    }

    public void ShowCharacterInSlot(int player, Character character)
    {
        bool nullChar = (character == null);

        Color alpha = nullChar ? Color.clear : Color.white;
        Sprite artwork = nullChar ? null : character.characterSprite;
        string name = nullChar ? string.Empty : character.characterName;

        string playernickname = "Player " + (player + 1).ToString();
        string playernumber = "P" + (player + 1).ToString();

        // 컨텐츠 박스에서 해당 플레이어의 이미지와 아이콘을 Find 하여 각각 애니메이션 지정
        Transform slot = playerSlotsContainer.GetChild(player);

        Transform slotArtwork = slot.Find("artwork");
        Transform slotIcon = slot.Find("icon");


        #region 이미지 선택 애니메이션 
        /*Append(): 순서대로 실행

          DOLocalMoveX(): X축으로 이동 애니메이션

          AppendCallback(): 중간에 코드 실행 삽입

          SetEase(): 애니메이션 곡선 설정*/
        Sequence s = DOTween.Sequence();
        s.Append(slotArtwork.DOLocalMoveX(-300, .05f).SetEase(Ease.OutCubic));
        s.AppendCallback(() => slotArtwork.GetComponent<Image>().sprite = artwork);
        s.AppendCallback(() => slotArtwork.GetComponent<Image>().color = alpha);
        s.Append(slotArtwork.DOLocalMoveX(300, 0));
        s.Append(slotArtwork.DOLocalMoveX(0, .05f).SetEase(Ease.OutCubic));

        if (nullChar)
        {
            slotIcon.GetComponent<Image>().DOFade(0, 0);
        }
        else
        {
            slotIcon.GetComponent<Image>().sprite = character.characterIcon;
            slotIcon.GetComponent<Image>().DOFade(.3f, 0);
        }

        if (artwork != null)
        {
            slotArtwork.GetComponent<RectTransform>().pivot = uiPivot(artwork);
            slotArtwork.GetComponent<RectTransform>().sizeDelta = slotArtworkSize;
            slotArtwork.GetComponent<RectTransform>().sizeDelta *= character.zoom;
        }
        slot.Find("name").GetComponent<TextMeshProUGUI>().text = name;
        slot.Find("player").GetComponentInChildren<TextMeshProUGUI>().text = playernickname;
        slot.Find("iconAndPx").GetComponentInChildren<TextMeshProUGUI>().text = playernumber;
        #endregion
    }

    public void ConfirmCharacter(int player, Character character)
    {
        if (confirmedCharacter == null)
        {
            confirmedCharacter = character;
            playerSlotsContainer.GetChild(player).DOPunchPosition(Vector3.down * 3, .3f, 10, 1);
        }
    }

    public Vector2 uiPivot(Sprite sprite)
    {
        Vector2 pixelSize = new Vector2(sprite.texture.width, sprite.texture.height);
        Vector2 pixelPivot = sprite.pivot;
        return new Vector2(pixelPivot.x / pixelSize.x, pixelPivot.y / pixelSize.y);
    }

}
