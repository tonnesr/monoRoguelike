using Microsoft.Xna.Framework;
using mono2.src.models;
using mono2.src.entities;
using System;
using System.Collections.Generic;
using mono2.src.entities.player;
using mono2.src.entities.enemy;
using Microsoft.Xna.Framework.Graphics;
using mono2.src.loading;
using Microsoft.Xna.Framework.Content;
using System.Linq;

// TODO move map generation to another class
// TODO better map generation (random levels and entities)

namespace mono2.src.mapping
{
  public class TileMap {
    private int tileDiameter;
    public Vector2 size;
    public Tile[,] map;
    private Color color;
    public List<Entity> entities;
    private TextureLoader entityTextures;
    private TextureLoader tileTextures;

    public TileMap(int tileDiameter, Vector2 size, ContentManager contentManager, Color color, List<Entity> players) {
      this.tileDiameter = tileDiameter;
      this.size = size;
      this.color = color;

      this.map = this.generateTileMap();

      this.entityTextures = new TextureLoader(contentManager, "entities", new string[] { "playerOne", "playerTwo", "monster1" });
      this.tileTextures = new TextureLoader(contentManager, "tiles", new string[] { "wall1", "floor1" });
      
      this.entities = players.Concat(this.populateEntities(this.map)).ToList();
    }

    private Tile[,] generateTileMap() {
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
        }
      }
      return _map;
    }

    private List<Entity> populateEntities(Tile[,] map) {
      List<Entity> entities = new List<Entity>();

      Random random = new Random();
      foreach(Tile tile in map) {
        if (random.Next(0, 100) < 1 && tile.movement == TileMovementType.Walkable) {
          entities.Add(new Enemy(EntityType.Monster, tile.X, tile.Y));
        }
      }
      return entities;
    }

    public void drawMap(SpriteBatch spriteBatch) {
      foreach (Tile tile in this.map) {
        spriteBatch.Draw(this.tileTextures.getTexture(tile.symbol), new Vector2(tile.X * this.tileDiameter, tile.Y * this.tileDiameter), tile.color);
      }
    }

    public void drawEntities(SpriteBatch spriteBatch) {
      foreach (Entity entity in this.entities) {
        spriteBatch.Draw(this.entityTextures.getTexture(entity.tile.symbol), new Vector2(entity.tile.X * this.tileDiameter, entity.tile.Y * this.tileDiameter), entity.color);
      }
    }

    public void addEntity(Entity entity) {
      this.entities.Add(entity);
    }
    public void addEntity(Entity[] entities) {
      this.entities.AddRange(entities);
    }

    public void removeEntity(int index) {
      this.entities.RemoveAt(index);
    }
    public void removeEntity(Entity entity) {
      this.entities.Remove(entity);
    }
    
    public bool canWalk(int x, int y) {
      try {
        Tile tile = this.map[x, y];
        if (tile.movement == TileMovementType.Impassable || x < 0 || y < 0 || x > this.size.X || y > this.size.Y) {
          return false;
        }
        return true;
      } catch (Exception e) {
        Console.WriteLine(e);
        return false;
      }
    }
  }
}