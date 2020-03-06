using mono2.src.mapping;
using Microsoft.Xna.Framework.Input;
using mono2.src.models;
using System;
using Microsoft.Xna.Framework;

namespace mono2.src.entities.enemy 
{
  public class Enemy : Entity {
    public Enemy(EnemyType enemyType, int spawnPosX, int spawnPosY, Color? color = null) {
      this.name = $"{enemyType}1".ToLower(); // NOTE 1 could be used to create variants of the same monster
      this.health = 10;
      this.tile = new Tile(this.name, spawnPosX, spawnPosY, TileType.Entity);
      this.color = color != null ? (Color)color : Color.White;
    }

    public override void update(KeyboardState keyboardState) {}
    public override void update() {
      /*Random random = new Random();

      switch (random.Next(0, 100)) {
        case 0:
          this.tile.X += 1;
          break;
        case 1:
          this.tile.Y += 1;
          break;
        case 2:
          this.tile.X -= 1;
          break;
        case 3:
          this.tile.Y -= 1;
          break;
      }*/
    }
  }
}