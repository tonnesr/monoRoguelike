namespace mono2.src.models.mapping 
{
  public struct Size {
    public int width { get; }
    public int height { get; }
    
    public Size(int width, int height) {
      this.width = width;
      this.height = height;
    }
  }
}