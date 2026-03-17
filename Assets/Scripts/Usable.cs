using UnityEngine;
using UnityEngine.Events;

public abstract class Usable : MonoBehaviour
{
    public Renderer OutlineObjectRenderer;
    public int OutlineMaterialIndex = 1;

    public Color SelectedColor = Color.yellow;
    public Color DefaultColor = Color.black;

    protected Material _outlineMaterial;

    public UnityEvent OnUsingFinished;
    
    void Start()
    {
        if(OutlineObjectRenderer == null) OutlineObjectRenderer = GetComponent<Renderer>();
        _outlineMaterial = OutlineObjectRenderer.materials[OutlineMaterialIndex];
    }

    public void SelectUsed()
    {
        _outlineMaterial.SetColor("_Color", SelectedColor);
    }

    public abstract void Use();

    public void UnselectUsed()
    {
        _outlineMaterial.SetColor("_Color", DefaultColor);
    }

}
