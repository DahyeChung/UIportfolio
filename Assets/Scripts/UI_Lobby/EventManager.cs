using System;

public static class EventManager
{
    public static Action<TabScreen> OnTabOpened;
    public static Action<TabScreen> OnTabClosed;

}
