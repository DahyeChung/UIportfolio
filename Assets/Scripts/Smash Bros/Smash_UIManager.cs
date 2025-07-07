using System.Collections.Generic;
using UnityEngine;

// UI 초기화, 생성 타이밍 관리
public class Smash_UIManager : MonoBehaviour
{
    [Header("Character Cards")]
    public List<Character> characterSO = new List<Character>(); // 추후 드래그 드랍 방식 개선
    public Transform charParent;
    public GameObject charPrefab;

    [Header("Player Slots")]
    private Smash_PlayerSlot playerSlot;
    public GameObject playerSlotPrefab;
    public Transform playerParent;
    private int playerCount = 4;
    // private List<Smash_PlayerSlot> playerSlots = new List<Smash_PlayerSlot>();
    // private int currentPlayerIndex = 0;

    private void OnEnable()
    {
        Smash_CharacterCard.OnCharacterSelected += OnCharcterSelected;
        Smash_CharacterCard.OnCharacterEntered += ShowHoverEffect;
    }

    private void OnDisable()
    {
        Smash_CharacterCard.OnCharacterSelected -= OnCharcterSelected;
        Smash_CharacterCard.OnCharacterEntered -= ShowHoverEffect;

    }

    private void Start()
    {
        CreateCards();
        CreatePlayerSlot();
    }

    // Make cards from scriptable object and set information
    void CreateCards()
    {
        foreach (Character character in characterSO)
        {
            GameObject cardObj = Instantiate(charPrefab, charParent);
            cardObj.name = character.characterName;

            Smash_CharacterCard cardView = cardObj.AddComponent<Smash_CharacterCard>(); // 스크립트 연결 

            cardView.SetCardInfo(character);
        }
    }

    void CreatePlayerSlot()
    {
        #region Multi Player Selection
        //// 주의 추후에 플레이어 인원이 변경 시 상응 가능하게 
        //for (int i = 0; i < playerCount; i++)
        //{
        //    GameObject slotObj = Instantiate(playerSlotPrefab, playerParent);
        //    slotObj._name = "player" + (i + 1);

        //    Smash_CharacterCard cardView = slotObj.AddComponent<Smash_CharacterCard>(); // 스크립트 연결 

        //    // playerSlots.Add(slotObj.GetComponent<Smash_PlayerSlot>()); // 리스트에 추가 해 둠

        //}

        #endregion

        // Single Player 
        GameObject slotObj = Instantiate(playerSlotPrefab, playerParent);
        slotObj.name = "Player 1";
        playerSlot = slotObj.GetComponent<Smash_PlayerSlot>();
    }

    void OnCharcterSelected(Character selectedCharacter)
    {
        playerSlot.SetInfo(selectedCharacter);
    }

    void ShowHoverEffect(Character selectedCharacter)
    {
        playerSlot.HoverEffect(selectedCharacter);
    }
}