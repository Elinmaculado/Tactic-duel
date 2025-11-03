using Fusion;
using UnityEngine;

public class UnitColorizer : NetworkBehaviour
{
    void Start()
    {
        if (!Object.HasStateAuthority) return;

        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = Object.InputAuthority == Runner.LocalPlayer ? Color.blue : Color.red;
        }
    }
}