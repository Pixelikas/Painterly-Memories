using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ2019 {
	public class Cheats : MonoBehaviour {
		private const string UnlockLevelCheat = "obradinn";

		private int _index;

		[UsedImplicitly]
		private void OnGUI() {
			if (_index >= UnlockLevelCheat.Length) {
				_index = 0;
				StartCoroutine(Unlock());
				return;
			}

			if (Event.current.type != EventType.KeyDown) {
				return;
			}

			if (Event.current.character == UnlockLevelCheat[_index]) {
				_index++;
			}
		}

		private IEnumerator Unlock() {
			PlayerPrefs.SetInt("STAGE", 3);
			var scene = SceneManager.GetActiveScene().name;
			yield return SceneManager.LoadSceneAsync(scene);
		}
	}
}
