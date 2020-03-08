using mono2.src.models.mapping;
using mono2.src.models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using mono2.src.loading;
using System.Linq;
using System;

namespace mono2.src.entities.player
{
  public class Player : Entity {
    public PlayerIndex playerIndex;
    private Dictionary<string, Keys> keys;
    
    public Player(PlayerIndex playerIndex, int spawnPosX, int spawnPosY, Color? color = null) {
      this.type = EntityType.Player;
      this.playerIndex = playerIndex;
      this.health = 100; // TODO
      this.name = $"player{playerIndex}";
      this.tile = new Tile(this.name, spawnPosX, spawnPosY, TileType.Entity, color != null ? (Color)color : Color.White);
      this.speed = 1;
      this.keys = loadKeyBindings(this.playerIndex);
    }

    private Dictionary<string, Keys> loadKeyBindings(PlayerIndex playerIndex) {      
      Dictionary<string, Keys> _keys = new DataLoader("Content\\data\\")
        .loadJsonFile<Dictionary<string, string>>("KeyMapping.json")
        .Where(item => item.Key.Contains($"p{(int)playerIndex}"))
        .ToDictionary(item => item.Key, item => (Keys)Enum.Parse(typeof(Keys), item.Value));
      return _keys;
    }

    public Keys GetKey(string key) {
      return this.keys[$"p{(int)playerIndex}_{key}"];
    }
  }
}