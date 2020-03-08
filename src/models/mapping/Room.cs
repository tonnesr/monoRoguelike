using Microsoft.Xna.Framework;

namespace mono2.src.models.mapping {
  public class Room {
    public Vector2 topLeftPosition { get; }
    public Vector2 bottomRightPosition { get; }
    public Size size { get; }
    
    public Room(Vector2 pos, Size size) {
      this.topLeftPosition = pos;
      this.size = size;

      this.bottomRightPosition = new Vector2(pos.X + size.width, pos.Y + size.height);
    }

    public Vector2 getCenter() {
      return new Vector2((this.topLeftPosition.X + this.bottomRightPosition.X) / 2, (this.topLeftPosition.Y + this.bottomRightPosition.Y) / 2);
    }

    public bool isOverlapping(Room room) {
      return (
        this.topLeftPosition.X <= room.bottomRightPosition.X &&
        this.bottomRightPosition.X >= room.topLeftPosition.X &&
        this.topLeftPosition.Y <= room.bottomRightPosition.Y &&
        room.bottomRightPosition.Y >= room.topLeftPosition.Y
      );
    }
  }
}
