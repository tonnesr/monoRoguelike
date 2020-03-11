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
using mono2.src.models.mapping;

// TODO Better map generation (random levels and entities)
// TODO Scrollable maps

namespace mono2.src.mapping
{
  public class TileMap {
    private int tileDiameter;
    public Size size;
    public Tile[,] map;
    private Color color;
    public List<Entity> entities;
    private TextureLoader entityTextures;
    private TextureLoader tileTextures;

    public TileMap(int tileDiameter, Size size, ContentManager contentManager, Color color, List<Entity> players) {
      this.tileDiameter = tileDiameter;
      this.size = size;
      this.color = color;

      this.map = this.getMap();

      this.entityTextures = new TextureLoader(contentManager, "entities", new string[] { "playerOne", "playerTwo", "monster1" }); // TODO load larger textures based on tileDiameter, instead of spritebatch scale.
      this.tileTextures = new TextureLoader(contentManager, "tiles", new string[] { "wall1", "floor1", "corridor1", "empty1" }); // TODO load larger textures based on tileDiameter, instead of spritebatch scale.
      
      this.entities = players.Concat(this.populateEntities(this.map)).ToList();

      Console.WriteLine($"Tile count: {this.map.Length}, Entity count: {this.entities.Count}");
    }

    private Tile[,] getMap() {
      TileMapGenerator generator = new TileMapGenerator();
      Tile[,] _map = generator.generate(this.size);
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
        spriteBatch.Draw(this.entityTextures.getTexture(entity.tile.symbol), new Vector2(entity.tile.X * this.tileDiameter, entity.tile.Y * this.tileDiameter), entity.tile.color);
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
        if (tile.movement == TileMovementType.Impassable) {
          return false;
        }
        return true;
      } catch (Exception) {
        return false;
      }
    }
  }
}