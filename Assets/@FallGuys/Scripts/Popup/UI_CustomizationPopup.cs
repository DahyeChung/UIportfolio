using DG.Tweening;
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
    [SerializeField]
    private GameObject characterObject;

    private UI_OutfitPopup _outfitPopup;



    private void OnEnable()
    {
        PopupAnimation(ContentsObject);
        outfitButton.onClick.AddListener(OnClickOutfitButton);
        CharacterFX();
    }
    private void OnDisable()
    {
        outfitButton.onClick.RemoveListener(OnClickOutfitButton);
        CharacterOFF();
    }

    public override void Init()
    {
        // _outfitPopup = Managers.UI.ShowPopupUI<UI_OutfitPopup>();
        characterObject = GameObject.FindWithTag("Player");


    }

    // 어딘가에 this.SetActive true 해야하는데

    void OnClickOutfitButton()
    {
        ContentsObject.SetActive(false);
        Managers.UI.ShowPopupUI<UI_OutfitPopup>(); // root 에 생성 
    }


    void CharacterFX()
    {
        characterObject.transform.DOMoveX(-4f, 0.5f).SetEase(Ease.OutBounce); ;
    }
    void CharacterOFF()
    {
        characterObject.transform.DOMoveX(0f, 0.5f).SetEase(Ease.OutBounce); ;

    }



}
