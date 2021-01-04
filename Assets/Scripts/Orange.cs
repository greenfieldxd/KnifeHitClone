using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Orange : MonoBehaviour
{
    [SerializeField] private Transform _parentOgange;
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<GameManager>().AddScore(2);
        transform.DOScale(Vector3.zero, 0.2f);
        
        Invoke(nameof(DestroyOrange), 0.2f);
    }

    private void DestroyOrange()
    {
        Destroy(_parentOgange.gameObject);
    }
}
