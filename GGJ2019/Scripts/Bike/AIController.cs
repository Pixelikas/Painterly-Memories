using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2019.Bike
{
    [RequireComponent(typeof(Movable))]
    public class AIController : MonoBehaviour
    {
        public Transform target;

        private Transform _transform;
        private Movable _movable;

        private void Awake()
        {
            _transform = transform;
            _movable = GetComponent<Movable>();
        }

        private void FixedUpdate()
        {
            Vector2 axis = Vector2.zero;

            Vector3 dir = (target.position - _transform.position).normalized;
            _transform.up = dir;

            //float newX = Mathf.Lerp(_transform.position.x, target.position.x, Time.deltaTime * 5f);
            //_transform.position = new Vector3(newX, _transform.position.y, 0f);

            //if (angle < 0f)
            //    axis.x = -1f;
            //else if (angle > 0f)
            //    axis.x = 1f;

            if (Vector2.Distance(_transform.position, target.position) > 1f)
                axis.y = 1f;

            _movable.Move(axis);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                GameManager.instance.GameOver();
            }
        }
    }
}