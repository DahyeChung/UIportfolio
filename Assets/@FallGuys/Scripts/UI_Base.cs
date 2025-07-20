using DG.Tweening;
using UnityEngine;

public class UI_Base : MonoBehaviour
{
    // 데이터 바인딩 



    public void PopupOpenAnimation(GameObject contentObject)
    {
        contentObject.transform.localScale = new Vector3(0.8f, 0.8f, 1);
        contentObject.transform.DOScale(1f, 0.1f).SetEase(Ease.InOutBack).SetUpdate(true);
    }
}
