using Microsoft.Xna.Framework;
using mono2.src.models;

namespace mono2.src.mapping 
{
  public class TileMap {
    public Vector2 size;
    public Tile[,] map;
    public Color color;

    public TileMap(Vector2 size, Color color) {
      this.size = size;
      this.color = color;
      this.map = generateTileMap();
    }

    private Tile[,] generateTileMap() {
      Tile[,] _map = new Tile[(int)this.size.X, (int)this.size.Y];
      for (int x = 0; x < (int)this.size.X; x++) {
        for (int y = 0; y < (int)this.size.Y; y++) {
          Tile newTile = new Tile("floor1", x, y, TileType.Floor, TileMovementType.Walkable, color);
          if ((x == 0 || x == (int)this.size.X - 1) || (y == 0 || y == (int)this.size.Y - 1)) {
            newTile.symbol = "wall1";
            newTile.type = TileType.Wall;
            newTile.movement = TileMovementType.Impassable;
          }
          _map[x, y] = newTile;
        }
      }
      return _map;
    }

    private Rectangle[] generateRectangles() {
      return new Rectangle[] { new Rectangle() }; // TODO https://docs.microsoft.com/en-us/previous-versions/windows/silverlight/dotnet-windows-silverlight/bb198628(v%3Dxnagamestudio.35)
    }

    public bool canMoveTo(int newXpos, int newYpos) {
      if (this.map[newXpos, newYpos].movement == TileMovementType.Walkable) {
        return true;
      } else if (this.map[newXpos, newYpos].movement == TileMovementType.Swimmable) {
        return true; // TODO swim logic
      }
      return false;
    }

    public bool isWalkable(int x, int y) {
      return this.map[x, y].movement == TileMovementType.Walkable || this.map[x, y].movement == TileMovementType.Swimmable;
    }
  }
}