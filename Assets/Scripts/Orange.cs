using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Orange : MonoBehaviour
{
    [SerializeField] private Transform _parentOgange;
    private void Start()
    {
        _parentOgange.DORotate(new Vector3(0, 0, Random.Range(0, 361)), 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<GameManager>().AddScore(2);
        transform.DOScale(Vector3.zero, 0.2f);
        
        Invoke(nameof(DestroyOrange), 0.2f);
    }

    private void DestroyOrange()
    {
        Destroy(gameObject);
    }
}
