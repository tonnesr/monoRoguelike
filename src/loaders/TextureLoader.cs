using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace mono2.src.loading 
{
  public class TextureLoader {
    private ContentManager contentManager;
    private Dictionary<string, Texture2D> textures;
    private string location;

    public TextureLoader(ContentManager contentManager, string location, string[] texturesToLoad) {
      this.contentManager = contentManager;
      this.location = location;
      textures = loadTextures(texturesToLoad);
    }

    private Dictionary<string, Texture2D> loadTextures(string[] texturesToLoad) {
      Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

      foreach (string texture in texturesToLoad) {
        textures.Add(texture, this.contentManager.Load<Texture2D>($"textures\\{location}\\{texture}"));
      }

      return textures;
    }

    public Texture2D getTexture(string textureName) {
      return this.textures[textureName];
    }
  }
}