using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

namespace GGJ2019.Bird
{
    [RequireComponent(typeof(CharacterController2D))]
    public class PlayerGrabber : MonoBehaviour
    {
        public Transform grabbedObj;
        public Vector2 offset;

        public GameObject torsoIdle, torsoCarry;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            GetComponent<CharacterController2D>().onTriggerEnterEvent += TriggerEnter2D;
        }

        private void Update()
        {
        }

        private void TriggerEnter2D(Collider2D col)
        {
            if (col.name.Contains("Worm"))
            {
                if (grabbedObj)
                    return;

                SoundManager.PlaySound("pop");

                torsoCarry.SetActive(true);
                torsoIdle.SetActive(false);

                grabbedObj = col.transform;
                grabbedObj.parent = torsoCarry.transform;
                grabbedObj.localPosition = new Vector3(-0.2f, -0.75f, 0f);
                col.GetComponent<Worm>().Grab();
            }

            if (col.name.Contains("Bird"))
            {
                if (!grabbedObj)
                    return;

                SoundManager.PlaySound("pop", false, 1f, 1.5f);

                torsoCarry.SetActive(false);
                torsoIdle.SetActive(true);

                grabbedObj.parent = col.transform;
                grabbedObj.position = col.transform.position + new Vector3(.3f, -0.05f, 0f);
                grabbedObj.rotation = Quaternion.Euler(0f, 0f, 90f);

                grabbedObj = null;

                col.transform.GetComponent<Bird>().FlyOut();

                GameManager.instance.FeedBird();
            }
        }
    }
}