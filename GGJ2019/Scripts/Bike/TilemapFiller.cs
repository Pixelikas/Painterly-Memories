using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GGJ2019.Bike {
	[RequireComponent(typeof(Tilemap))]
	public class TilemapFiller : MonoBehaviour {
		[SerializeField]
		private TileBase _fillTile;

		[UsedImplicitly]
		private void Awake() {
			var tilemap = GetComponent<Tilemap>();

			foreach (var cell in tilemap.cellBounds.allPositionsWithin) {
				if (tilemap.GetTile(cell) == null) {
					tilemap.SetTile(cell, _fillTile);
				}
			}
		}
	}
}
