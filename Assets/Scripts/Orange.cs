using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Source.Scripts.Systems.Game;
using UnityEngine;
using Random = UnityEngine.Random;

public class Orange : MonoBehaviour
{
    [SerializeField] private Transform _parentOgange;
    [SerializeField] private ParticleSystem effect;

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        AddResourceSystem.Instance.AnimateMoney(transform.position);
        GameManager.Instance.AddScore(1);

        DestroyOrange();
    }

    private void DestroyOrange()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(_parentOgange.gameObject);
    }  
}
