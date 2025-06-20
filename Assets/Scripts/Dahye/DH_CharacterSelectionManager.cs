using System.Collections.Generic;
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


    private void Start()
    {
        SpawnCharacterCards();
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
        currentSelectedCard = data; // TODO: update character level and name
        Debug.Log("current selected card:" + data.name);
    }
}
