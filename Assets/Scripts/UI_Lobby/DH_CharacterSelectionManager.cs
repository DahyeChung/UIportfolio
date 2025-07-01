using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DH_CharacterSelectionManager : MonoBehaviour
{
    [SerializeField]
    private DH_CardUiPrefab characterCardUIPrefab;
    [SerializeField]
    private Transform cardParent; // content

    [SerializeField]
    private List<CharacterData> characterList; // Scriptable object

    private CharacterData currentSelectedCard;

    [SerializeField]
    private TextMeshProUGUI characterLevelText;
    [SerializeField]
    private TextMeshProUGUI characterNameText;

    // 3D model Display
    [SerializeField]
    private Transform characterPrefabParent;

    public GameObject characterPrefab;


    private void Start()
    {
        SpawnCharacterCards();
        UpdateSelectedCharacter(characterList[0]); // Default display
    }

    // spawn prefab
    void SpawnCharacterCards()
    {
        foreach (var character in characterList)
        {
            var card = Instantiate(characterCardUIPrefab, cardParent);
            card.SetCardDetail(character, this);
        }
    }

    public void UpdateSelectedCharacter(CharacterData data)
    {

        // update character level and name
        currentSelectedCard = data;
        characterLevelText.text = currentSelectedCard.characterLevel.ToString();
        characterNameText.text = currentSelectedCard.characterName;

        if (characterPrefab != null)
        {
            Destroy(characterPrefab);
        }

        // 3D display
        characterPrefab = Instantiate(currentSelectedCard.characterPrefab, characterPrefabParent);
        characterPrefab.transform.localPosition = Vector3.zero;
        characterPrefab.transform.localRotation = Quaternion.identity;
    }
}
