using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 1. 관리할 파츠의 종류를 Enum으로 정의
public enum CharacterPartType
{
    Weapon,
    Face,
    Body,
    Head,
    Tail
}

// 2. 인스펙터에서 파츠 종류와 부모 오브젝트를 함께 설정할 수 있는 클래스
[System.Serializable]
public class PartCategory
{
    public CharacterPartType type;
    public GameObject parentObject;
    [HideInInspector] public List<GameObject> partInstances = new List<GameObject>();
}

public class PlayerCharacter : MonoBehaviour
{
    [Header("메인 렌더러 및 색상")]
    [SerializeField] private SkinnedMeshRenderer mainBodyRenderer;
    [SerializeField] private Color defaultColor;

    [Header("캐릭터 파츠 목록")]
    // 3. 인스펙터에 노출될 파츠 카테고리 리스트
    [SerializeField] private List<PartCategory> partCategories;

    // 4. Enum을 Key로 사용하여 각 파츠 리스트에 빠르게 접근하기 위한 Dictionary
    private Dictionary<CharacterPartType, PartCategory> partsDictionary;
    private Material mainBodyMaterial;

    private void Awake()
    {
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


    private void InitializeParts()
    {
        partsDictionary = new Dictionary<CharacterPartType, PartCategory>();

        foreach (var category in partCategories)
        {
            if (!partsDictionary.ContainsKey(category.type))
            {
                partsDictionary.Add(category.type, category);
            }

            if (category.parentObject != null)
            {
                category.partInstances = category.parentObject
                                                 .GetComponentsInChildren<Transform>(true)
                                                 .Where(t => t.gameObject != category.parentObject)
                                                 .Select(t => t.gameObject)
                                                 .ToList();
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
        else
        {
            Debug.LogWarning($"{type} 타입의 파츠가 등록되어 있지 않습니다.");
        }
    }


    public void UpdateColor(Color color)
    {
        if (mainBodyMaterial != null)
        {
            mainBodyMaterial.SetColor("_BaseColor", color);
        }
    }
}