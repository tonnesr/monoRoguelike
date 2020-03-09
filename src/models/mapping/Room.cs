using Microsoft.Xna.Framework;
using System;

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
      return new Vector2((int)((this.topPos.X + this.bottomPos.X) / 2), (int)((this.topPos.Y + this.bottomPos.Y) / 2));
    }

    public bool isOverlapping(Room room) {
      return (
        this.topPos.X <= room.bottomPos.X &&
        this.bottomPos.X >= room.topPos.X &&
        this.topPos.Y <= room.bottomPos.Y &&
        room.bottomPos.Y >= room.topPos.Y
      );
    }

    /*public bool isPointOverlapping(Vector2 point) {
      int x = (int)point.X;
      int y = (int)point.Y;

      return (
        x >= Math.Max((int)this.topPos.X, (int)this.bottomPos.X) && 
        x <= Math.Min((int)this.topPos.X, (int)this.bottomPos.X) && 
        y >= Math.Max((int)this.topPos.Y, (int)this.bottomPos.Y) && 
        y <= Math.Min((int)this.topPos.Y, (int)this.bottomPos.Y)
      );
    }*/
  }
}
