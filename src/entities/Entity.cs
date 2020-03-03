using mono2.src.mapping;
using mono2.src.models;
using Microsoft.Xna.Framework;

namespace  mono2.src.entities 
{
  public abstract class Entity {
    public Tile tile;
    public string name;
    public int health;

    public abstract void move(Direction direction);
  }
}