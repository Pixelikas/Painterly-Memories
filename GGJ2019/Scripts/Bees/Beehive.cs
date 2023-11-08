using JetBrains.Annotations;
using UnityEngine;

namespace GGJ2019.Bees {
	public class Beehive : MonoBehaviour {

		[SerializeField]
		private float _verticalVariance;
		[SerializeField]
		private float _horizontalVariance;
		[SerializeField]
		private float _period;
		[SerializeField]
		private float _periodDeviation;
		[SerializeField]
		private float _initialDelay;
		[SerializeField]
		private GameObject _bee;
		[SerializeField]
		private BeeGameManager _manager;

		private float _nextSpawn;

		[UsedImplicitly]
		private void Awake() {
			_nextSpawn = Time.time + _initialDelay + Random.Range(0, _periodDeviation * 2);
		}

		[UsedImplicitly]
		private void Update() {
			if (_nextSpawn > Time.time || !_manager.CanSpawnBee) {
				return;
			}

			_nextSpawn = Time.time + _period + Random.Range(0, _periodDeviation);

			var position = transform.position
				+ Vector3.right * Random.Range(-_horizontalVariance, _horizontalVariance)
				+ Vector3.up * Random.Range(-_verticalVariance, _verticalVariance);

			var go = Instantiate(_bee, position, Quaternion.identity);
			go.SetActive(true);
		}

		[UsedImplicitly]
		private void OnDrawGizmos() {
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireCube(transform.position, new Vector3(
				_horizontalVariance * 2,
				_verticalVariance * 2
			));
		}
	}
}
