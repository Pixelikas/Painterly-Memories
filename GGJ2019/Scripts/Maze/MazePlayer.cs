using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GGJ2019.Maze {
	[RequireComponent(typeof(Rigidbody2D))]
	public class MazePlayer : MonoBehaviour {
		[SerializeField]
		private float _speed;
		[SerializeField]
		private float _acceleration;
		[SerializeField]
		private float _destroyFadeDuration;
		[SerializeField]
		private TileBase _finishLine;
		[SerializeField]
		private ScreenFader _fader;

		private readonly HashSet<Vector3Int> _removingPositions = new HashSet<Vector3Int>();
		private Rigidbody2D _rigidbody;
		private Animator _animator;
		private bool _finished;

		[UsedImplicitly]
		private void Awake() {
			_rigidbody = GetComponent<Rigidbody2D>();
			_animator = GetComponent<Animator>();

			_rigidbody.isKinematic = false;
			Invoke("Gambs", 1f);
		}

		[UsedImplicitly]
		private void Gambs() {
			_rigidbody.isKinematic = false;
		}

		[UsedImplicitly]
		private void OnEnable() {
			Shader.SetGlobalFloat("_PlayerPositionInfluence", 1f);
		}

		[UsedImplicitly]
		private void OnDisable() {
			Shader.SetGlobalFloat("_PlayerPositionInfluence", 0f);
		}

		[UsedImplicitly]
		private void FixedUpdate() {
			Shader.SetGlobalVector("_PlayerPosition", transform.position);
			
			var movement = new Vector2(
				Input.GetAxis("Horizontal"),
				Input.GetAxis("Vertical")
			);

			var desired = movement * _speed;
			var delta = desired - _rigidbody.velocity;
			
			_rigidbody.AddForce(delta * _acceleration);
			_animator.SetFloat("Movement", _rigidbody.velocity.magnitude);
			transform.rotation = Quaternion.FromToRotation(Vector3.up, movement);
		}

		[UsedImplicitly]
		private void OnCollisionStay2D(Collision2D collision) {
			var tilemap = collision.gameObject.GetComponent<Tilemap>();
			if (tilemap == null) {
				return;
			}

			foreach (var contact in collision.contacts) {
				var pointInCell = contact.point - 0.01f * contact.normal;

				var cell = tilemap.WorldToCell(pointInCell);
				var tile = tilemap.GetTile(cell);

				if (tile == _finishLine) {
					Finish();
					return;
				}

				if (tile is DestructibleTile && !_removingPositions.Contains(cell)) {
					StartCoroutine(Remove(tilemap, cell));
				}
			}
		}

		private IEnumerator Remove(Tilemap tilemap, Vector3Int cell) {
			SoundManager.PlaySound(
				"whack",
				pitch: Random.Range(0.7f, 1f),
				volume: 0.3f
			);
			_removingPositions.Add(cell);
			tilemap.SetColliderType(cell, Tile.ColliderType.None);
			yield return DOTween
				.ToAlpha(
					() => tilemap.GetColor(cell),
					color => tilemap.SetColor(cell, color),
					0,
					_destroyFadeDuration
				)
				.WaitForCompletion();
			tilemap.SetTile(cell, null);
			_removingPositions.Remove(cell);
		}

		private void Finish() {
			if (_finished) {
				return;
			}

            _finished = true;
			SoundManager.PlaySound("success");
			_fader.GoToMenu();
		}
	}
}
