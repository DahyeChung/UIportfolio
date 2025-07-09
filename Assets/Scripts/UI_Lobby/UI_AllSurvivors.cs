using UnityEngine;
using UnityEngine.UI;


public class UI_AllSurvivors : MonoBehaviour
{
    public GameObject contentScreen;
    private Button allSurvivorBtn;

    void Start()
    {
        allSurvivorBtn = GetComponentInChildren<Button>();

        if (allSurvivorBtn != null)
        {
            // assign function
            allSurvivorBtn.onClick.AddListener(ToggleCharacterMenu);
        }

        // Default
        contentScreen.SetActive(false);

    }
    private void OnEnable()
    {
        EventManager.OnScreenOpened += OnAllSurvivorsOpened;
        EventManager.OnScreenClosed += OnAllSurvivorsClosed;
    }
    private void OnDisable()
    {
        allSurvivorBtn.onClick.RemoveAllListeners();
        EventManager.OnScreenOpened -= OnAllSurvivorsOpened;
        EventManager.OnScreenClosed -= OnAllSurvivorsClosed;
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
    void OnAllSurvivorsClosed(GameObject screen)
    {

    }
    void OnAllSurvivorsOpened(GameObject screen)
    {
        if (screen != contentScreen)
        {
            ToggleCharacterMenu();
        }


    }
}
