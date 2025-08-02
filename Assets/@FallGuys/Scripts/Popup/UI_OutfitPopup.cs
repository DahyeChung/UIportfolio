public class UI_OutfitPopup : UI_Popup
{

    private void OnEnable()
    {
        PopupAnimation(ContentsObject);
    }

    public override void Init()
    {



    }
    // Esc 키 눌렀을 때 팝업 스택에서 제거
}
