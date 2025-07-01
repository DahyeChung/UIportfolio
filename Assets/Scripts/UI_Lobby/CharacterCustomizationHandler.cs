using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizationHandler : MonoBehaviour
{
    private GameObject displayedCharacter;
    private DH_CharacterSelectionManager characterSelectionManager;


    [SerializeField]
    private List<CharacterBodyParts> characterEquipments;
    [SerializeField]
    private List<CharaterEyes> charaterEyes;

    // Class 캐릭터 셀렉션 매니저 안에 있는 
    // Character Display Parent 변수에 생성된 프리팹 모델을 가져와서 
    // 그 프리팹 안에 있는 Body Parts를 활성화 비활성화 한다

    private void Start()
    {
        displayedCharacter = characterSelectionManager.characterPrefab;
    }



    /*
     * Create a class to hold item game objects 
     * Name of the items in string for UI description
     * Icon of the items in sprite
     * Character prefeb Hold all item class in List 
     * Manually add the items icons and name for each List item classes
     * 
     * On Customization create enabled instantiate equipment cards (name, icon, object reference)
     * In scroll view content 
     * for each item of the list
     *
     *
     * Item for Green 
     * - item parts game object 
     * 
     * Character 
     * 
     *
     * TODO : Fill all the items in lists
     */


}
