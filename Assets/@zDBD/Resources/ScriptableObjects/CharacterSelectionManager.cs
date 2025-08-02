// CharacterSelectionManager.cs
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages the character selection screen.
/// It reads all available CharacterData assets, dynamically populates the UI,
/// handles selection, and persists the choice to the next scene.
/// </summary>
public class CharacterSelectionManager : MonoBehaviour
{
    [Header("Character Data")]
    [Tooltip("A list of all character data assets available for selection.")]
    public List<CharacterData> allCharacters;

    [Header("UI References")]
    [Tooltip("The parent object where character UI cards will be instantiated.")]
    public Transform cardParent; // content box

    [Tooltip("The UI prefab for displaying a single character's info.")]
    public GameObject characterCardPrefab;

    [Header("Main Display Panel")]
    [Tooltip("Displays the 3D model of the currently selected character.")]
    public Transform characterDisplayParent;
    public TextMeshProUGUI displayNameText;
    public TextMeshProUGUI displayLevelText;
    public TextMeshProUGUI displayDescriptionText;
    // public Button confirmButton;

    private int selectedCharacterIndex = 0;
    private GameObject currentDisplayModel;

    void Start()
    {
        // Ensure the confirm button is initially not interactable until a character is chosen
        // confirmButton.interactable = false;

        // Populate the UI with all available characters
        PopulateCharacterCards();

        // Select the first character by default
        if (allCharacters.Count > 0)
        {
            SelectCharacter(0); // 기본값 1번 SO
        }
    }

    /// <summary>
    /// Instantiates and sets up a UI card for each character in the allCharacters list.
    /// </summary>
    private void PopulateCharacterCards()
    {
        // 캐릭터 선택란 컨텐츠 박스에 캐릭터UI카드를 생성하는 작업 
        for (int i = 0; i < allCharacters.Count; i++)
        {
            GameObject cardGO = Instantiate(characterCardPrefab, cardParent);
            CharacterCardUI cardUI = cardGO.GetComponent<CharacterCardUI>();

            // Pass the data and its index to the card
            cardUI.Setup(allCharacters[i], this, i);
        }
    }

    /// <summary>
    /// This method is called by CharacterCardUI when a card is clicked.
    /// </summary>
    /// <param _name="index">The index of the character in the allCharacters list.</param>
    public void SelectCharacter(int index)
    {
        // 인텍스가 현재 등록한 SO 캐릭터 갯수와 맞지 않으면 에러처리 인텍스 활용은 어떻게 하는가는 확인요마ㅐㅇ
        if (index < 0 || index >= allCharacters.Count)
        {
            Debug.LogError("Invalid character index selected.");
            return;
        }

        selectedCharacterIndex = index;
        CharacterData selectedData = allCharacters[selectedCharacterIndex];

        // Update the main display panel with the selected character's info
        displayNameText.text = selectedData.characterName;
        displayLevelText.text = selectedData.characterLevel.ToString();
        displayDescriptionText.text = selectedData.description;

        // Update the 3D model display
        if (currentDisplayModel != null)
        {
            Destroy(currentDisplayModel);
        }
        currentDisplayModel = Instantiate(selectedData.characterPrefab, characterDisplayParent);
        // You might want to adjust the spawned model's scale, rotation, and position
        currentDisplayModel.transform.localPosition = Vector3.zero;
        currentDisplayModel.transform.localRotation = Quaternion.identity;

        // Enable the confirm button now that a selection has been made
        //  confirmButton.interactable = true;

        Debug.Log("Selected Character" + allCharacters[selectedCharacterIndex].characterName);
    }

    /// <summary>
    /// Called when the 'Confirm' button is clicked.
    /// Saves the selected character and loads the main game scene.
    /// </summary>
    public void ConfirmSelection()
    {
        // Use a persistent manager to carry the data to the next scene
        // GameDataManager.Instance.SetSelectedCharacter(allCharacters[selectedCharacterIndex]);

        // Load your main game scene (make sure it's in your Build Settings)
        // SceneManager.LoadScene("MainGameScene"); // Change "MainGameScene" to your actual scene _name
        Debug.Log("Selected Character" + allCharacters[selectedCharacterIndex].characterName);
    }
}
