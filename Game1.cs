using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using mono2.src.mapping;
using mono2.src.loading;
using mono2.src.entities.player;
using System;
using mono2.src.entities;

namespace mono2
{
  public class Game1 : Game {
    private int tileDiameter = 16;
    private Vector2 mapSize = new Vector2(32, 32);
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private TileMap map;
    private TextureLoader tileTextures;
    private TextureLoader entityTextures;

    public Game1() {
      graphics = new GraphicsDeviceManager(this);
      graphics.PreferredBackBufferWidth = (int)mapSize.X * tileDiameter;
      graphics.PreferredBackBufferHeight = (int)mapSize.Y * tileDiameter;
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    protected override void Initialize() {
      map = new TileMap(mapSize, Color.Gray);

      base.Initialize();
    }

    protected override void LoadContent() {
      spriteBatch = new SpriteBatch(GraphicsDevice);

      tileTextures = new TextureLoader(this.Content, "tiles", new string[] { "floor1", "wall1" }); // TODO Move to map's entities ???
      entityTextures = new TextureLoader(this.Content, "entities", new string[] { "playerOne", "playerTwo", "monster1" }); // TODO Move to map's entities ???
    }

    protected override void Update(GameTime gameTime) {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) { // TODO gamepad support inside of player class
        Exit();
      }

      foreach (Entity entity in map.entities) {
        if (entity is Player) {
          entity.update(Keyboard.GetState());
        } else {
          entity.update();
        } 
      }

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.Black);
      spriteBatch.Begin();
      for (int i = 0; i < map.size.X; i++) {
        for (int j = 0; j < map.size.Y; j++) {
          Tile currentTile = map.map[i, j];
          spriteBatch.Draw(tileTextures.getTexture(currentTile.symbol), new Vector2(currentTile.X * tileDiameter, currentTile.Y * tileDiameter), currentTile.color);
        }
      }
      foreach (Entity entity in map.entities) {
        spriteBatch.Draw(entityTextures.getTexture(entity.tile.symbol), new Vector2(entity.tile.X * tileDiameter, entity.tile.Y * tileDiameter), entity.color);
      }
      spriteBatch.End();
      base.Draw(gameTime);
    }
  }
}
