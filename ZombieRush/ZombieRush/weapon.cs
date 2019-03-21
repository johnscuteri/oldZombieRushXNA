//******************************************************
// File: weapons.cs
//
// Purpose: This is the class for all weapons
//
// Written By: John Scuteri
// last modified on 3/11/2013 
//******************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace ZombieRush
{
    class weapon
    {
        //variables
        # region ClassVariables
        //position
        private Vector2 m_pos;
        //texture holding the image
        private Texture2D m_img;
        //frame size
        private Point m_frameSize;
        //current frame
        private Point m_currentFrame;
        //sheet dimensions
        private Point m_sheetSize;
        //layer
        private float m_layer;
        //bounding rectangle
        private Rectangle m_bounds;
        //Up or down
        private bool m_up;
        # endregion ClassVariables
        public Vector2 pos
        {
            get
            {
                return m_pos;
            }
            set
            {
                m_pos.X = value.X;
                m_pos.Y = value.Y;
            }
        }
        public Texture2D img
        {
            get
            {
                return m_img;
            }
            set
            {
                m_img = value;
            }
        }
        public Point frameSize
        {
            get
            {
                return m_frameSize;
            }
            set
            {
                m_frameSize.X = value.X;
                m_frameSize.Y = value.Y;
            }
        }
        public Point currentFrame
        {
            get
            {
                return m_currentFrame;
            }
            set
            {
                m_currentFrame.X = value.X;
                m_currentFrame.Y = value.Y;
            }
        }
        public Point sheetSize
        {
            get
            {
                return m_sheetSize;
            }
            set
            {
                m_sheetSize.X = value.X;
                m_sheetSize.Y = value.Y;
            }
        }
        public float layer
        {
            get
            {
                return m_layer;
            }
            set
            {
                m_layer = value;
            }
        }
        public Rectangle bounds
        {
            get
            {
                return m_bounds;
            }
            set
            {
                m_bounds = new Rectangle(value.X, value.Y, value.Width, value.Height);
            }
        }
        public bool up
        {
            get
            {
                return m_up;
            }
            set
            {
                m_up = value;
            }
        }
        //****************************************************
        // Method: constructor
        //
        // Purpose: provides default values
        //****************************************************
        public weapon()
        {
            m_pos = new Vector2(0, 0);
            m_frameSize = new Point(90, 90);
            m_currentFrame = new Point(0, 0);
            m_sheetSize = new Point(8, 1);
            m_layer = 0.4f;
            m_bounds = new Rectangle((int)pos.X, (int)pos.Y, frameSize.X, frameSize.Y);
            m_up = true;
        }
        public weapon(Vector2 v, bool b)
        {
            m_pos = new Vector2(v.X, v.Y);
            m_frameSize = new Point(90, 90);
            m_currentFrame = new Point(0, 0);
            m_sheetSize = new Point(8, 1);
            m_layer = 0.4f;
            m_bounds = new Rectangle((int)m_pos.X, (int)m_pos.Y, frameSize.X, frameSize.Y);
            m_up = b;
        }
        //****************************************************
        // Method: Spin
        //
        // Purpose: makes the cannon spin
        //****************************************************
        public void Spin()
        {
            ++m_currentFrame.X;
            if (m_currentFrame.X >= m_sheetSize.X)
            {
                m_currentFrame.X = 0;
                ++m_currentFrame.Y;
                if (m_currentFrame.Y >= m_sheetSize.Y)
                    m_currentFrame.Y = 0;
            }
        }
        //****************************************************
        // Method: Draw
        //
        // Purpose: To make the draw segment of the game easier to read
        //****************************************************
        public void Draw(SpriteBatch sp)
         {
            sp.Draw(m_img, m_pos,
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X,
                    frameSize.Y),
                Color.White, 0, Vector2.Zero,
                1, SpriteEffects.None, m_layer);
         }
    }
}
