using JetBrains.Annotations;
using UnityEngine;

namespace GGJ2019.Bees {
	public class Swatter : MonoBehaviour {
		[SerializeField]
		private string _leftSwatter;
		[SerializeField]
		private string _rightSwatter;
		[SerializeField]
		private string _upSwatter;

		private Animator _animator;

		[UsedImplicitly]
		private void Awake() {
			_animator = GetComponent<Animator>();
		}

		[UsedImplicitly]
		private void Update() {
			_animator.SetBool(_leftSwatter, Input.GetAxis("Horizontal") < -0.05f);
			_animator.SetBool(_rightSwatter, Input.GetAxis("Horizontal") > 0.05f);
			_animator.SetBool(_upSwatter, Input.GetAxis("Vertical") > 0.05f);
		}
	}
}
