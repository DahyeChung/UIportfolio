using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 캐릭터 분장실 
public enum CharacterPartType
{
    Weapon,
    Face,
    Body,
    Head,
    Tail
}

[System.Serializable]
public class PartCategory
{
    public CharacterPartType type;
    public GameObject parentObject;
    [HideInInspector] public List<GameObject> partInstances = new List<GameObject>();
}

public class CharacterCustomSelection : MonoBehaviour
{

    // 외형 데이터 ==============================================================
    [Header("색상 변경 대상")]
    [SerializeField] private SkinnedMeshRenderer mainBodyRenderer;
    [SerializeField] private Color defaultColor;

    [Header("파츠 목록")]
    // [SerializeField] private List<PartCategory> partCategories;

    // 내부 시스템 변수 ==========================================================
    private PlayerCharacter playerCharacter;
    private Dictionary<CharacterPartType, PartCategory> partsDictionary;
    private Material mainBodyMaterial;

    private void Awake()
    {
        playerCharacter = GetComponent<PlayerCharacter>();
        if (playerCharacter == null)
        {
            Debug.LogError("CharacterCustomSelection: 같은 게임 오브젝트에 PlayerCharacter 스크립트가 없습니다");
            return; // 없으면 초기화를 진행할 수 없으므로 중단
        }

        InitializeParts();
        InitializeMaterial();
    }

    private void Start()
    {
        if (mainBodyMaterial != null)
        {
            UpdateColor(defaultColor);
        }
    }
    private void RegisterPartCategory(CharacterPartType type, GameObject parentObject)
    {
        // PlayerCharacter에 parentObject가 설정되어 있지 않으면 건너뜁니다.
        if (parentObject == null) return;

        // PartCategory 객체를 코드로 직접 생성
        PartCategory category = new PartCategory
        {
            type = type,
            parentObject = parentObject,
            partInstances = parentObject.transform
                                        .Cast<Transform>()
                                        .Select(t => t.gameObject)
                                        .ToList()
        };

        // Dictionary에 추가
        if (!partsDictionary.ContainsKey(type))
        {
            partsDictionary.Add(type, category);
        }
    }

    // 공개 API 외부클래스에서 호출 ==================================================
    public void ChangePart(CharacterPartType type, int index)
    {
        if (partsDictionary.TryGetValue(type, out PartCategory category))
        {
            for (int i = 0; i < category.partInstances.Count; i++)
            {
                category.partInstances[i].SetActive(false);
            }

            if (index >= 0 && index < category.partInstances.Count)
            {
                category.partInstances[index].SetActive(true);
            }
        }

    }
    public void UpdateColor(Color color)
    {
        if (mainBodyMaterial != null)
        {
            mainBodyMaterial.SetColor("_BaseColor", color);
        }
    }

    // 특정 타입의 파츠 개수를 물어보는 함수
    public int GetPartsCount(CharacterPartType partType)
    {
        if (partsDictionary.TryGetValue(partType, out PartCategory category))
            return category.partInstances.Count;
        return 0;
    }
    // 특정 타입의 파츠의 이름을 얻을 때 사용
    public string GetPartsName(CharacterPartType partType, int index)
    {
        if (partsDictionary.TryGetValue(partType, out PartCategory category))
        {
            if (index >= 0 && index < category.partInstances.Count)
            { return category.partInstances[index].name; }
        }
        return "N/A";
    }

    // 내부 구현 함수 ==============================================================
    private void InitializeParts()
    {
        partsDictionary = new Dictionary<CharacterPartType, PartCategory>();

        // 각 파츠 타입에 대해 PlayerCharacter에 있는 parentObject를 사용하여 직접 등록합니다.
        RegisterPartCategory(CharacterPartType.Weapon, playerCharacter.weaponParentObject);
        RegisterPartCategory(CharacterPartType.Face, playerCharacter.faceParentObject);
        RegisterPartCategory(CharacterPartType.Body, playerCharacter.bodyParentObject);
        RegisterPartCategory(CharacterPartType.Head, playerCharacter.headParentObject);
        RegisterPartCategory(CharacterPartType.Tail, playerCharacter.tailParentObject);


    }

    private void InitializeMaterial()
    {
        if (mainBodyRenderer != null)
        {
            mainBodyMaterial = mainBodyRenderer.material;
        }
    }

}


