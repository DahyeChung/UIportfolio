using UnityEngine;
using UnityEngine.UI;

public class UI_CustomizationPopup : UI_Popup
{
    [SerializeField]
    private Button outfitButton;
    [SerializeField]
    private Button theatricsButton;
    [SerializeField]
    private Button interfaceButton;


    private UI_OutfitPopup _outfitPopup;



    private void OnEnable()
    {
        PopupAnimation(ContentsObject);
        outfitButton.onClick.AddListener(OnClickOutfitButton);
    }
    private void OnDisable()
    {
        outfitButton.onClick.RemoveListener(OnClickOutfitButton);
    }

    public override void Init()
    {
        // _outfitPopup = Managers.UI.ShowPopupUI<UI_OutfitPopup>();


    }

    // 어딘가에 this.SetActive true 해야하는데

    void OnClickOutfitButton()
    {
        ContentsObject.SetActive(false);
        Managers.UI.ShowPopupUI<UI_OutfitPopup>(); // root 에 생성 
    }


}
