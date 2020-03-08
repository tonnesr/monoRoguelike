using Microsoft.Xna.Framework;

namespace mono2.src.models.mapping 
{
  public class Tile {
    public string symbol;
    public int X;
    public int Y;
    public Color color;
    public TileType type;
    public TileMovementType movement;

    public Tile(string symbol, int x, int y, TileType type, Color? color = null, TileMovementType movement = TileMovementType.Walkable) {
      this.symbol = symbol;
      this.X = x;
      this.Y = y;
      this.type = type;
      this.movement = movement;
      this.color = color == null ? Color.White : (Color)color;
    }
  }
}