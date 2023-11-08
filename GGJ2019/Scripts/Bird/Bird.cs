using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GGJ2019.Bird
{
    public class Bird : Spawnable
    {
        public float lifetime;

        private float time;

        private Transform _transform;
        private Animator _animator;
        private Vector2 _idlePos;

        private bool flyingOut = false;

        private void Start()
        {
            _transform = transform;
            _animator = GetComponent<Animator>();

            _idlePos = _transform.position;

            _animator.SetBool("flying", true);
            _transform.position += Vector3.up * 20f;
            _transform.DOMove(_idlePos, 2f).OnComplete(() =>
            {
                _animator.SetBool("flying", false);
            });

            time = lifetime;
        }

        private void Update()
        {
            if (time < 0f && !flyingOut)
            {
                Invoke("CallGameOver", 1f);
                FlyOut();
            }

            time -= Time.deltaTime;
        }

        public void FlyOut()
        {
            GameManager.instance.FreePos(posID);

            flyingOut = true;

            _animator.SetBool("flying", true);
            _transform.DOMove(_transform.position + (Vector3.up * 20f), 2f).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }

        private void CallGameOver()
        {
            GameManager.instance.GameOver();
        }
    }
}