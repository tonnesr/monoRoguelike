using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace mono2.src.loading 
{
  public class TextureLoader {
    private ContentManager contentManager;
    private Dictionary<string, Texture2D> textures;
    private string location;

    public TextureLoader(ContentManager contentManager, string location, string[] texturesToLoad) {
      this.location = location;
      this.contentManager = contentManager;
      this.textures = this.loadTextures(texturesToLoad);
    }

    private Dictionary<string, Texture2D> loadTextures(string[] texturesToLoad) {
      Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

      foreach (string textureName in texturesToLoad) {
        Texture2D texture = this.contentManager.Load<Texture2D>($"textures\\{this.location}\\{textureName}");
        textures.Add(textureName, texture);
      }

      return textures;
    }

    public Texture2D getTexture(string textureName) {
      return this.textures[textureName];
    }
  }
}