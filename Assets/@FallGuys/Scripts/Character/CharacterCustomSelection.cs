using System.Collections.Generic;
using UnityEngine;

// 캐릭터 파츠의 종류를 정의합니다. (변경 없음)
public enum CharacterPartType
{
    Weapon,
    Eye,
    Mouth,
    Body,
    Head,
    Tail
}

// 각 파츠 카테고리별로 필요한 데이터를 정의합니다.
[System.Serializable]
public class PartCategory
{
    [Tooltip("이 카테고리의 파츠 타입입니다.")]
    public CharacterPartType type;

    [Tooltip("하이어라키에 배치된 파츠 오브젝트들의 목록입니다.")]
    public List<GameObject> partInstances = new List<GameObject>();
}

public class CharacterCustomSelection : MonoBehaviour
{
    // 외형 데이터 ==============================================================
    [Header("색상 변경 대상")]
    [SerializeField]
    [Tooltip("캐릭터의 주 몸체 색상을 변경할 SkinnedMeshRenderer입니다.")]
    private SkinnedMeshRenderer mainBodyRenderer;

    [SerializeField]
    [Tooltip("캐릭터의 기본 색상입니다.")]
    private Color defaultColor;

    [Header("파츠 목록")]
    [Tooltip("여기에서 캐릭터의 모든 파츠 카테고리를 설정합니다.")]
    [SerializeField]
    private List<PartCategory> partCategories;

    // 내부 시스템 변수 ==========================================================
    private Dictionary<CharacterPartType, PartCategory> partsDictionary;
    private Material mainBodyMaterial;

    private void Awake()
    {
        InitializeParts();
        InitializeMaterial();
    }

    private void Start()
    {
        // 게임 시작 시 모든 파츠를 기본(첫 번째) 파츠로 설정하고 나머지는 비활성화
        foreach (PartCategory category in partCategories)
        {
            ChangePart(category.type, Random.Range(0, partCategories.Count));
        }

        // 기본 색상 적용
        if (mainBodyMaterial != null)
        {
            UpdateColor(defaultColor);
        }
    }

    // 공개 API (외부 클래스에서 호출) ==================================================

    /// <summary>
    /// 지정된 타입의 캐릭터 파츠를 교체합니다.
    /// </summary>
    /// <param name="type">교체할 파츠 타입</param>
    /// <param name="index">활성화할 파츠의 인덱스. 나머지는 모두 비활성화됩니다.</param>
    public void ChangePart(CharacterPartType type, int index)
    {
        if (!partsDictionary.TryGetValue(type, out PartCategory category)) return;

        // 해당 카테고리의 모든 파츠를 순회
        for (int i = 0; i < category.partInstances.Count; i++)
        {
            GameObject partObject = category.partInstances[i];
            if (partObject != null)
            {
                // 선택된 인덱스와 일치하는 파츠만 활성화하고 나머지는 모두 비활성화
                partObject.SetActive(i == index);
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

    public int GetPartsCount(CharacterPartType partType)
    {
        if (partsDictionary.TryGetValue(partType, out PartCategory category))
        {
            return category.partInstances.Count;
        }
        return 0;
    }

    public string GetPartsName(CharacterPartType partType, int index)
    {
        if (partsDictionary.TryGetValue(partType, out PartCategory category))
        {
            if (index >= 0 && index < category.partInstances.Count && category.partInstances[index] != null)
            {
                return category.partInstances[index].name;
            }
        }
        return "N/A";
    }

    private void InitializeParts()
    {
        partsDictionary = new Dictionary<CharacterPartType, PartCategory>();
        foreach (var category in partCategories)
        {
            if (!partsDictionary.ContainsKey(category.type))
            {
                partsDictionary.Add(category.type, category);
            }
        }
    }

    private void InitializeMaterial()
    {
        if (mainBodyRenderer != null)
        {
            mainBodyMaterial = mainBodyRenderer.material;
        }
    }
}