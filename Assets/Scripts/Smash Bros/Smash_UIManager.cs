using System.Collections.Generic;
using UnityEngine;

// UI 초기화, 생성 타이밍 관리
public class Smash_UIManager : MonoBehaviour
{
    public List<Character> characterSO = new List<Character>(); // 추후 드래그 드랍 방식 개선
    public Transform charParent;
    public GameObject charPrefab;

    private void Start()
    {
        // 캐릭터 카드 갯수 관리 
        CreateCards();

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

            Smash_UICharacterSelection cardView = cards.GetComponent<Smash_UICharacterSelection>();
            if (cardView != null)
                cardView = cards.AddComponent<Smash_UICharacterSelection>();

            cardView.SetInfo(character);

        }
    }

}