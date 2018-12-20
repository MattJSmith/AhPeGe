using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhPeGe.GameProject.GameObjects
{
    public static class ContentStore
    {
        public static Texture2D GridTiled;
        public static Texture2D MapIconRock;
        public static Texture2D PlayerMapIcon;

        public static SpriteFont Font;
        public static void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            GridTiled = content.Load<Texture2D>("GridTiled");
            MapIconRock = content.Load<Texture2D>("Rock");

            Font = content.Load<SpriteFont>("MainFont");

            PlayerMapIcon = content.Load<Texture2D>("Player");
        }

    }
}
