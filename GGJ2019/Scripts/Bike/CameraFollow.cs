using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2019.Bike
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform _transform;

        public Transform target;
        public float smoothness;
        public float offset;

        private void Start()
        {
            _transform = transform;
        }

        private void FixedUpdate()
        {
            if (!target)
                return;

            Vector3 newPos = new Vector3(0f, target.position.y + offset, -10f);
            _transform.position = Vector3.Lerp(_transform.position, newPos, Time.deltaTime * smoothness);
        }
    }
}