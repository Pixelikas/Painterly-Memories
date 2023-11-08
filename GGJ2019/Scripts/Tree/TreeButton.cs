using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GGJ2019.Tree
{
    [RequireComponent(typeof(Collider2D))]
    public class TreeButton : MonoBehaviour
    {
        public string scene;
        public ScreenFader fader;

        private Transform _transform;
        private Vector3 _initialScale;

        private void Awake()
        {
            _transform = transform;
            _initialScale = _transform.localScale;
        }

        private void OnMouseEnter()
        {
            _transform.DOKill();
            _transform.localScale = _initialScale;
            _transform.DOPunchScale(Vector3.one * .25f, .25f);

            SoundManager.PlaySound("pop", false, 1f, Random.Range(.8f, 1.2f));
        }

        private void OnMouseDown()
        {
            StartCoroutine(LoadGame());
            SoundManager.PlaySound("woosh");
        }

        private IEnumerator LoadGame()
        {
            yield return fader.GoToScene(scene);
        }
    }
}