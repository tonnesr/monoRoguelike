﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using mono2.src.mapping;
using mono2.src.entities.player;
using mono2.src.entities;
using mono2.src.models;
using mono2.src.models.mapping;
using System.Linq;
using System.Collections.Generic;
using System;

namespace mono2
{
  public class Game1 : Game {
    // Screen size and game size should not be the same (larger scrolling map and menu)
    private int tileDiameter = 16;
    private Size mapSize = new Size(32, 32);
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private TileMap map;
    private PlayerController playerController;
    private AiController monsterController;
    private int framesSinceLastUpdate = 0;

    public Game1() {
      this.graphics = new GraphicsDeviceManager(this);
      this.graphics.PreferredBackBufferWidth = mapSize.width * this.tileDiameter;
      this.graphics.PreferredBackBufferHeight = mapSize.height * this.tileDiameter;
      this.Content.RootDirectory = "Content";
      this.IsMouseVisible = true;

      this.monsterController = new AiController(EntityType.Monster);
      this.playerController = new PlayerController();
    }

    protected override void Initialize() {
      map = new TileMap(this.tileDiameter, this.mapSize, this.Content, Color.Gray, new List<Entity>() { new Player(PlayerIndex.One, 1, 1, Color.OrangeRed), new Player(PlayerIndex.Two, 1, 1, Color.Teal) });
      
      base.Initialize();
    }

    protected override void LoadContent() {
      spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime) {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) { // TODO gamepad support inside of player class
        this.Exit();
      }
      if (framesSinceLastUpdate > 4) {
        foreach (Entity entity in this.map.entities.ToList()) {
          if (entity is Player) {
            entity.setPos(this.playerController.getMove(this.map, (Player)entity, Keyboard.GetState()));
          } else {
            entity.setPos(this.monsterController.getMove(this.map, entity));
          }
        }
        framesSinceLastUpdate = 0;          
      }
      framesSinceLastUpdate++;
      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
      this.GraphicsDevice.Clear(Color.Black);
      this.spriteBatch.Begin();
        this.map.drawMap(spriteBatch);
        this.map.drawEntities(spriteBatch);
      this.spriteBatch.End();
      base.Draw(gameTime);
    }
  }
}
