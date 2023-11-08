using JetBrains.Annotations;
using UnityEngine;

namespace GGJ2019.Bees {
	[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
	public class Bee : MonoBehaviour {
		[SerializeField]
		private Transform _target;
		[SerializeField]
		private float _speed;
		[SerializeField]
		private float _drift;
		[SerializeField]
		private AudioClip[] _deathNotes;
		
		[SerializeField]
		private BeeGameManager _manager;

		private Rigidbody2D _rigidbody;

		[UsedImplicitly]
		private void Awake() {
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		[UsedImplicitly]
		private void Update() {
			var direction = (_target.position - transform.position).normalized;
			var orthogonal = Quaternion.Euler(0, 0, 90) * direction;

			var driftIntensity = Mathf.Sin(Time.time + transform.GetSiblingIndex());

			_rigidbody.velocity =
				direction * _speed
				+ driftIntensity * orthogonal * _drift;

			transform.localScale = Vector3.up + Vector3.left * Mathf.Sign(direction.x);
		}

		[UsedImplicitly]
		private void OnCollisionEnter2D() {
			transform.position = Vector3.down * 1000;
			SoundManager.PlaySound(
				_deathNotes[Random.Range(0, _deathNotes.Length - 1)],
				volume: 0.3f
			);
			Destroy(gameObject);
			_manager.BeeKilled();
		}
	}
}
