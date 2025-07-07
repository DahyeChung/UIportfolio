using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 역할: 선택된 캐릭터 정보를 받아서 슬롯 UI에 표시
public class Smash_PlayerSlot : MonoBehaviour
{
    // UIManager가 호출하여 슬롯의 캐릭터 정보를 갱신
    // 슬롯을 비우는 함수

    private Image artwork;
    private TextMeshProUGUI name;

    private void Awake()
    {
        artwork = transform.Find("artwork").GetComponent<Image>();
        name = transform.Find("name").GetComponent<TextMeshProUGUI>();
    }

    public void SetInfo(Character character)
    {
        artwork.sprite = character.characterSprite;
        name.text = character.characterName;
    }


}
