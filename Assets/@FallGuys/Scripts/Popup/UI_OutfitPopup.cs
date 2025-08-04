using UnityEngine;

public class UI_OutfitPopup : UI_Popup
{
    private UI_CustomizationPopup _customizationPopup;






    private void OnEnable()
    {
        PopupAnimation(ContentsObject);

    }
    private void OnDisable()
    {

    }


    public override void Init()
    {
        _customizationPopup = FindObjectOfType<UI_CustomizationPopup>();

        // 질문

    }
    private void Update()
    {   // event?
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ClosePopupUI();
            _customizationPopup.ContentsObject.SetActive(true);
        }
    }

    void OnClickButton()
    {

    }


}
