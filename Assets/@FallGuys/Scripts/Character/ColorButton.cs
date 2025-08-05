using UnityEngine;

public class ColorButton : MonoBehaviour
{
    public Color myColor;


    public void OnClickColorButton()
    {
        CharacterCustomSelection customManager = (CharacterCustomSelection)FindFirstObjectByType(typeof(CharacterCustomSelection));

        customManager.UpdateColor(myColor);
    }

}
