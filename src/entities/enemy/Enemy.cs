using mono2.src.models.mapping;
using mono2.src.models;
using Microsoft.Xna.Framework;

namespace mono2.src.entities.enemy 
{
  public class Enemy : Entity {
    public Enemy(EntityType enemyType, int spawnPosX, int spawnPosY, Color? color = null) {
      this.type = enemyType;
      this.name = $"{enemyType}1".ToLower(); // NOTE 1 could be used to create variants of the same monster
      this.health = 10;
      this.tile = new Tile(this.name, spawnPosX, spawnPosY, TileType.Entity, color != null ? (Color)color : Color.White);
      this.speed = 1;
    }
  }
}