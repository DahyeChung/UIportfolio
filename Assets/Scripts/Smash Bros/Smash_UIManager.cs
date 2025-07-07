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
    public int playerCount = 4;
    // private List<Smash_PlayerSlot> playerSlots = new List<Smash_PlayerSlot>();
    // private int currentPlayerIndex = 0;

    private void OnEnable()
    {
        Smash_CharacterCard.OnCharacterSelected += OnCharcterSelected;
    }

    private void OnDisable()
    {
        Smash_CharacterCard.OnCharacterSelected -= OnCharcterSelected;
    }

    private void Start()
    {
        CreateCards();
        CreatePlayerSlot();
    }
    void CreateCards()
    {
        // 카드 프리팹 생성 및 SO 정보 넘겨주는 일 까지만 
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
        //// 주의 추후에 플레이어 인원이 변경 시 상응 가능하게 
        //for (int i = 0; i < playerCount; i++)
        //{
        //    GameObject slotObj = Instantiate(playerSlotPrefab, playerParent);
        //    slotObj.name = "player" + (i + 1);

        //    Smash_CharacterCard cardView = slotObj.AddComponent<Smash_CharacterCard>(); // 스크립트 연결 

        //    // playerSlots.Add(slotObj.GetComponent<Smash_PlayerSlot>()); // 리스트에 추가 해 둠

        //}

        GameObject slotObj = Instantiate(playerSlotPrefab, playerParent);
        slotObj.name = "Player 1";
        playerSlot = slotObj.GetComponent<Smash_PlayerSlot>();
    }

    // 캐릭터가 선택되었을 때 캐릭터 카드 정보를 플레이어 슬랏 정보에 연결 
    void OnCharcterSelected(Character selectedCharacter) // event
    {
        playerSlot.SetInfo(selectedCharacter);
    }


}