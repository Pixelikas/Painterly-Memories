using System.Collections;
using UnityEngine;

namespace GGJ2019.Bees {
	public class BeeGameManager : MonoBehaviour {
		[SerializeField]
		private int _beeKillsToWin;
		[SerializeField]
		private ScreenFader _fader;

		private int _beesKilled;
		private YieldInstruction _ongoingResult;

		public bool CanSpawnBee => _beesKilled < _beeKillsToWin;

		public void BeeKilled() {
			_beesKilled++;
			if (_beesKilled == _beeKillsToWin && _ongoingResult == null) {
				_ongoingResult = StartCoroutine(Win());
			}
		}

		public void HealthDepleted() {
			if (_ongoingResult == null) {
				_ongoingResult = StartCoroutine(Lose());
			}
		}

		private IEnumerator Win() {
            if (PlayerPrefs.GetInt("STAGE") < 2)
                PlayerPrefs.SetInt("STAGE", 2);

            SoundManager.PlaySound("win");
			yield return null;
			yield return _fader.GoToMenu();
		}

		private IEnumerator Lose() {
			SoundManager.PlaySound("lose");
			_fader.Color = Color.black;
			yield return _fader.GoToMenu();
		}
	}
}
