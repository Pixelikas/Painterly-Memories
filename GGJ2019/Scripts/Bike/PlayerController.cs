using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GGJ2019.Bike
{
    [RequireComponent(typeof(Movable))]
    public class PlayerController : MonoBehaviour
    {
        public float winY;

        private Transform _transform;
        private Movable _bike;
        private Animator _animator;

        private void Awake()
        {
            _transform = transform;
            _bike = GetComponent<Movable>();
            _animator = _transform.Find("Sprite").GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            Vector2 axis = Vector2.zero;

            axis.x = Input.GetAxis("Horizontal");
            axis.y = Input.GetAxis("Vertical");

            _bike.Move(axis);

            _animator.speed = _bike.speed / _bike.maxSpeed;

            if (_transform.position.y > winY)
                GameManager.instance.Success();
        }
    }
}