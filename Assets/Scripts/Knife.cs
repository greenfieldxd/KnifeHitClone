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
    [SerializeField] private SpriteRenderer _spriteRenderer2;
    [SerializeField] private GameObject _effectKnife;
    [SerializeField] private AudioClip _knifeSound;
    [SerializeField] private AudioClip _circleSound;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotateSpeed = 25;
    [Space]
    [SerializeField] private KnifeSetup _knifeSetup;
    

    private Collider2D _collider2D;
    private Rigidbody2D _rigidbody2D;
        
    private bool _moving;
    private bool _rotating;
    private bool _canCollision;

    private void Start()
    {
        _canCollision = true;
        
        GetCurrentKnifeType();
        gameObject.AddComponent<PolygonCollider2D>();
        
        _collider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_rotating)
        {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (_moving)
        {
            var target = GameManager.Instance.ActiveCircle.transform;
            transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
        }
    }


    public void Launch()
    {
        var rand = Random.Range(300, 1000);
        var value = Random.value;
        rotateSpeed = value > 0.5f ? rand : - rand;
        
        _moving = true;
        _rotating = true;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        _canCollision = false;
        _moving = false;
        _rotating = false;
        
        if (other.gameObject.CompareTag("Knife"))
        {
            FindObjectOfType<GameManager>().LoseGame();

            _collider2D.enabled = false;
            Destroy(_collider2D);

            Instantiate(_effectKnife);
            if (SoundManager.Instance != null) SoundManager.Instance.PlaySound(_knifeSound);

            transform.DORotate(new Vector3(0, 0, 360) * 1, 0.5f, RotateMode.FastBeyond360);
            transform.DOMoveY(-8, 0.5f).SetEase(Ease.InSine);
            transform.DOMoveX(Random.Range(-4, -4), 0.6f).SetEase(Ease.InSine).OnComplete(() => Destroy(gameObject));
        }
        else if (other.gameObject.CompareTag("Circle"))
        {
            if (SoundManager.Instance != null) SoundManager.Instance.PlaySound(_circleSound);
            
            //set knife parent as circle and set knife trigger
            transform.SetParent(other.transform);
            _collider2D.isTrigger = true;
            Destroy(_rigidbody2D);
            
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
        _spriteRenderer2.sprite = _knifeSetup.GetKnifeSprite2();
    }
}
    
    