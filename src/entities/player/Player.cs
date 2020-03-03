using mono2.src.mapping;
using mono2.src.models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using mono2.src.loading;
using System.Linq;
using System;

// TODO Store tiles in XML, or something?
// FIXME the error comes from the content loader, (string array of names). 

namespace mono2.src.entities.player 
{
  public class Player : Entity {
    public PlayerIndex playerIndex;
    public Dictionary<string, Keys> keyMapping; // Load from file?
    
    public Player(PlayerIndex playerIndex, int spawnPosX, int spawnPosY) {
      this.playerIndex = playerIndex;
      this.health = 100;
      this.name = $"player{playerIndex}";
      this.tile = new Tile(this.name, spawnPosX, spawnPosY, TileType.Entity);

      this.keyMapping = getKeyMapping(this.playerIndex);
    }

    private Dictionary<string, Keys> getKeyMapping(PlayerIndex playerIndex) { // TODO should just directly load based on the current player index instead.
      // TODO make dynamic, and move? or?
      Dictionary<string, string> keyMapping = new DataLoader().loadJsonFile<Dictionary<string, string>>("E:\\code\\mono2\\Content\\data\\KeyMapping.json");
      Dictionary<string, Keys> keys = keyMapping.ToDictionary(item => item.Key, item => (Keys)Enum.Parse(typeof(Keys), item.Value));
      return keys;
    }

    public override void move(Direction direction) {
      switch(direction) {
        case Direction.Up:
          this.tile.Y -= 1;
          break;
        case Direction.Down:
          this.tile.Y += 1;
          break;
        case Direction.Right:
          this.tile.X += 1;
          break;
        case Direction.Left:
          this.tile.X -= 1;
          break;
        default:
          break;
      }
    }
  }
}