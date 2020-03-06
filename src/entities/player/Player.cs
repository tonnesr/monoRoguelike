using mono2.src.mapping;
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
    public Dictionary<string, Keys> keys;
    
    public Player(PlayerIndex playerIndex, int spawnPosX, int spawnPosY, Color? color = null) {
      this.playerIndex = playerIndex;
      this.health = 100; // TODO
      this.name = $"player{playerIndex}";
      this.tile = new Tile(this.name, spawnPosX, spawnPosY, TileType.Entity);
      this.color = color != null ? (Color)color : Color.White;
      this.keys = getKeys(this.playerIndex);
    }

    private Dictionary<string, Keys> getKeys(PlayerIndex playerIndex) {      
      Dictionary<string, Keys> _keys = new DataLoader("Content\\data\\")
        .loadJsonFile<Dictionary<string, string>>("KeyMapping.json")
        .Where(item => item.Key.Contains($"p{(int)playerIndex}"))
        .ToDictionary(item => item.Key, item => (Keys)Enum.Parse(typeof(Keys), item.Value));
      return _keys;
    }

    public override void update() {}
    public override void update(KeyboardState keyboardState) {
      string propertyPrefix = $"p{(int)playerIndex}";
      if (keyboardState.IsKeyDown(this.keys[$"{propertyPrefix}_up"])) {
        this.tile.Y -= 1;
      }
      if (keyboardState.IsKeyDown(this.keys[$"{propertyPrefix}_down"])) {
        this.tile.Y += 1;
      }
      if (keyboardState.IsKeyDown(this.keys[$"{propertyPrefix}_right"])) {
        this.tile.X += 1;
      }
      if (keyboardState.IsKeyDown(this.keys[$"{propertyPrefix}_left"])) {
        this.tile.X -= 1;
      }
    }
  }
}