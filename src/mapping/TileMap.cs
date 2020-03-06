using Microsoft.Xna.Framework;
using mono2.src.models;
using mono2.src.entities;
using System;
using System.Collections.Generic;
using mono2.src.entities.player;
using mono2.src.entities.enemy;

// TODO move map generation to another class
// TODO better map generation (random levels and entities)

namespace mono2.src.mapping
{
  public class TileMap {
    public Vector2 size;
    public Tile[,] map;
    public Color color;
    public List<Entity> entities;

    public TileMap(Vector2 size, Color color, List<Entity> entities = null) {
      this.size = size;
      this.color = color;
      this.entities = entities != null ? entities : new List<Entity>();
      this.map = generateTileMap();
    }

    private Tile[,] generateTileMap() {
      addEntity(new Entity[] { new Player(PlayerIndex.One, 1, 1, Color.OrangeRed), new Player(PlayerIndex.Two, 1, 1, Color.Teal) });

      Random random = new Random();
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

          if (random.Next(0, 150) < 1 && newTile.movement != TileMovementType.Impassable) {
            addEntity(new Enemy(EnemyType.Monster, x, y));
          }
        }
      }
      return _map;
    }

    public void addEntity(Entity entity) {
      this.entities.Add(entity);
    }
    public void addEntity(Entity[] entities) {
      this.entities.AddRange(entities);
    }

    /*private Rectangle[] generateRectangles() {
      return new Rectangle[] { new Rectangle() }; // TODO https://docs.microsoft.com/en-us/previous-versions/windows/silverlight/dotnet-windows-silverlight/bb198628(v%3Dxnagamestudio.35)
    }*/

    /*public bool canMoveTo(int newXpos, int newYpos) {
      if (this.map[newXpos, newYpos].movement == TileMovementType.Walkable) {
        return true;
      }
      return false;
    }
    public bool canMoveTo(Vector2 newPos) {
      if (this.map[(int)newPos.X, (int)newPos.Y].movement == TileMovementType.Walkable) {
        return true;
      }
      return false;
    }*/

    /*public bool isWalkable(int x, int y) {
      return this.map[x, y].movement == TileMovementType.Walkable || this.map[x, y].movement == TileMovementType.Swimmable;
    }*/
  }
}