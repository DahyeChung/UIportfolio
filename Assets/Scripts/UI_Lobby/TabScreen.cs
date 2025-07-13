using UnityEngine;

public class TabScreen : MonoBehaviour
{

    public bool isTabOpened { private set; get; }

    private void Start()
    {
        isTabOpened = false;
        gameObject.SetActive(false);
    }

    public void OpenTab()
    {
        if (isTabOpened)
            return;

        isTabOpened = true;

        gameObject.SetActive(true);
        EventManager.OnTabOpened?.Invoke(this);
    }
    public void CloseTab()
    {
        if (!isTabOpened)
            return;

        isTabOpened = false;
        EventManager.OnTabClosed?.Invoke(this);
        gameObject.SetActive(false);
    }

}
