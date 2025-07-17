using UnityEngine;

public class UI_LobbyScene : MonoBehaviour
{
    // 각 enum 예시 목록
    private enum EnumList
    {
        GameObjects,
        Buttons,
        Texts,
        Images,
    }

    protected void Awake()
    {
        // Enum & 함수 바인딩

    }

    private void OnEnable()
    {
        // 이벤트 리스너 구독
    }
    private void OnDisable()
    {
        // 이벤트 리스너 구독 해제
    }

    public void SetInfo()
    {
        // 외부에서 정보 업데이트에 사용 
    }

    #region UI Event

    private void OnClickExampleButton()
    {
        // 팝업 UI 뜨게 하고 정보 업데이트 
        // UI_HeroPopup popup = Managers.UI.ShowPopupUI<UI_HeroPopup>();
        // popup.SetInfo();
    }

    #endregion
}
