using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character Selection/Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("Character Info")]
    public string characterName;

    [Tooltip("The main 3D model prefab for the character used in-game.")]
    public GameObject characterPrefab;

    [Tooltip("The icon to display in the character selection UI.")]
    public Sprite characterSprite;

    [Header("Base Stats")]
    public int characterLevel;
    public float maxHealth = 100f;
    public float baseDamage = 10f;
    public float moveSpeed = 5f;



    [Header("Description")]
    [TextArea(3, 10)]
    public string description;

    // We can even link to our future inventory system here!
    // [Header("Starting Inventory")]
    // public List<ItemData> startingItems;

}
