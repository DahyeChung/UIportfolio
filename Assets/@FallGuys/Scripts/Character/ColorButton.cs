using UnityEngine;

public class ColorButton : MonoBehaviour
{
    public Color myColor;


    public void OnClickColorButton()
    {
        PlayerCharacter playerCharacter = (PlayerCharacter)FindFirstObjectByType(typeof(PlayerCharacter));

        playerCharacter.UpdateColor(myColor);
    }

}
