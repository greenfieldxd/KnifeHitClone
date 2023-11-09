using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Source.Scripts.Extensions;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Knife : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _spriteRenderer2;
    [SerializeField] private Sprite spriteDeath;
    [SerializeField] private GameObject _effectDeathKnife;
    [SerializeField] private AudioClip sound;
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
            transform.position = Vector3.Lerp(transform.position, target.position + Vector3.up, speed * Time.deltaTime);
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
        if (!_canCollision) return;
        
        if (other.gameObject.CompareTag("Knife"))
        {
            _canCollision = false;
            _moving = false;
            _rotating = false;
            transform.SetParent(null);
            
            FindObjectOfType<GameManager>().LoseGame();

            _collider2D.enabled = false;
            Destroy(_collider2D);
            
            _spriteRenderer2.sprite = spriteDeath;
            var tr = transform;
            OtherExtensions.TransformPunchScale(tr, 0.2f, 0.2f, 1);
            
            if (SoundManager.Instance != null) SoundManager.Instance.PlaySound(sound);

            transform.DORotate(new Vector3(0, 0, Random.Range(0, 360f)) , 0.5f, RotateMode.FastBeyond360);
            transform.DOJump(new Vector3(Random.Range(-4, 4), -8, 0), 3f, 1, 0.5f).SetEase(Ease.InSine);
            Destroy(gameObject, 0.65f);
        }
        else if (other.gameObject.CompareTag("Circle"))
        {
            _canCollision = false;
            _moving = false;
            _rotating = false;

            var tr = transform;
            OtherExtensions.TransformPunchScale(tr, -0.2f, 0.2f, 1);
            
            _spriteRenderer2.sprite = spriteDeath;
            var effects = Instantiate(_effectDeathKnife, transform.position, Quaternion.identity).GetComponentsInChildren<ParticleSystem>();

            foreach (var effect in effects)
            {
                var main = effect.main;
                main.startColor = _knifeSetup.GetEffectColor();
            }

            if (SoundManager.Instance != null) SoundManager.Instance.PlaySound(sound);
            
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
    
    