using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace GGJ2019.Bees {
	public class BeePlayer : MonoBehaviour {
		[SerializeField]
		private int _health;

		[SerializeField]
		private TMP_Text _healthDisplay;
		
		[SerializeField]
		private BeeGameManager _manager;

		[UsedImplicitly]
		private void Awake() {
			_healthDisplay.text = _health.ToString();
		}

		[UsedImplicitly]
		private void OnCollisionEnter2D(Collision2D collision) {
			if (collision.otherCollider.gameObject != gameObject) {
				return;
			}

			SoundManager.PlaySound("ouch");

			_health--;
			_healthDisplay.text = _health.ToString();
			if (_health == 0) {
				_manager.HealthDepleted();
			}
		}
	}
}
