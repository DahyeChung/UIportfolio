// CharacterCardUI.cs
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sits on the character card prefab. It holds references to its own UI elements
/// and tells the manager when it has been clicked.
/// </summary>
public class CharacterCardUI : MonoBehaviour
{
    [Header("UI Element References")]
    // public TextMeshProUGUI _nameText;
    public Image iconImage;
    public Button selectButton;

    private CharacterSelectionManager manager;
    private int characterIndex;

    /// <summary>
    /// Populates the UI card with the given character's data.
    /// </summary>
    public void Setup(CharacterData data, CharacterSelectionManager selectionManager, int index)
    {
        manager = selectionManager;
        characterIndex = index;

        // _nameText.text = data.characterName;
        iconImage.sprite = data.characterSprite;

        // Add a listener to the button's onClick event
        selectButton.onClick.AddListener(OnCardClicked);
    }

    /// <summary>
    /// Called when this card's button is clicked. Notifies the manager.
    /// </summary>
    private void OnCardClicked()
    {
        manager.SelectCharacter(characterIndex);
    }

    // It's good practice to remove listeners when the object is destroyed
    private void OnDestroy()
    {
        selectButton.onClick.RemoveListener(OnCardClicked);
    }
}