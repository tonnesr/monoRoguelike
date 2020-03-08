using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using mono2.src.models.mapping;
using mono2.src.models;

namespace mono2.src.mapping 
{
  public class TileMapGenerator {
    Random random;
    
    public TileMapGenerator() {
      this.random = new Random();
    }

    public Tile[,] generate(Size mapSize) {
      int width = mapSize.width;
      int height = mapSize.height;

      Tile[,] map = new Tile[width, height];
      List<Room> rooms = this.generateRooms(20, 3, 10, mapSize); // TODO 
      Console.WriteLine(rooms.Count);
      // TODO generate corridors

      for (int x = 0; x < width; x++) {
        for (int y = 0; y < height; y++) {
          Tile newTile = new Tile("floor1", x, y, TileType.Floor, Color.Gray, TileMovementType.Walkable);

          foreach (Room room in rooms) {
            if (
              (x == room.topLeftPosition.X && y == room.topLeftPosition.Y) || 
              (x == room.bottomRightPosition.X && y == room.bottomRightPosition.Y) || 
              (x == room.topLeftPosition.X && y == room.bottomRightPosition.Y) || 
              (x == room.bottomRightPosition.X && y == room.topLeftPosition.Y)
            ) {
              newTile = new Tile("wall1", x, y, TileType.Wall, Color.Gray, TileMovementType.Impassable);
              break;
            }
          }

          map[x, y] = newTile;
        }
      }

      return map;
    }

    private List<Room> generateRooms(int roomCount, int minRoomSize, int maxRoomSize, Size mapSize) {
      List<Room> rooms = new List<Room>();

      Size newSize = new Size(0, 0);
      Vector2 newPos = Vector2.Zero;
      bool failed = false;
      for (int r = 0; r < roomCount; r++) {
        newSize = new Size(minRoomSize + random.Next(maxRoomSize - minRoomSize + 1), minRoomSize + random.Next(maxRoomSize - minRoomSize + 1));
        newPos = new Vector2(random.Next((mapSize.width - newSize.width) - 1) + 1, random.Next((mapSize.height - newSize.height) - 1) + 1);

        Room newRoom = new Room(newPos, newSize);
        Vector2 newCenter = newRoom.getCenter();

        failed = false;
        foreach (Room room in rooms) {
          if (newRoom.isOverlapping(room)) {
            failed = true;
            break;
          }
        }

        if (!failed) rooms.Add(newRoom);
      }

      return rooms;
    }
  }
}