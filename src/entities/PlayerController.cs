using Microsoft.Xna.Framework.Input;
using mono2.src.mapping;
using Microsoft.Xna.Framework;
using mono2.src.entities.player;
using System;
using mono2.src.models;

namespace mono2.src.entities
{
  public class PlayerController {
    public Vector2 getMove(TileMap map, Player player, KeyboardState keyboardState) {
      int newX = player.tile.X;
      int newY = player.tile.Y;
      
      if (keyboardState.IsKeyDown(player.GetKey("up"))) {
        if (map.canWalk(newX, newY - player.speed)) {
          newY -= player.speed;
        }
      }
      if (keyboardState.IsKeyDown(player.GetKey("down"))) {
        if (map.canWalk(newX, newY + player.speed)) {
          newY += player.speed;
        }
      }
      if (keyboardState.IsKeyDown(player.GetKey("right"))) {
        if (map.canWalk(newX + player.speed, newY)) {
          newX += player.speed;
        }
      }
      if (keyboardState.IsKeyDown(player.GetKey("left"))) {
        if (map.canWalk(newX - player.speed, newY)) {
          newX -= player.speed;
        }
      }

      int battleIndex = map.entities.FindIndex(e => e.tile.X == newX && e.tile.Y == newY && e.type != EntityType.Player);
      if (battleIndex != -1) {
        map.removeEntity(battleIndex);
      }

      return new Vector2(newX, newY);
    }
  }
}