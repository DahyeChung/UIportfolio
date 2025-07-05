using TMPro;
using UnityEngine;
using UnityEngine.UI;

// UI 표시와 데이터 바인딩 
public class Smash_UICharacterSelection : MonoBehaviour
{
    private Smash_UIManager characterManager;

    [Header("Images")]
    private Sprite _optionImage;
    private Sprite _selectedImage;

    [Header("Texts")]
    private TextMeshProUGUI _optionName;
    private TextMeshProUGUI _selectedName;

    void Start()
    {

    }


    // Data Bindind of Scriptable Object & UI cards
    // charPrefab 카드 정보세팅에만 사용 가능 
    // 호출될 때 
    public void SetInfo(Character characterData)
    {
        Image artwork = transform.Find("artwork").GetComponent<Image>();
        TextMeshPro nameText = transform.Find("nameText").GetComponent<TextMeshPro>();

        if (artwork || nameText == null)
        {
            Debug.Log("Missing UI element: not found in prefab");
            return;
        }

        artwork.sprite = characterData.characterSprite;
        nameText.text = characterData.characterName;
    }

    void OnCardHover()
    {
        // 카드더미에 해당하는 이미지,이름 정보를 가져와서
        // 플레이어 칸의 이미지,이름 변경
        // **주의, Create Card 와 Set Info 함수가 실행된 이후에만 실행해야함
    }
    void OnCardClick()
    {
        // 클릭 시 P1 아이콘 커서 위치에 고정시키고 
        // 플레이어 칸의 이미지 이름 고정
        // **주의, Create Card 와 Set Info 함수가 실행된 이후에만 실행해야함

    }

}


/* 클래스 목적
 * 빈 카드더미 생성, 카드정보 부여, 마우스올렸을 때 기능, 마우스 클릭 시 기능
 * 
 * 고려할 점. 캐릭터 리스트에 count가 몇개가 되어도 영향 없이 만들기
 */