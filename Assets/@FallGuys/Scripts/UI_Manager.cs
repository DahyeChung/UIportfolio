using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    private int _order = 10;
    private Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();



}


/* UI Manager: UI 팝업 스택 관리 및 기능 제공 
 * UI Scene: 모든 인터렉션 가능한 UI 컴포넌트 요소를 관리 및 업데이트
 * UI Popup: 을 상속받는 개별 클래스
 */