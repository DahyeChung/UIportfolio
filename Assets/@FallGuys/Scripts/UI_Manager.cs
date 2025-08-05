using System.Collections.Generic;
using UnityEngine;

public class UI_Manager
{
    private int _order = 10;
    private Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();

    // root 만들기: 게임오브젝트 타입으로 모아두기 위해
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = new GameObject { name = name };
        T sceneUI = go.AddComponent<T>();
        go.transform.SetParent(Root.transform);

        return sceneUI;
    }
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {

        foreach (UI_Popup p in _popupStack)
        {
            // 스택에 이미 같은 타입의 팝업이 있다면 함수를 그냥 종료
            if (p.GetType() == typeof(T))
            {
                return null;
            }
        }

        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }


        // 팝업 UI 생성 및 스택에 추가
        T popup = Object.Instantiate(Resources.Load<T>($"Prefabs/UI/{typeof(T).Name}"));

        if (popup == null)
        {
            Debug.LogError($"Failed to load popup UI: {typeof(T).Name}");
            return null;
        }

        // Canvas 컴포넌트가 프리팹 최상단에 있어야 합니다.
        Canvas popupCanvas = popup.GetComponent<Canvas>();
        if (popupCanvas != null)
        {
            popupCanvas.overrideSorting = true;
            popupCanvas.sortingOrder = _order++;
        }
        else
        {
            Debug.LogWarning($"'{typeof(T).Name}' 프리팹에 Canvas 컴포넌트가 없습니다. Sorting Order를 설정할 수 없습니다.");
        }

        _popupStack.Push(popup);

        popup.transform.SetParent(Root.transform);

        return popup;
    }
    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() == popup)
        {
            _popupStack.Pop();
            Object.Destroy(popup.gameObject);
            _order--;
        }
        else
        {
            Debug.LogWarning("Close Popup Failed");
        }
    }

    public void ClosePopupUI()
    {

    }
    public void CloseAllPopupUI()
    {
        // 모든 팝업 UI 닫기
        while (_popupStack.Count > 0)
        {
            UI_Popup popup = _popupStack.Pop();
            Object.Destroy(popup.gameObject);
            _order--;
        }
    }
}


/* UI Manager: UI 팝업 스택 관리 및 기능 제공 
 * UI Scene: 모든 인터렉션 가능한 UI 컴포넌트 요소를 관리 및 업데이트
 * UI Popup: 을 상속받는 개별 클래스
 */