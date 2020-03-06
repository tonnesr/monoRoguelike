using mono2.src.mapping;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace  mono2.src.entities 
{
  public abstract class Entity {
    public Tile tile;
    public string name;
    public int health;
    public Color color;

    public abstract void update();
    public abstract void update(KeyboardState keyboardState);
  }
}