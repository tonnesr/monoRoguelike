using Microsoft.Xna.Framework;
using mono2.src.models;

namespace mono2.src.mapping 
{
  public class Tile {
    public string symbol;
    public int X;
    public int Y;
    public Color color;
    public TileType type;
    public TileMovementType movement;

    public Tile(string symbol, int x, int y, TileType type, TileMovementType movement = TileMovementType.Walkable, Color? color = null) {
      this.symbol = symbol;
      this.X = x;
      this.Y = y;
      this.type = type;
      this.movement = movement;
      this.color = color == null ? Color.White : (Color)color;
    }
  }
}