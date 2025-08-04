using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public SkinnedMeshRenderer m_RendererMainBody;

    [SerializeField]
    private Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        UpdateColor(defaultColor);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void UpdateColor(Color color)
    {
        m_RendererMainBody.material.SetColor("_BaseColor", color);
        Debug.Log("컬러 변경 to " + color);
    }


}
