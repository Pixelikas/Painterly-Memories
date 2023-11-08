using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GGJ2019.Bike
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movable : MonoBehaviour
    {
        public float handling;
        public float accel;
        public float drag;
        public float speed;
        public float maxSpeed;

        private Transform _transform;
        private Transform _sprite;
        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
            _sprite = _transform.Find("Sprite");
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _transform.up * speed;
            speed *= drag;
        }

        public void Move(Vector2 axis)
        {
            speed += axis.y * accel * Time.fixedDeltaTime;
            speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

            float handle = -axis.x * handling * Time.fixedDeltaTime;
            _rigidbody.rotation += handle;

            if (Mathf.Abs(axis.x) > 0f)
                speed *= drag;
        }

        public void Jump()
        {
            _transform.DOKill();
            _transform.localScale = Vector3.one;

            speed = maxSpeed;

            _sprite.DOLocalMoveY(.25f, .35f).SetEase(Ease.OutSine);
            _sprite.DOScale(1.5f, .35f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                _sprite.DOLocalMoveY(0f, .35f).SetEase(Ease.InSine);
                _sprite.DOScale(1f, .35f).SetEase(Ease.InSine);
            });

            SoundManager.PlaySound("woosh", false, 1f, Random.Range(.8f, 1.2f));
        }
    }
}