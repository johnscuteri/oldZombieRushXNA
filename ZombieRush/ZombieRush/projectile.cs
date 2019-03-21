//******************************************************
// File: projectile.cs
//
// Purpose: This is the class for projectiles
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
    class projectile
    {
        //variables
        # region ClassVariables
        //position
        private Vector2 m_pos;
        //speed
        private Vector2 m_speed;
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
        private Rectangle m_bounds;
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
                m_bounds = new Rectangle((int)pos.X, (int)pos.Y, frameSize.X, frameSize.Y);
            }
        }
        public Vector2 speed
        {
            get
            {
                return m_speed;
            }
            set
            {
                m_speed.X = value.X;
                m_speed.Y = value.Y;
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
        //****************************************************
        // Method: constructor
        //
        // Purpose: provides default values
        //****************************************************
        public projectile()
        {
            m_speed = new Vector2(0, 0);
            m_pos = new Vector2(0, 0);
            m_frameSize = new Point(7, 7);
            m_currentFrame = new Point(0, 0);
            m_sheetSize = new Point(5,1);
            m_layer = 0.6f;
            m_bounds = new Rectangle((int)pos.X, (int)pos.Y, frameSize.X, frameSize.Y);
        }
        //****************************************************
        // Method: Spin
        //
        // Purpose: makes the projectile spin
        //****************************************************
        public void spin()
        {
            ++m_currentFrame.X;
            if (m_currentFrame.X >= m_sheetSize.X)
            {
                m_currentFrame.X = 0;
            }
        }
        //****************************************************
        // Method: Move
        //
        // Purpose: makes projectile move and spin
        //****************************************************
        public void move()
        {
            spin();
            m_pos.X += m_speed.X;
            m_pos.Y += m_speed.Y;
            m_bounds = new Rectangle((int)pos.X, (int)pos.Y, frameSize.X, frameSize.Y);
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
                    frameSize.X, frameSize.Y),
                    Color.White, 0, Vector2.Zero,
                    1, SpriteEffects.None, m_layer);
        }
    }
}