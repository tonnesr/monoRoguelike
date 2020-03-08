using Microsoft.Xna.Framework;

namespace mono2.src.models.mapping {
  public class Room {
    public Vector2 topPos { get; }
    public Vector2 bottomPos { get; }
    public Size size { get; }
    
    public Room(Vector2 pos, Size size) {
      this.topPos = pos;
      this.size = size;

      this.bottomPos = new Vector2(pos.X + size.width, pos.Y + size.height);
    }

    public Vector2 getCenter() {
      return new Vector2((this.topPos.X + this.bottomPos.X) / 2, (this.topPos.Y + this.bottomPos.Y) / 2);
    }

    public bool isOverlapping(Room room) {
      return (
        this.topPos.X <= room.bottomPos.X &&
        this.bottomPos.X >= room.topPos.X &&
        this.topPos.Y <= room.bottomPos.Y &&
        room.bottomPos.Y >= room.topPos.Y
      );
    }
  }
}
