using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class Smash_PlayerSlot : MonoBehaviour
{
    private Image _artwork;
    private TextMeshProUGUI _name;

    private void Awake()
    {
        _artwork = transform.Find("artwork").GetComponent<Image>();
        _name = transform.Find("name").GetComponent<TextMeshProUGUI>();
    }

    public void SetInfo(Character character)
    {
        _artwork.sprite = character.characterSprite;
        _name.text = character.characterName;

        // Color fx
        Color c = _artwork.color;
        c.a = 1f;
        _artwork.color = c;

        // Animation
        _artwork.transform.DOPunchPosition(Vector3.down * 3, .3f, 10, 1);


    }

    public void HoverEffect(Character character)
    {
        _artwork.sprite = character.characterSprite;
        _name.text = character.characterName;

        // Color fx
        if (_artwork != null)
        {
            Color c = _artwork.color;
            c.a = 0.5f; // 약간 투명하게 (0.0 ~ 1.0)
            _artwork.color = c;
        }
        // Animation
        Sequence s = DOTween.Sequence();
        s.Append(_artwork.transform.DOLocalMoveX(-300, .05f).SetEase(Ease.OutCubic));
        s.AppendCallback(() => _artwork.GetComponent<Image>().sprite = _artwork.sprite);
        s.Append(_artwork.transform.DOLocalMoveX(300, 0));
        s.Append(_artwork.transform.DOLocalMoveX(0, .05f).SetEase(Ease.OutCubic));
    }

}
