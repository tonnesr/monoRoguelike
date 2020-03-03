using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using mono2.src.mapping;
using mono2.src.loading;
using mono2.src.models;
using mono2.src.entities.player;
using System.Collections.Generic;
using System;

namespace mono2
{
  public class Game1 : Game {
    private int tileDiameter = 16;
    private Vector2 mapSize = new Vector2(32, 32);
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private TileMap map;
    private TextureLoader tileTextures;
    private TextureLoader playerTextures;
    private Player playerOne;

    public Game1() {
      graphics = new GraphicsDeviceManager(this);
      graphics.PreferredBackBufferWidth = (int)mapSize.X * tileDiameter;
      graphics.PreferredBackBufferHeight = (int)mapSize.Y * tileDiameter;
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    protected override void Initialize() {
      map = new TileMap(mapSize, Color.Gray);
      playerOne = new Player(PlayerIndex.One, 1, 1); // TODO Spawn point handling

      //DataLoader dataLoader = new DataLoader();
      //Dictionary<string, string> json = dataLoader.loadJsonFile<Dictionary<string, string>>("E:\\code\\mono2\\Content\\data\\KeyMapping.json");

      base.Initialize();
    }

    protected override void LoadContent() {
      spriteBatch = new SpriteBatch(GraphicsDevice);

      tileTextures = new TextureLoader(this.Content, "tile", new string[] { "floor1", "wall1" });
      playerTextures = new TextureLoader(this.Content, "player", new string[] { "playerOne", "playerTwo" });
    }

    protected override void Update(GameTime gameTime) {
      // TODO create a generic input class? for players.
      // FIXME Having thousands of GetState is ineffective af.
      
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
        Exit();
      }

      if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W)) {
        playerOne.move(Direction.Up);
      }
      if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S)) {
        playerOne.move(Direction.Down);
      }
      if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D)) {
        playerOne.move(Direction.Right);
      }
      if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A)) {
        playerOne.move(Direction.Left);
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
      spriteBatch.Draw(playerTextures.getTexture(playerOne.tile.symbol), new Vector2(playerOne.tile.X * tileDiameter, playerOne.tile.Y * tileDiameter), Color.White);
      spriteBatch.End();
      base.Draw(gameTime);
    }
  }
}
