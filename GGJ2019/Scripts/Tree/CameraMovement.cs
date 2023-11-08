using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2019.Tree
{
    public class CameraMovement : MonoBehaviour
    {
        public float scrollSpeed;
        public float lowerTolerance, upperTolerance;
        public float minY, maxY;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            if (!PlayerPrefs.HasKey("STAGE"))
                PlayerPrefs.SetInt("STAGE", 0);

            int stage = PlayerPrefs.GetInt("STAGE");
            maxY = 10f * stage;
        }

        private void Update()
        {
            float newY = _transform.position.y;

            if (Input.mousePosition.y > (Screen.height * upperTolerance))
                newY += scrollSpeed * Time.deltaTime;
            else if (Input.mousePosition.y < (Screen.height * lowerTolerance))
                newY -= scrollSpeed * Time.deltaTime;

            newY = Mathf.Clamp(newY, minY, maxY);
            _transform.position = new Vector3(_transform.position.x, newY, _transform.position.z);
        }
    }
}