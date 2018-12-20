using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AhPeGe.GameProject.GameClasses.WorldMap
{
    /// <summary>
    /// A Map Tile in the map Array
    /// </summary>
    public class MapTile
    {
        public bool IsDiscovered;

        public Vector2 MapTileCoordinate;

        public Sprite MapTileIcon;

        public MapTile(Vector2 mapTileCoordinate, Texture2D mapTileIcon)
        {
            MapTileCoordinate = mapTileCoordinate;
            IsDiscovered = false;
            var mapTileIconTexture = mapTileIcon;
            MapTileIcon = new Sprite(mapTileIconTexture);
        }

        public void DiscoverTile()
        {
            IsDiscovered = true;
            
            //Triggers events when entering a tile

        }


        public void Draw(Vector2 position)
        {

        }
    }
}
