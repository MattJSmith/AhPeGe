
using AhPeGe.GameProject.Enum;
using AhPeGe.GameProject.GameObjects;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AhPeGe.GameProject.GameClasses.WorldMap
{
    public enum NewRow
    {
        Top,
        Bottom,
    }
    public enum NewColumn
    {
        Left,
        Right,
    }
 
    public class MapArray
    {
        /// <summary>
        /// How many tiles are visible on screen. E.G. A 5 by 5 world map.
        /// </summary>
        int howManyVisibleTilesInRow;
        /// <summary>
        /// Doesnt include center
        /// </summary>
        Vector2 VisibleTilesFromCenterExcludingCenter;

        Dictionary<Vector2, MapTile> WorldMapTiles = new Dictionary<Vector2,MapTile>();

        public MapArray(int visibleMapTileSize, Vector2 TopLeftTileCoordinate)
        {
            howManyVisibleTilesInRow = visibleMapTileSize;

            VisibleTilesFromCenterExcludingCenter = GetHowManyTilesVisibleFromCenter(howManyVisibleTilesInRow);

            SetupStartMapTiles(TopLeftTileCoordinate);
        }

        public void Move(Direction movementDirection, Vector2 mapCoordinates)
        {
            switch (movementDirection)
            {
                case Direction.Up:
                    MoveUp(mapCoordinates);
                    break;
                case Direction.Down:
                    MoveDown(mapCoordinates);
                    break;
                case Direction.Left:
                    MoveLeft(mapCoordinates);
                    break;
                case Direction.Right:
                    MoveRight(mapCoordinates);
                    break;
            }
            WorldMapTiles.TryGetValue(mapCoordinates, out MapTile currentMapTile);

            if (!currentMapTile.IsDiscovered)
            {
                currentMapTile.DiscoverTile();
            }
            //TODO: if(newly added tiles are now visible, do something)
        }

        public List<Vector2> GetTileKeysForCurrentPosition(Vector2 currentPlayersMapCoordinates)
        {
            var xIndex = currentPlayersMapCoordinates.X - VisibleTilesFromCenterExcludingCenter.X;

            var yIndex = currentPlayersMapCoordinates.Y - VisibleTilesFromCenterExcludingCenter.Y;

            List<Vector2> currentlyVisibleTiles = new List<Vector2>();

            for(var x = 0; x < howManyVisibleTilesInRow; x++)
            {
                for (var y = 0; y < howManyVisibleTilesInRow; y++)
                {
                    var tileCoordinate = new Vector2(x, y);

                    currentlyVisibleTiles.Add(tileCoordinate);
                }
            }

            return currentlyVisibleTiles;
        }

        public void Draw(Vector2 currentPlayersMapCoordinates)
        {
            var visibleTiles = GetTileKeysForCurrentPosition(currentPlayersMapCoordinates);


            foreach(var tile in visibleTiles)
            {
                //Get position of each tile
             //   tile.Draw();


            }



        }
        
        public int GetDiscoveredTileCount()
        {
            return WorldMapTiles.Values.Select(t => t.IsDiscovered).Count();
        }

        #region Private 
        private void MoveUp(Vector2 newMapCoordinates)
        {
            AddNewRow(newMapCoordinates, NewRow.Top);
        }
        private void MoveDown(Vector2 newMapCoordinates)
        {
            AddNewRow(newMapCoordinates, NewRow.Bottom);
        }
        private void MoveLeft(Vector2 newMapCoordinates)
        {
            AddNewColumn(newMapCoordinates, NewColumn.Left);
        }
        private void MoveRight(Vector2 newMapCoordinates)
        {
            AddNewColumn(newMapCoordinates, NewColumn.Right);
        }

        /// <summary>
        /// When Moving Up, An entire new row is visible.
        /// From the old player coordinate, adds a new row of tiles.
        /// </summary>
        /// <param name="playersPosition"></param>
        private void AddNewRow(Vector2 playersNextCoordinatesOnMap, NewRow newRowDirection)
        {
            var y = newRowDirection == NewRow.Top ?
             playersNextCoordinatesOnMap.Y + (VisibleTilesFromCenterExcludingCenter.Y) :
             playersNextCoordinatesOnMap.Y - (VisibleTilesFromCenterExcludingCenter.Y);

            var startX = playersNextCoordinatesOnMap.X - (VisibleTilesFromCenterExcludingCenter.X);

            for (var i = 0; i < howManyVisibleTilesInRow; i++)
            {
                var x = startX + i;
                var newTileCoordinate = new Vector2(x, y);
                var newTile = new MapTile(newTileCoordinate, ContentStore.MapIconRock);
                if (WorldMapTiles.ContainsKey(newTileCoordinate)) continue;

                WorldMapTiles.Add(newTileCoordinate, newTile);
            }
        }
        private void AddNewColumn(Vector2 playersNextCoordinatesOnMap, NewColumn newColumnDirection)
        {
            var x = newColumnDirection == NewColumn.Right ?
                playersNextCoordinatesOnMap.X + (VisibleTilesFromCenterExcludingCenter.X):
                playersNextCoordinatesOnMap.X - (VisibleTilesFromCenterExcludingCenter.X);

            var startY = playersNextCoordinatesOnMap.Y - (VisibleTilesFromCenterExcludingCenter.Y);

            for(var i = 0; i < howManyVisibleTilesInRow; i++)
            {
                var y = startY + i;
                var newTileCoordinate = new Vector2(x, y);
                var newTile = new MapTile(newTileCoordinate, ContentStore.MapIconRock);
                if (WorldMapTiles.ContainsKey(newTileCoordinate)) continue;

                WorldMapTiles.Add(newTileCoordinate, newTile);
            }
        }

        /// <summary>
        /// Gets number of tiles outward from the centre
        /// </summary>
        /// <param name="oddSizedArray"></param>
        /// <returns></returns>
        private Vector2 GetHowManyTilesVisibleFromCenter(int visibleTilesInRow)
        {
            var evenArraySizeY = visibleTilesInRow;
            var halfArraySizeY = (int) (evenArraySizeY / 2);

            var evenArraySizeX = visibleTilesInRow;
            var halfArraySizeX = (int) (evenArraySizeX / 2);

            return new Vector2(halfArraySizeX, halfArraySizeY);
        }

        private void SetupStartMapTiles(Vector2 topLeftTileCoordinate)
        {
            for (var i = 0; i < howManyVisibleTilesInRow; i++)
            {

                for (var j = 0; j < howManyVisibleTilesInRow; j++)
                {
                    var tileCoord = topLeftTileCoordinate;
                    tileCoord.X += i;
                    tileCoord.Y += j;
                    WorldMapTiles.Add(tileCoord, new MapTile(tileCoord, ContentStore.MapIconRock));
                }
            }
        }
        #endregion

    }
}
