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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState previousKeyboardState;

        //Audio
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;
        Cue track;

        //score items
        SpriteFont score;
        int scoreNum;
        float bones;

        //Sprite textures
        Texture2D wepen;
        Texture2D ene1;
        Texture2D ene2;
        Texture2D cb;
        Texture2D bkgrnd;
        Texture2D splash;

        //enemy speed variable
        Vector2 spe;

        //Sprite lists
        List <enemy> enemies;
        List <enemy> enemiesToBeDeleted;
        List <projectile> projectiles;
        List <projectile> projectilesToBeDeleted;
        List <weapon> weapons;

        //Edges
        List <Rectangle> edges;

        //values to pass
        Point frameSizezom = new Point(21, 43);
        Point frameSizezomkni = new Point(15, 42);
        Point sheetSize = new Point(4, 1);

        //Mouse stuff
        mouse m;
        Point good = new Point(1,0);
        Point bad = new Point(0,0);

        //Collision stuff
        Rectangle bounds1;
        Rectangle bounds2;
        Rectangle bounds3;
        
        //enemy generation
        Random rand;
        int enemiesLeft;

        //Game control stuff
        enum Leevel { StartScreen, Level, EndGame };
        Leevel level;
        int endGameCycles;
        int LEvel;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //enemy speed variable
            spe = new Vector2(1, 0);
            //allows bounds to goto (100, 0, 600, 90) with some wiggle room //Top weapon bounding rectangle
            bounds1 = new Rectangle(189, 0, 511, 5);
            //allows bounds to goto (100, 390, 600, 90) with some wiggle room //Bottom weapon bounding rectangle
            bounds2 = new Rectangle(189, 475, 511, 5);
            //bounds of the right edge
            bounds3 = new Rectangle(799, 0, 1, 480);
            m = new mouse();
            enemies = new List <enemy> ();
            enemiesToBeDeleted = new List <enemy>();
            projectiles = new List <projectile>();
            projectilesToBeDeleted = new List <projectile>();
            weapons = new List <weapon> ();
            //edges of the map
            edges = new List <Rectangle>();
            edges.Add(new Rectangle(0, 0, 800, 1));//Up
            edges.Add(new Rectangle(0, 0, 1, 480));//Left
            edges.Add(bounds3);//Right
            edges.Add(new Rectangle(0, 479, 800, 1));//Down
            scoreNum = 0;
            bones = 13;
            enemiesLeft = 150;
            endGameCycles = 200;
            //enemy generation
            rand = new Random();
            //level start
            level = Leevel.StartScreen;
            LEvel = 0;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Sprites
            bkgrnd = Content.Load<Texture2D>("Sprites/GameBackground");
            splash = Content.Load<Texture2D>("Sprites/splash");
            wepen = Content.Load<Texture2D>("Sprites/Cannon2");
            ene1 = Content.Load<Texture2D>("Sprites/zombie");
            ene2 = Content.Load<Texture2D>("Sprites/zombieKnight");
            cb = Content.Load<Texture2D>("Sprites/cannonballs");
            score = Content.Load<SpriteFont>("score");
            m.img = Content.Load<Texture2D>("Sprites/goodbad");

            //Sounds
            audioEngine = new AudioEngine(@"Content\Sounds.xgs");
            waveBank = new WaveBank(audioEngine, @"Content\Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, @"Content\Sound Bank.xsb");
            track = soundBank.GetCue("ThemeOne");
            track.Play();

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            KeyboardState keyboardState = Keyboard.GetState();
            if ((previousKeyboardState.IsKeyUp(Keys.Escape) && (keyboardState.IsKeyDown(Keys.Escape))) || (previousKeyboardState.IsKeyUp(Keys.Q) && (keyboardState.IsKeyDown(Keys.Q))))
            {
                this.Exit();
            }

            // TODO: Add your update logic here


            switch (level)
            {
                case Leevel.StartScreen:
                    if (previousKeyboardState.IsKeyUp(Keys.P) && (keyboardState.IsKeyDown(Keys.P)))
                    {
                        //Left
                        level = Leevel.Level;
                    }
                    break;
                case Leevel.Level:
                    //Enemy Generating code
                    #region enemy-generator
                    if (rand.Next(0,100) < 10)
                    {
                        if (enemiesLeft > 0)
                        {
                            Vector2 v = new Vector2(0, rand.Next(97, 335));
                            int boole = rand.Next(0, 2);
                            if (boole == 0)
                            {
                                enemy e = new enemy(spe, v, frameSizezom, sheetSize);
                                e.img = ene1;
                                enemies.Add(e);
                            }
                            else if (boole == 1)
                            {
                                enemy e = new enemy(spe, v, frameSizezomkni, sheetSize);
                                e.img = ene2;
                                enemies.Add(e);
                            }
                            --enemiesLeft;
                        }
                    }

            //(speed, spot, frameSizezom or frameSizezomkni, sheetSize)
            #endregion enemy-generator

                    //move the mouse
                    #region mouse-movement
                    MouseState mouseState = Mouse.GetState();
                    m.pos = new Vector2(mouseState.X, mouseState.Y);
                    if (m.bounds.Intersects(new Rectangle(0, 0, 800, 1)))
                    {
                        //Up .Y=0
                        m.pos = new Vector2(mouseState.X, 0);
                    }
                    if (m.bounds.Intersects(new Rectangle(0, 0, 1, 480)))
                    {
                        //Left, .X=0    ;
                        m.pos = new Vector2( 0, mouseState.Y);
                    }
                    if (m.bounds.Intersects(new Rectangle(799, 0, 1, 480)))
                    {
                        //Right, .X=710    ;
                        m.pos = new Vector2(710,mouseState.Y);
                    }
                    if (m.bounds.Intersects(new Rectangle(0, 479, 800, 1)))
                    {
                        //Down,  .Y=390  ;
                        m.pos = new Vector2(mouseState.X,390);
                    }
                    #endregion mouse-movement

                    //Keyboard controls
                    #region Keyboard_controls
                    foreach (weapon d in weapons)
                    {
                        if (previousKeyboardState.IsKeyUp(Keys.Left) && (keyboardState.IsKeyDown(Keys.Left)))
                        {
                            //Left
                            if (d.up)
                            {
                                d.currentFrame = new Point(5, 0);
                            }
                            else
                            {
                                d.currentFrame = new Point(0, 0);
                            }
                        }
                        if (previousKeyboardState.IsKeyUp(Keys.Right) && (keyboardState.IsKeyDown(Keys.Right)))
                        {
                            //Right
                            if (d.up)
                            {
                                d.currentFrame = new Point(3, 0);
                            }
                            else
                            {
                                d.currentFrame = new Point(2, 0);
                            }
                        }
                        if (previousKeyboardState.IsKeyUp(Keys.Up) && (keyboardState.IsKeyDown(Keys.Up)))
                        {
                            //Up
                            if (d.up)
                            {
                                d.currentFrame = new Point(4, 0);
                            }
                            else
                            {
                                d.currentFrame = new Point(1, 0);
                            }
                        }
                        if (previousKeyboardState.IsKeyUp(Keys.Down) && (keyboardState.IsKeyDown(Keys.Down)))
                        {
                            //Down
                            if (d.up)
                            {
                                d.currentFrame = new Point(4, 0);
                            }
                            else
                            {
                                d.currentFrame = new Point(1, 0);
                            }
                        }
                        if (previousKeyboardState.IsKeyUp(Keys.Space) && (keyboardState.IsKeyDown(Keys.Space)) && (bones > 0.3))
                        {
                            projectile p = new projectile();
                            p.img = cb;
                            bones -= 0.3f;
                            Vector2 j = d.pos;
                            j.X += 41;
                            j.Y += 41;
                            p.pos = j;
                            Point k = d.currentFrame;
                            int s = 1;
                            int Swi = k.X;
                            switch (Swi)
                            {
                                case 0:
                                    j = new Vector2(-s, s);
                                    break;
                                case 1:
                                    j = new Vector2(0, s);
                                    break;
                                case 2:
                                    j = new Vector2(s, s);
                                    break;
                                case 3:
                                    j = new Vector2(s, -s);
                                    break;
                                case 4:
                                    j = new Vector2(0, -s);
                                    break;
                                case 5:
                                    j = new Vector2(-s, -s);
                                    break;
                            }   
                            p.speed = j;
                            projectiles.Add(p);
                        }
                    }
                    #endregion Keyboard_controls

                    //Movement of enemies and projectiles
                    #region move
                    foreach (projectile p in projectiles)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
                    {
                        p.move();
                    }
                    foreach (enemy e in enemies)
                    {
                        e.move();
                    }
                    #endregion move

                    //collision stuff for weapons/weapon generation
                    #region weapon_collison
                    bool flag = false;
                    bool up = false;
                    if (m.bounds.Intersects(bounds1))
                    {
                        m.gb = true;
                        up = false;
                        //flag showing collison of tower with potential tower
                        foreach (weapon d in weapons)
                        {
                            if (m.bounds.Intersects(d.bounds))
                            {
                                flag = true;
                                m.gb = false;
                            }
                        }

                    }
                    else if (m.bounds.Intersects(bounds2))
                    {
                        m.gb = true;
                        up = true;
                        //flag showing collison of tower with potential tower
                        foreach (weapon d in weapons)
                        {
                            if (m.bounds.Intersects(d.bounds))
                            {
                                flag = true;
                                m.gb = false;
                            }
                        }

                    }
                    else
                    {
                        m.gb = false;
                    }
                    if ((mouseState.LeftButton == ButtonState.Pressed) && (flag == false) && (m.gb == true))
                    {
                        if (bones >= 10)
                        {
                            weapon z = new weapon(m.pos, up);
                            z.img = wepen;
                            if (up)
                            {
                                z.currentFrame = new Point(5, 0);
                            }
                            weapons.Add(z);
                            bones -= 10;
                        }
                    }
                    #endregion weapon_collison

                    //collision enemies reaching their goal
                    #region goalie
                    foreach (enemy e in enemies)
                    {
                        if (bounds3.Intersects(e.bounds))
                        {

                            enemiesToBeDeleted.Add(e);
                            scoreNum -= 3;
                            soundBank.PlayCue("grunt");
                        }
                    }
                    #endregion goalie

                    //collision stuff for enemies-projectiles
                    #region enemies-projectiles-collison
                    foreach (projectile p in projectiles)
                    {
                        foreach (enemy e in enemies)
                        {
                            if(p.bounds.Intersects(e.bounds))
                            {
                                projectilesToBeDeleted.Add(p);
                                enemiesToBeDeleted.Add(e);
                                ++bones;
                                ++scoreNum;
                                soundBank.PlayCue("wahoo");
                            }
                        }
                    }
                    #endregion enemies-projectiles-collison

                    //collision for projectiles aganst walls
                    #region projectiles-walls-collison
                    foreach (projectile p in projectiles)
                    {
                        foreach (Rectangle r in edges)
                        {
                            if (p.bounds.Intersects(r))
                            {
                                projectilesToBeDeleted.Add(p);
                            }
                        }
                    }
                    #endregion projectiles-walls-collison

                    //Removal of items
                    #region removal
                    foreach (projectile p in projectilesToBeDeleted)
                    {
                        projectiles.Remove(p);
                    }
                    foreach (enemy e in enemiesToBeDeleted)
                    {
                        enemies.Remove(e);
                    }
                    projectilesToBeDeleted.Clear();
                    enemiesToBeDeleted.Clear();
                    #endregion removal

                    //Level Control
                    if ((enemiesLeft == 0) && (enemies.Count == 0))
                    {
                        ++LEvel;
                        enemiesLeft = 150;
                        if (LEvel > 5)
                        {
                            level = Leevel.EndGame;
                        }
                    }
                    break;
                case Leevel.EndGame:
                    --endGameCycles;
                    if (endGameCycles == 0)
                    {
                        this.Exit();
                    }
                    break;
                }
                    previousKeyboardState = keyboardState;

                    base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            switch(level)
            {
                case Leevel.StartScreen:
                    spriteBatch.Draw(splash, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                    break;
                case Leevel.Level:
                    //Background
                    spriteBatch.Draw(bkgrnd, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                    //Score
                    spriteBatch.DrawString(score, "Score: " + scoreNum + "    Bones: " + bones + "   Enemies Left: " + enemiesLeft + "   Level :" + LEvel, new Vector2(200, 0), Color.Black, 0f, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
                    //mouse
                    m.Draw(spriteBatch);
                    foreach (enemy e in enemies)
                    {
                        e.Draw(spriteBatch);
                    }
                    foreach (projectile p in projectiles)
                    {
                        p.Draw(spriteBatch);
                    } 
                    foreach (weapon we in weapons)
                    {
                        we.Draw(spriteBatch);
                    }
                    break;
                case Leevel.EndGame:     
                    if (endGameCycles < 150)
                    {
                        spriteBatch.Draw(bkgrnd, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                        spriteBatch.DrawString(score, "GAME OVER, YOUR SCORE IS: " + scoreNum, new Vector2(200, 200), Color.Green, 0f, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
                    }
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
