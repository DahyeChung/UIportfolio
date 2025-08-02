using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// UI 표시와 데이터 바인딩 
public class Smash_CharacterCard : MonoBehaviour, IPointerEnterHandler
{
    public static event Action<Character> OnCharacterSelected;
    public static event Action<Character> OnCharacterEntered;

    private Character characterData;

    private Image _artwork;
    private TextMeshProUGUI _nameText;

    void Awake()
    {
        _artwork = transform.Find("artwork").GetComponent<Image>();
        _nameText = transform.Find("nameRect").GetComponentInChildren<TextMeshProUGUI>();
    }

    // Data Bindind of Scriptable Object & UI cards
    public void SetCardInfo(Character data)
    {
        characterData = data;

        _artwork.sprite = characterData.characterSprite;
        _nameText.text = characterData.characterName;

        _artwork.GetComponent<RectTransform>().pivot = uiPivot(_artwork.sprite);
        _artwork.GetComponent<RectTransform>().sizeDelta *= characterData.zoom;

        Button selectButton;
        selectButton = GetComponent<Button>();
        selectButton.onClick.AddListener(OnCardClick);
    }

    public Vector2 uiPivot(Sprite sprite)
    {
        Vector2 pixelSize = new Vector2(sprite.texture.width, sprite.texture.height);
        Vector2 pixelPivot = sprite.pivot;
        return new Vector2(pixelPivot.x / pixelSize.x, pixelPivot.y / pixelSize.y);
    }

    // Click Detect
    void OnCardClick()
    {
        OnCharacterSelected?.Invoke(characterData); // 버튼 클릭 시 On Character Selected 이벤트 발생 
    }

    // Hover Detect
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnCharacterEntered?.Invoke(characterData);

    }
}
