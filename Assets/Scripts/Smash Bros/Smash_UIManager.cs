using System.Collections.Generic;
using UnityEngine;

// UI 초기화, 생성 타이밍 관리
public class Smash_UIManager : MonoBehaviour
{
    public List<Character> characterSO = new List<Character>(); // 추후 드래그 드랍 방식 개선
    public Transform charParent;
    public GameObject charPrefab;

    public GameObject playerSlot;
    public Transform playerParent;
    public int playerCount = 5;


    private void Start()
    {
        // 캐릭터 카드 갯수 관리 
        CreateCards();
        CreatePlayerCards();
    }
    void CreateCards()
    {
        // 카드 프리팹 생성 및 SO 정보 넘겨주는 일 까지만 
        foreach (Character character in characterSO)
        {
            // 게임 오브젝트 형태의 카드를 부모 객체 위치에 생성 
            // 인스턴스 생성 및 각 카드별 클래스 기능 부여
            // 각 캐릭터 정보 바인딩 

            GameObject cards = Instantiate(charPrefab, charParent);
            cards.name = character.characterName;

            Smash_UICharacterSelection cardView = cards.AddComponent<Smash_UICharacterSelection>(); // 스크립트 연결 


            cardView.SetCardInfo(character);


        }
    }

    void CreatePlayerCards()
    {
        // 주의 추후에 플레이어 인원이 변경 시 상응 가능하게 
        for (int i = 1; i < playerCount; i++)
        {
            GameObject playerCards = Instantiate(playerSlot, playerParent);
            playerCards.name = "player" + i.ToString();

            Smash_UICharacterSelection cardView = playerCards.AddComponent<Smash_UICharacterSelection>(); // 스크립트 연결 



        }


    }

}