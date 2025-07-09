using UnityEngine;
using UnityEngine.UI;

public class CharacterMenuButton : MonoBehaviour
{

    public GameObject contentScreen;
    private Button characterMenuBtn;

    private void OnEnable()
    {
        EventManager.OnScreenOpened += OnMenuScreenOpened;
        EventManager.OnScreenClosed += OnMenuScreenClosed;
    }
    private void OnDisable()
    {
        characterMenuBtn.onClick.RemoveAllListeners();
        EventManager.OnScreenOpened -= OnMenuScreenOpened;
        EventManager.OnScreenClosed -= OnMenuScreenClosed;
    }
    void Start()
    {
        characterMenuBtn = GetComponent<Button>();
        if (characterMenuBtn != null)
        {
            // assign function
            characterMenuBtn.onClick.AddListener(ToggleCharacterMenu);
        }

        // Default
        contentScreen.SetActive(false);

    }

    void ToggleCharacterMenu()
    {
        // if character screen is already open, close it 
        // otherwise open it
        if (contentScreen.activeSelf == true)
        {
            contentScreen.SetActive(false);
            EventManager.OnScreenClosed?.Invoke(contentScreen);

        }
        else
        {
            contentScreen.SetActive(true);
            EventManager.OnScreenOpened?.Invoke(contentScreen);

        }
        // TODO: add sfx, animation

    }
    void OnMenuScreenClosed(GameObject screen)
    {

    }
    void OnMenuScreenOpened(GameObject screen)
    {
        if (screen != contentScreen)
        {
            ToggleCharacterMenu();
        }


    }
}
