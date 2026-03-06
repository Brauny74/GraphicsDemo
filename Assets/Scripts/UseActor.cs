using System.Collections;
using UnityEngine;

public class UseActor : MonoBehaviour
{
    public Camera ViewCamera;
    public float UseDistance = 2.0f;
    private Usable currentUsable;


    PlayerController pc;
    Ray ray;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pc = GetComponent<PlayerController>();
        if (ViewCamera == null)
            ViewCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(ViewCamera.transform.position, ViewCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, UseDistance, LayerMask.GetMask("Usable")))
        {
            currentUsable = hit.transform.gameObject.GetComponent<Usable>();
            if (currentUsable != null)
            {
                SelectUsable();
            }
        }
        else
        {
            if (currentUsable != null)
            {
                UnselectUsable();
            }
        }
    }

    void SelectUsable()
    {
        currentUsable.SelectUsed();
        currentUsable.OnUsingFinished.AddListener(FinishUse);
    }

    void UnselectUsable()
    {
        currentUsable.UnselectUsed();
        currentUsable.OnUsingFinished.RemoveListener(FinishUse);
        currentUsable = null;
    }

    public void Use()
    {
        if(currentUsable != null && (pc != null && pc.IsActive))
        {
            currentUsable.Use();
            pc.IsActive = false;
            UnselectUsable();
        }
    }

    public void FinishUse()
    {
        StartCoroutine("ReactivatePlayer");
    }

    private IEnumerator ReactivatePlayer()
    {
        yield return new WaitForSeconds(0.1f);
        pc.IsActive = true;
    }
}
