using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceOrange : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(DestroySliceOrange), 1f);
    }

    private void DestroySliceOrange()
    {
        Destroy(gameObject);
    }

}