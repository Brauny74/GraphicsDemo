using UnityEngine;
using UnityEngine.Events;

public abstract class Usable : MonoBehaviour
{
    public SkinnedMeshRenderer OutlinedObjectRenderer;
    public int OutlineMaterialIndex = 1;

    public Color SelectedColor = Color.yellow;
    public Color DefaultColor = Color.black;

    private Material _outlineMaterial;

    public UnityEvent OnUsingFinished;
    
    void Start()
    {
        if(OutlinedObjectRenderer == null) OutlinedObjectRenderer = GetComponent<SkinnedMeshRenderer>();
        _outlineMaterial = OutlinedObjectRenderer.materials[OutlineMaterialIndex];
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
