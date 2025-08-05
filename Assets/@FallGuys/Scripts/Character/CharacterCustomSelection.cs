using UnityEngine;
using UnityEngine.UI;

public class CharacterCustomSelection : MonoBehaviour
{
    // 배열로 버튼을 가진다음에
    // 클릭되었을 시 해당 버튼의 인덱스를 사용해서 
    // changePart 함수 적용 

    [SerializeField] private Button[] allWeaponButtons;
    private PlayerCharacter playerCharacter;

    private void Awake()
    {
        // 게임 시작 시 PlayerCharacter를 한 번만 찾아 변수에 저장합니다.
        playerCharacter = FindFirstObjectByType<PlayerCharacter>();
    }

    private void Start()
    {
        for (int i = 0; i < allWeaponButtons.Length; i++)
        {
            int buttonIndex = i;

            // 각 버튼의 onClick 이벤트에 OnClickWeaponButton 함수를 연결합니다.
            // 버튼이 클릭될 때마다 해당 버튼의 인덱스(buttonIndex)를 인자로 전달합니다.
            allWeaponButtons[i].onClick.AddListener(() => OnClickWeaponButton(buttonIndex));
        }
    }

    public void OnClickWeaponButton(int buttonIndex)
    {
        // playerCharacter가 제대로 찾아졌는지 확인합니다.
        if (playerCharacter == null)
        {
            Debug.LogError("PlayerCharacter를 찾을 수 없습니다!");
            return;
        }

        playerCharacter.ChangePart(CharacterPartType.Weapon, buttonIndex);
    }
}


