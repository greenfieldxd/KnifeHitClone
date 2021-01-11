using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Knife : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float targetPosition;
    [SerializeField] private float durationFly;
    [SerializeField] private GameObject _effectKnife;
    [SerializeField] private AudioClip _knifeSound;
    [SerializeField] private AudioClip _circleSound;
    [Space]
    [SerializeField] private KnifeSetup _knifeSetup;
    

    private PolygonCollider2D _collider2D;
        
    private Tween _moveTween;

    private void Start()
    {
        _collider2D = GetComponent<PolygonCollider2D>();

        GetCurrentKnifeType();
    }

    public void Launch()
    {
        _moveTween = transform.DOMoveY(targetPosition, durationFly).SetEase(Ease.InOutCubic);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Knife"))
        {
            Instantiate(_effectKnife);
            Vibration.Vibrate(30);
            SoundManager.PlaySound(_knifeSound);

            transform.DORotate(new Vector3(0, 0, 360) * 3, 1f, RotateMode.FastBeyond360);
            transform.DOMoveY(-8, 0.5f).SetEase(Ease.InSine);
            transform.DOMoveX(Random.Range(-4, -5), 0.5f).SetEase(Ease.InSine).OnComplete((() => 
                {
                    FindObjectOfType<GameManager>().LoseGame();
                    Destroy(gameObject);
                }));
        }
        else if (other.gameObject.CompareTag("Circle"))
        {
            SoundManager.PlaySound(_circleSound);
            Vibration.Vibrate(30);

            //Knife stop
            _moveTween.Pause();
            _moveTween.Kill();

            //set knife parent as circle and set knife trigger
            transform.SetParent(other.transform);
            _collider2D.isTrigger = true;
            
            //ShakeCircle\
            other.GetComponent<MovingCircle>().ShakeCircle();

            //Hit target
            FindObjectOfType<GameManager>().HitTarget();
            Destroy(this);
        }
    }

    private void GetCurrentKnifeType()
    {
        _spriteRenderer.sprite = _knifeSetup.GetKnifeSprite();
    }

    
    
}
    
    