using System.Collections.Generic;
using UnityEngine;

// 캐릭터 파츠의 종류를 정의합니다.
public enum CharacterPartType
{
    Weapon,
    Eye,
    Mouth,
    Nose,
    Body,
    Head,
    Tail
}

// 각 파츠 카테고리별로 필요한 데이터를 정의합니다.
// 이제 모든 파츠는 메시 리스트를 직접 가집니다.
[System.Serializable]
public class PartCategory
{
    [Tooltip("이 카테고리의 파츠 타입입니다.")]
    public CharacterPartType type;

    [Tooltip("이 파츠의 메시가 적용될 렌더러를 포함한 부모 오브젝트입니다.")]
    public GameObject parentObject;

    [Tooltip("교체할 수 있는 메시(3D 모델)의 목록입니다.")]
    public List<Mesh> partMeshes = new List<Mesh>();
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
        // 게임 시작 시 모든 파츠를 기본(첫 번째) 메시로 설정
        foreach (PartCategory category in partCategories)
        {
            ChangePart(category.type, 0);
        }

        // 기본 색상 적용
        if (mainBodyMaterial != null)
        {
            UpdateColor(defaultColor);
        }
    }

    // 공개 API (외부 클래스에서 호출) ==================================================

    public void ChangePart(CharacterPartType type, int index)
    {
        if (partsDictionary.TryGetValue(type, out PartCategory category))
        {
            SkinnedMeshRenderer partMeshRenderer = category.parentObject.GetComponentInChildren<SkinnedMeshRenderer>();

            if (partMeshRenderer == null)
            {
                Debug.LogWarning($"{type} 파츠의 SkinnedMeshRenderer를 찾을 수 없습니다.");
                return;
            }

            // 인덱스가 유효한지 확인하고 메시를 교체합니다.
            if (index >= 0 && index < category.partMeshes.Count)
            {
                partMeshRenderer.sharedMesh = category.partMeshes[index];
                partMeshRenderer.gameObject.SetActive(true); // 렌더러가 꺼져있을 수 있으니 켭니다.
            }
            else
            {
                // 유효하지 않은 인덱스(예: -1)는 파츠를 숨기는 것으로 간주합니다.
                partMeshRenderer.sharedMesh = null;
                partMeshRenderer.gameObject.SetActive(false);
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
            Debug.Log(category.partMeshes.Count);
            return category.partMeshes.Count;
        }
        return 0;
    }


    public string GetPartsName(CharacterPartType partType, int index)
    {
        if (partsDictionary.TryGetValue(partType, out PartCategory category))
        {
            if (index >= 0 && index < category.partMeshes.Count && category.partMeshes[index] != null)
            {
                return category.partMeshes[index].name;
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