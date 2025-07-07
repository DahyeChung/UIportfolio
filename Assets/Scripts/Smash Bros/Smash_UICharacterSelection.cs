using TMPro;
using UnityEngine;
using UnityEngine.UI;

// UI 표시와 데이터 바인딩 
public class Smash_UICharacterSelection : MonoBehaviour
{


    // Data Bindind of Scriptable Object & UI cards
    // charPrefab 카드 정보세팅에만 사용 가능 
    // 호출될 때 
    public void SetCardInfo(Character characterData)
    {
        Image artwork = transform.Find("artwork").GetComponent<Image>();
        TextMeshProUGUI nameText = transform.Find("nameRect").GetComponentInChildren<TextMeshProUGUI>();

        if (artwork == null || nameText == null)
        {
            Debug.LogError("Missing UI element: 'artwork' or 'nameText' not found in prefab.");
            return;
        }

        artwork.sprite = characterData.characterSprite;
        nameText.text = characterData.characterName;

        artwork.GetComponent<RectTransform>().pivot = uiPivot(artwork.sprite);
        artwork.GetComponent<RectTransform>().sizeDelta *= characterData.zoom;

        Button selectButton;
        selectButton = GetComponent<Button>();
        selectButton.onClick.AddListener(OnCardClick);
        //  selectButton.OnPointerEnter.AddListener(OnCardHover);
    }

    public Vector2 uiPivot(Sprite sprite)
    {
        Vector2 pixelSize = new Vector2(sprite.texture.width, sprite.texture.height);
        Vector2 pixelPivot = sprite.pivot;
        return new Vector2(pixelPivot.x / pixelSize.x, pixelPivot.y / pixelSize.y);
    }
    public void SetPlayerInfo(Character characterData)
    {

    }

    void OnCardHover()
    {
        // 카드더미에 해당하는 이미지,이름 정보를 가져와서
        // 플레이어 칸의 이미지,이름 변경
        // **주의, Create Card 와 Set Info 함수가 실행된 이후에만 실행해야함
    }
    // 카드 클릭 감지 
    void OnCardClick()
    {
        Debug.Log("클릭됨");

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