using Microsoft.Xna.Framework;

namespace mono2.src.models.mapping
{
  public struct Corridor {
    public Vector2 start;
    public Vector2 end;
    
    public Corridor(Vector2 start, Vector2 end) {
      this.start = start;
      this.end = end;
    }
  }
}