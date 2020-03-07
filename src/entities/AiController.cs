using mono2.src.models;
using Microsoft.Xna.Framework;
using mono2.src.mapping;
using System;

namespace mono2.src.entities
{
  public class AiController {
    private EntityType type;
    private Random random;
    
    public AiController(EntityType type) {
      this.type = type;
      this.random = new Random();
    }

    public Vector2 getMove(TileMap map, Entity entity) {
      int newX = entity.tile.X;
      int newY = entity.tile.Y;

      switch(this.type) {
        case EntityType.Monster:
            int x;
            int y;
            switch (this.random.Next(0, 100)) {
              case 0:
                x = newX + entity.speed;
                if (map.canWalk(x, newY) && map.entities.FindIndex(e => e.tile.X == x && e.tile.Y == newY) == -1) { // Right
                  newX = x;
                }
                break;
              case 1:
                y = newY + entity.speed;
                if (map.canWalk(newX, y) && map.entities.FindIndex(e => e.tile.Y == y && e.tile.X == newX) == -1) { // Down
                  newY = y;
                }
                break;
              case 2:
                x = newX - entity.speed;
                if (map.canWalk(x, newY) && map.entities.FindIndex(e => e.tile.X == x && e.tile.Y == newY) == -1) { // Left
                  newX = x;
                }
                break;
              case 3:
                y = newY - entity.speed;
                if (map.canWalk(newX, y) && map.entities.FindIndex(e => e.tile.Y == y && e.tile.X == newX) == -1) { // Up
                  newY = y;
                }
                break;
            }
          break;
      }

      return new Vector2(newX, newY);
    }
  }
}