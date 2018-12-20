using AhPeGe.GameProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhPeGe.GameProject.GameClasses.Player
{
    public class BasePlayer
    {
        Texture2D playerImage;
        public BasePlayer()
        {
            playerImage = ContentStore.PlayerMapIcon;
        }

        public void Update() {
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 worldMapCentre) {

            spriteBatch.Draw(playerImage, worldMapCentre, Color.White);
        }
    }
}
