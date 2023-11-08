using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GGJ2019.Bird
{
    public class Worm : Spawnable
    {
        public float lifetime;

        private float time;
        private bool grabbed = false;
        private bool hiding = false;

        private Transform _transform;
        private Vector3 _idlePos;
        
        private void Start()
        {
            _transform = transform;

            transform.Find("Sprite").GetComponent<SpriteRenderer>()
                .sortingOrder = (transform.position.y < -2) ? 0 : -3;

            _idlePos = _transform.position;

            _transform.position += Vector3.down;
            _transform.DOMove(_idlePos, .5f);

            time = lifetime;
        }

        private void Update()
        {
            if (grabbed || hiding)
                return;

            if (time < 0f)
                Hide();

            time -= Time.deltaTime;
        }

        private void Hide()
        {
            GameManager.instance.FreePos(posID);

            hiding = true;
            _transform.DOMove(_transform.position + Vector3.down, .5f).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }

        public void Grab()
        {
            GameManager.instance.FreePos(posID);

            _transform.DOKill();

            grabbed = true;
            GetComponent<Collider2D>().enabled = false;
            transform.Find("Sprite").GetComponent<SpriteRenderer>().sortingOrder = 8;
        }
    }
}