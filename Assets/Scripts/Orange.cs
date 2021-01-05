using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Orange : MonoBehaviour
{
    [SerializeField] private Transform _parentOgange;
    [SerializeField] private GameObject sliceOrangePrefab;

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager gm = FindObjectOfType<GameManager>();
        gm.AddScore(1);
        gm.AddOrange(2);

        DestroyOrange();
    }

    private void DestroyOrange()
    {
        Instantiate(sliceOrangePrefab);
        Destroy(_parentOgange.gameObject);    }
    
    
}
