using DG.Tweening;
using UnityEngine;

public class UI_OutfitPopup : UI_Popup
{
    private UI_CustomizationPopup _customizationPopup;

    [SerializeField]
    private GameObject characterObject;

    private void OnEnable()
    {
        PopupAnimation(ContentsObject);
        //CharacterFX();
    }
    private void OnDisable()
    {
        //CharacterOFF();
    }


    public override void Init()
    {
        _customizationPopup = FindObjectOfType<UI_CustomizationPopup>();

    }
    private void Update()
    {   // event?
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ClosePopupUI();
            _customizationPopup.ContentsObject.SetActive(true);
        }
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
