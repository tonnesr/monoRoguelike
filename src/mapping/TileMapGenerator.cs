using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using mono2.src.models.mapping;
using mono2.src.models;
using System.Linq;

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
      List<Room> rooms = this.generateRooms(100, 3, 9, mapSize); // TODO
      List<Corridor> corridors = this.generateCorridors(rooms);
      Console.WriteLine($"Room count: {rooms.Count}, Corridor count: {corridors.Count}");
      // TODO generate corridors

      for (int x = 0; x < width; x++) {
        for (int y = 0; y < height; y++) {
          Tile newTile = new Tile("empty1", x, y, TileType.Wall, Color.Gray, TileMovementType.Impassable); // TODO remove with empty tile? (when implementing inside of rooms filling stuff v)

          foreach (Room room in rooms) {
            if ((x >= room.topPos.X && x <= room.bottomPos.X) && (y >= room.topPos.Y && y <= room.bottomPos.Y)) {
              newTile = new Tile("floor1", x, y, TileType.Floor, Color.Gray, TileMovementType.Walkable);
              break;
            }
          }

          foreach (Corridor corridor in corridors) {
            if (
              (x <= corridor.bottomPos.X && x >= corridor.topPos.X && y == corridor.topPos.Y) ||
              (x <= corridor.topPos.X && x >= corridor.bottomPos.X && y == corridor.bottomPos.Y) ||
              (y <= corridor.bottomPos.Y && y >= corridor.topPos.Y && x == corridor.topPos.X) ||
              (y <= corridor.topPos.Y && y >= corridor.bottomPos.Y && x == corridor.bottomPos.X) 
            ) {
              newTile = new Tile("floor1", x, y, TileType.Floor, Color.Gray, TileMovementType.Walkable);
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

      for (int r = 0; r < roomCount; r++) {
        newSize = new Size(minRoomSize + this.random.Next(maxRoomSize - minRoomSize + 1), minRoomSize + this.random.Next(maxRoomSize - minRoomSize + 1));
        newPos = new Vector2(this.random.Next((mapSize.width - newSize.width) - 1) + 1, this.random.Next((mapSize.height - newSize.height) - 1) + 1);

        Room newRoom = new Room(newPos, newSize);
        Vector2 newCenter = newRoom.getCenter();

        if ((rooms.Where(room => room.isOverlapping(newRoom)).ToList()).Count <= 0) {
          rooms.Add(newRoom);
        }
      }

      return rooms;
    }

    private List<Corridor> generateCorridors(List<Room> rooms) {
      List<Corridor> corridors = new List<Corridor>();

      Room prevRoom = null;
      foreach (Room room in rooms) {
        if (prevRoom != null) {
          Vector2 prevRoomCenter = prevRoom.getCenter();
          Vector2 currentRoomCenter = room.getCenter();

          if (this.random.Next(0, 1) == 0) { // TODO different corridor alignment
            corridors.Add(new Corridor(prevRoomCenter, new Vector2((int)prevRoomCenter.X, (int)currentRoomCenter.Y))); // |
            corridors.Add(new Corridor(currentRoomCenter, new Vector2((int)prevRoomCenter.X, (int)currentRoomCenter.Y))); // -
          } else {
            corridors.Add(new Corridor(prevRoomCenter, new Vector2((int)currentRoomCenter.X, (int)prevRoomCenter.Y))); // |
            corridors.Add(new Corridor(currentRoomCenter, new Vector2((int)currentRoomCenter.X, (int)prevRoomCenter.Y))); // -
          }
        }
        prevRoom = room;
      }

      return corridors;
    }
  }
}