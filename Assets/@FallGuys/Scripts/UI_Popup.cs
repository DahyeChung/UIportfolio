using DG.Tweening;
using UnityEngine;

public class UI_Popup : MonoBehaviour
{
    [SerializeField]
    protected GameObject ContentsObject;

    protected void Awake()
    {
        Init();
    }


    public virtual void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI(this);
    }

    public virtual void Init()
    {
        if (ContentsObject == null)
        {
            Transform found = transform.Find("ContentsObject");

            if (found != null)
            {
                ContentsObject = found.gameObject;
            }
            else
            {
                Debug.LogError($"Cannot find 'ContentsObject' in '{this.gameObject.name}' Prefab");
            }
        }

    }



    public void PopupAnimation(GameObject popup)
    {

        popup.transform.localScale = new Vector3(0.8f, 0.8f, 1);
        popup.transform.DOScale(1f, 0.1f).SetEase(Ease.InOutBack).SetUpdate(true);
        // play one shot 으로 resources/SFX/PopupOpen.wav 재생
        //  Managers.Sound.Play("SFX/PopupOpen", 0.5f, false, true);
    }

}
