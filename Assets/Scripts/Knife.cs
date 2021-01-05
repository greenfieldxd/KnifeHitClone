using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Knife : MonoBehaviour
{
    [SerializeField] private float targetPosition;
    [SerializeField] private float durationFly;
    [SerializeField] private Vector3 _startScale;

    private PolygonCollider2D _collider2D;
        
    private Tween _moveTween;

    private void Start()
    {
        _collider2D = GetComponent<PolygonCollider2D>();
        transform.DOScale(_startScale, 0.1f).SetEase(Ease.InSine);
    }

    public void Launch()
    {
        _moveTween = transform.DOMoveY(targetPosition, durationFly).SetEase(Ease.InOutCubic);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Knife"))
        {
            transform.DOMoveY(-8, 0.5f).SetEase(Ease.InSine);
            transform.DOMoveX(Random.Range(-4, -5), 0.5f).SetEase(Ease.InSine).OnComplete((() => FindObjectOfType<GameManager>().LoseGame()));
            transform.DORotate(new Vector3(0, 0, 360) * 3, 1f, RotateMode.FastBeyond360);
            
            Vibration.Vibrate(30);
        }

        if (other.gameObject.CompareTag("Circle"))
        {
            //Knife stop
            _moveTween.Pause();
            _moveTween.Kill();

            //set knife parent as circle and set knife trigger
            transform.SetParent(other.transform);
            _collider2D.isTrigger = true;
            
            //ShakeCircle and vibrate
            other.GetComponent<MovingCircle>().ShakeCircle();
            Vibration.Vibrate(30);

            //Hit target
            FindObjectOfType<GameManager>().HitTarget();
            enabled = false;
        }
    }

    public void ClearGame()
    {
        Destroy(gameObject);
    }
    
    
    
    
}
    
    