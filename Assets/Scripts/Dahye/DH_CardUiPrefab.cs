using UnityEngine;
using UnityEngine.UI;

public class DH_CardUiPrefab : MonoBehaviour
{
    private CharacterData characterData; // to hold data in this class

    [SerializeField]
    private Image spriteImage;
    [SerializeField]
    private Button cardButton;

    private DH_CharacterSelectionManager characterSelectionManager;

    private void Start()
    {
        cardButton.onClick.AddListener(OnCardClicked);
    }

    public void SetCardDetail(CharacterData data, DH_CharacterSelectionManager i_characterSelectionManager)
    {


        characterData = data;
        spriteImage.sprite = characterData.characterSprite;

        characterSelectionManager = i_characterSelectionManager;
    }

    private void OnCardClicked()
    {

        characterSelectionManager.UpdateSelectedCharacter(characterData);


    }

    private void OnDestroy()
    {
        cardButton.onClick.RemoveAllListeners();
    }

}
