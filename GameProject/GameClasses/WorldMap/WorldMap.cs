using AhPeGe.GameProject.Enum;
using AhPeGe.GameProject.GameClasses.WorldMap;
using AhPeGe.GameProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AhPeGe.Classes.GameClasses.WorldMap
{
    public class WorldMap
    {
        public SpriteAnimationCross MapGrid { get; private set; }
        public Vector2 playersCoordinatesOnMap;
        private MapArray worldMapTileArray;
        private int visibleTilesAcrossOnMap = 5;
        private Vector2 backgroundPosition = new Vector2(250, 250);

        /// <summary>
        /// Preset Constructor
        /// </summary>
        /// <param name="content"></param>
        public WorldMap()
        {
            var gridTex = ContentStore.GridTiled;

            MapGrid = new SpriteAnimationCross(gridTex, visibleTilesAcrossOnMap, 0.1f, backgroundPosition);

          //  MapGrid.setScale(1f);

            playersCoordinatesOnMap = new Vector2(100,100);

            var topLeftVisibleCoordinate = GetTopLeftVisibleCoordinateFromPlayersCoordinate(playersCoordinatesOnMap, visibleTilesAcrossOnMap);

            worldMapTileArray = new MapArray(visibleTilesAcrossOnMap, topLeftVisibleCoordinate);
        }
 
        /// <summary>
        /// Custom world map Image Constuctor
        /// </summary>
        /// <param name="mapGrid"></param>
        public WorldMap(SpriteAnimationCross mapGrid)
        {
            playersCoordinatesOnMap = Vector2.Zero;

            MapGrid = mapGrid;
        }

        public void Update(GameTime gameTime, KeyboardState keyboard)
        {
            if (!MapGrid.Paused) {
                MapGrid.Update(gameTime);
                return;
            }

            if (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up))
            {
                MoveUp();
                worldMapTileArray.Move(Direction.Up,playersCoordinatesOnMap);
            }
            else if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Down))
            {
                MoveDown();
                worldMapTileArray.Move(Direction.Down, playersCoordinatesOnMap);
            }
            else if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left))
            {
                MoveLeft();
                worldMapTileArray.Move(Direction.Left, playersCoordinatesOnMap);
            }
            else if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right))
            {
                MoveRight();
                worldMapTileArray.Move(Direction.Right, playersCoordinatesOnMap);
            }

            MapGrid.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            MapGrid.Draw(gameTime, spriteBatch, SpriteEffects.None);
        }

        public Vector2 GetCoordinates()
        {
            return playersCoordinatesOnMap;
        }


        private void MoveUp()
        {
            MapGrid.StartAnimationInDirection(GameProject.Enum.Direction.Up);
            playersCoordinatesOnMap.Y++;
        }
        private void MoveDown()
        {
            MapGrid.StartAnimationInDirection(GameProject.Enum.Direction.Down);
            playersCoordinatesOnMap.Y--;
        }
        private void MoveLeft()
        {
            MapGrid.StartAnimationInDirection(GameProject.Enum.Direction.Left);
            playersCoordinatesOnMap.X--;
        }
        private void MoveRight()
        {
            MapGrid.StartAnimationInDirection(GameProject.Enum.Direction.Right);
            playersCoordinatesOnMap.X++;
        }
        private Vector2 GetTopLeftVisibleCoordinateFromPlayersCoordinate(Vector2 playerCoordinate, int visibleTilesAcrossCount)
        {
            var visibleTilesAccrossFromCentre = (visibleTilesAcrossOnMap - 1) / 2;
            var topLeftCoordinateOnMap = new Vector2(playersCoordinatesOnMap.X - visibleTilesAccrossFromCentre,
                playersCoordinatesOnMap.Y - visibleTilesAccrossFromCentre);

            return topLeftCoordinateOnMap;
        }
    }
}
