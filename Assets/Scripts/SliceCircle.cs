using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceCircle : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(DestroySliceCircle), 1f);
    }

    private void DestroySliceCircle()
    {
        Destroy(gameObject);
    }

}
