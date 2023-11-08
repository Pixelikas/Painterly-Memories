using UnityEngine;
using UnityEngine.Tilemaps;

namespace GGJ2019.Maze {
	[CreateAssetMenu]
	public class DestructibleTile : TileBase {
		public Sprite Sprite;

		public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData) {
			tileData.sprite = Sprite;
			tileData.colliderType = Tile.ColliderType.Grid;
		}
	}
}
