using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Framework;

namespace MouseHunt
{
    public class gameInput
    {
            //sets up variables for keyboard
            private KeyboardState prevKeyState;
            private KeyboardState curKeyState;
            //sets up variables for mouse
            private MouseState prevMouseState;
            private MouseState curMouseState;
            private Vector2 mousePos;

            //gets previous values from devices
            public gameInput()
            {
                prevKeyState = Keyboard.GetState();
                prevMouseState = Mouse.GetState();
            }

            // Checks the possible keyboard states
            public bool IsKeyDown(Keys key) //Checks if key is down
            {
                return (curKeyState.IsKeyDown(key));
            }

            public bool IsHoldingKey(Keys key) //checks if key was previously and still pressed
            {
                return (curKeyState.IsKeyDown(key) &&
                    prevKeyState.IsKeyDown(key));
            }

            public bool WasKeyPressed(Keys key) // checks if key has only just been pressed
            {
                return (curKeyState.IsKeyDown(key) &&
                    prevKeyState.IsKeyUp(key));
            }

            public bool HasReleasedKey(Keys key) //checks if key is no longer pressed
            {
                return (curKeyState.IsKeyUp(key) &&
                    prevKeyState.IsKeyDown(key));
            }

            //Finds mouse position
            public Vector2 getMousePos()
            {
                Vector2 v;
                v.X = mousePos.X;
                v.Y = mousePos.Y;


                return v;
            }
            public MouseState getMouseState()
            {
                return curMouseState;
            }

            public bool isLeftMouseButtonDown()
            {
                // returns the state of the left mouse button
                if (curMouseState.LeftButton == ButtonState.Pressed) { return true; }

                return false;
            }
            public bool isRightMouseButtonDown()
            {
                // returns the state of the left mouse button
                if (curMouseState.RightButton == ButtonState.Pressed) { return true; }

                return false;
            }
            //checks if mouse only just been pressed
            public bool isLeftMouseButtonClick()
            {
                // return true only directly after a mouse down click
                if (curMouseState.LeftButton == ButtonState.Pressed &&
                   prevMouseState.LeftButton == ButtonState.Released) return true;
                return false;

            }
            public bool isRightMouseButtonClick()
            {
                // return true only directly after a mouse down click
                if (curMouseState.RightButton == ButtonState.Pressed &&
                   prevMouseState.RightButton == ButtonState.Released) return true;
                return false;
            }
            // the update method, must be called from the main game.Update function
            public void Update(Rectangle graphicsBoundries)
            {
                //set our previous state to our new state
                prevKeyState = curKeyState;

                //get our new state
                curKeyState = Keyboard.GetState();

                prevMouseState = curMouseState;
                curMouseState = Mouse.GetState();
                mousePos.X = curMouseState.X;
                mousePos.Y = curMouseState.Y;

                var mousePoint = new Point( Convert.ToInt32(mousePos.X), Convert.ToInt32(mousePos.Y));

                if(!graphicsBoundries.Contains(mousePos))
                {
                  Mouse.SetPosition(prevMouseState.X,prevMouseState.Y);
                }
      
            }
        }
}
