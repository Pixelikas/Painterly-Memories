using UnityEngine;
using UnityEngine.EventSystems;

namespace GGJ2019 {
	public class ButtonFeedback : MonoBehaviour, IPointerDownHandler {
		[SerializeField]
		private AudioClip _click;

		public void OnPointerDown(PointerEventData eventData) {
			SoundManager.PlaySound(_click, pitch: Random.Range(0.9f, 1.1f));
		}
	}
}
