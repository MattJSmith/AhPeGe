using AhPeGe.GameProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AhPeGe.GameProject.Text
{
    public class Message
    {

        SpriteFont font;
        string text;

        public Message(SpriteFont textFont)
        {
            font = textFont;
        }

        public Message()
        {
            font = ContentStore.Font;
        }
 

        public void SetMessage(string message)
        {
            text = message;
        }

        public void Draw(SpriteBatch spriteBatch,  Vector2 position)
        {
            spriteBatch.DrawString(font, text, position, Color.Black);
        }
        public void Draw(SpriteBatch spriteBatch, string message, Vector2 position)
        {
            spriteBatch.DrawString(font, message, position, Color.Black);
        }
    }
}
