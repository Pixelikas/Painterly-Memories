using System;
using System.Collections;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GGJ2019 {
	public class ScreenFader : MonoBehaviour {
		[SerializeField]
		private float _fadeInDuration;
		[SerializeField]
		private float _fadeOutDuration;

		private Graphic _graphic;
		private CanvasGroup _group;
		private YieldInstruction _ongoingChange;

		public Color Color {
			get {
				if (_graphic == null) {
					Awake();
				}

				return _graphic.color;
			}
			set {
				if (_graphic == null) {
					Awake();
				}

				_graphic.color = value;
			}
		}

		[UsedImplicitly]
		public void GoToEvent(string sceneName) {
			if (string.IsNullOrWhiteSpace(sceneName)) {
				GoToMenu();
			} else {
				GoToScene(sceneName);
			}
		}

		public YieldInstruction GoToScene(string sceneName) {
			if (_ongoingChange != null) {
				throw new InvalidOperationException("Already fading to another scene");
			}

			gameObject.SetActive(true);
			_ongoingChange = StartCoroutine(GoToSceneRoutine(sceneName));
			return _ongoingChange;
		}

		public YieldInstruction GoToMenu() {
			return GoToScene("tree");
		}

		private IEnumerator GoToSceneRoutine(string sceneName) {
			DontDestroyOnLoad(gameObject);
			
			yield return _group
				.DOFade(1, _fadeInDuration)
				.SetUpdate(true)
				.WaitForCompletion();
			yield return SceneManager
				.LoadSceneAsync(sceneName);
			yield return _group
				.DOFade(0, _fadeOutDuration)
				.WaitForCompletion();

			_ongoingChange = null;
			Destroy(gameObject);
		}

		[UsedImplicitly]
		private void Awake() {
			_graphic = GetComponent<Graphic>();
			_group = GetComponent<CanvasGroup>();
		}
	}
}
