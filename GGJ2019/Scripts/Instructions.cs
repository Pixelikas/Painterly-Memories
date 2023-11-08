using JetBrains.Annotations;
using UnityEngine;

namespace GGJ2019 {
	public class Instructions : MonoBehaviour {
		private bool _stopped;

		[UsedImplicitly]
		private void Update() {
			if (!_stopped) {
				Time.timeScale = 0;
				_stopped = true;
				return;
			}

			if (Input.anyKeyDown) {
				Time.timeScale = 1;
			}
		}
	}
}
