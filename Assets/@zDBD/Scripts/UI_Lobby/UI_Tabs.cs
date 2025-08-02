using UnityEngine;
using UnityEngine.UI;

public class UI_Tabs : MonoBehaviour
{
    protected TabScreen tabScreen;
    protected Button tabButton;

    protected void Start()
    {
        tabButton = GetComponentInChildren<Button>();
        tabScreen = GetComponentInChildren<TabScreen>();

        if (tabButton != null)
        {
            tabButton.onClick.AddListener(OpenTab);
        }

        if (tabScreen != null)
        {
            tabScreen.CloseTab();
        }
    }


    private void OnEnable()
    {
        EventManager.OnTabOpened += OnTabOpened;
        // EventManager.OnTabClosed += OnTabClosed;
    }
    private void OnDisable()
    {
        tabButton.onClick.RemoveAllListeners();
        EventManager.OnTabOpened -= OnTabOpened;
        // EventManager.OnTabClosed -= OnTabClosed;
    }

    void OpenTab()
    {
        // if character screen is already open, close it 
        // otherwise open it
        //if (tabScreen.isTabOpened == true)
        //{
        //    tabScreen.CloseTab();

        //}
        //else
        //{
        //    tabScreen.OpenTab();

        //}
        // TODO: add sfx, animation
        tabScreen.OpenTab();

    }
    void OnTabClosed(TabScreen screen)
    {

    }
    void OnTabOpened(TabScreen screen)
    {
        if (screen != tabScreen)
        {
            tabScreen.CloseTab();
        }


    }
}
