using mono2.src.mapping;
using Microsoft.Xna.Framework;
using mono2.src.models;

namespace  mono2.src.entities 
{
  public abstract class Entity {
    public Tile tile;
    public string name;
    public int health;
    public Color color;
    public int speed;
    public EntityType type;

    public void setPos(Vector2 newPos) {
      this.tile.X = (int)newPos.X;
      this.tile.Y = (int)newPos.Y;
    }
  }
}