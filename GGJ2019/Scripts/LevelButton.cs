using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ2019 {
	[RequireComponent(typeof(Button))]
	public class LevelButton : MonoBehaviour {
		[SerializeField]
		private string _sceneName;
		[SerializeField]
		private ScreenFader _fader;

		[UsedImplicitly]
		private void Awake() {
			GetComponent<Button>().onClick.AddListener(Play);
		}

		private void Play() {
			_fader.GoToScene(_sceneName);
		}
	}
}
