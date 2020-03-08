using Microsoft.Xna.Framework;

namespace mono2.src.models.mapping
{
  public struct Corridor {
    public Vector2 topPos;
    public Vector2 bottomPos;

    public Corridor(Vector2 topPos, Vector2 bottomPos) {
      this.topPos = topPos;
      this.bottomPos = bottomPos;
    }
  }
}