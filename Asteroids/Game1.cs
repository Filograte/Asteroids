using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;

//todo: learn matrix transformation/per pixel collision
// learn math.cos(angle) for movement direction
// for loop vs for each loop

namespace Asteroids
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
       
        //system
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keyboardState;
        private SpriteFont font;
        Texture2D pixel;
        

       


        //ship attributes
        GameObject ship;
        Texture2D shipTexture;
        Vector2 shipPosition;
        Vector2 shipVelocity;
        Vector2 shipScale;
        float shipRotation;
        Vector2 shipOrigin;
        SpriteEffects shipSE;
        float shipLayer;
        float shipCell;
        int shipHealth;
        

        //bullet attributes, most are used in the C keypress update

        Texture2D bulletTexture;
        Vector2 bulletPosition;
        Vector2 bulletVelocity;
        Vector2 bulletScale;
        float bulletRotation;
        Vector2 bulletOrigin;
        SpriteEffects bulletSE;
        float bulletLayer;
        GameObject bullet;
        List<GameObject> bullets = new List<GameObject>();
        float bulletTimer;
        bool bulletFlag;
        int bulletHealth;

        //meteor attributes
        GameObject BigMeteor;
        GameObject SmallMeteor;
        GameObject SmallMeteor2;
        GameObject TinyMeteor;
        GameObject TinyMeteor2;
        Texture2D bigMeteorTexture;
        Texture2D smallMeteorTexture;
        Texture2D tinyMeteorTexture;
        Vector2 meteorPosition;
        Vector2 meteorVelocity;
        Vector2 smallMeteorVelocity;
        Vector2 smallMeteorVelocity2;
        Vector2 tinyMeteorVelocity;
        Vector2 tinyMeteorVelocity2;
        Vector2 meteorScale;
        float meteorRotation;
        float smallMeteorRotation;
        float smallMeteorRotation2;
        float tinyMeteorRotation;
        float tinyMeteorRotation2;
        Vector2 meteorOrigin;
        Vector2 smallMeteorOrigin;
        Vector2 tinyMeteorOrigin;
        SpriteEffects meteorSE;
        float meteorLayer;
        int bigMeteorHealth;
        int smallMeteorHealth;
        int tinyMeteorHealth;
        Random meteorSwitch = new Random();
        Random meteorRanPos = new Random();
        Random meteorRanPos2 = new Random();
        Random meteorRanPos3 = new Random();
        Random meteorRanPos4 = new Random();
        Random meteorRanDir = new Random();
        Random meteorRanDir2 = new Random();
        Random meteorRanDir3 = new Random();
        Random meteorRanDir4 = new Random();
        Random smallMeteorRanDir = new Random();
        Random smallMeteorRanDir2 = new Random();
        Random tinyMeteorRanDir = new Random();
        Random tinyMeteorRanDir2 = new Random();
        List<GameObject> meteors = new List<GameObject>();
        List<GameObject> smallMeteors = new List<GameObject>();
        
        
        float meteorTimer;
       


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
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
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

            shipTexture = Content.Load<Texture2D>("ship");
            shipPosition = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2,
    GraphicsDevice.Viewport.Bounds.Height / 2);
            shipVelocity = new Vector2();
            shipScale = new Vector2(0.2f, 0.2f);
            shipRotation = 0;
            shipOrigin = new Vector2(shipTexture.Width / 2,
    shipTexture.Height / 2);
            shipSE = default(SpriteEffects);
            shipLayer = 0;
            shipHealth = 3;





            ship = new GameObject(
               shipTexture,
               shipPosition,
               shipVelocity,
               shipScale,
               shipRotation,
               shipOrigin,
               shipSE,
               shipLayer,
               shipHealth
                );

            ship.Update();
           

            pixel = new Texture2D(this.GraphicsDevice, 1, 1);
            Color[] colourData = new Color[1];
            colourData[0] = Color.White; //The Colour of the rectangle
            pixel.SetData<Color>(colourData);


            font = Content.Load<SpriteFont>("text");
            base.LoadContent();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here


            keyboardState = Keyboard.GetState();


            if (keyboardState.IsKeyDown(Keys.A))
            {
               ship.Rotation -= 0.1f;
                ship.Update();

            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                ship.Rotation += 0.1f;
                ship.Update();

            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                //ship.Velocity = Direction(ship.Rotation);

             
                ship.Velocity.X += (float)Math.Cos(ship.Rotation)  ;
                ship.Velocity.Y += (float)Math.Sin(ship.Rotation) ;
                shipCell += 0.3f;
                if (shipCell >= 7)
                {
                    shipCell = 7;
                }
                
                ship.Velocity.Normalize();
                ship.Position += ship.Velocity * shipCell;
                ship.Update();
                //ship.Velocity += ship.Accel * 0.2f;


            }
            if (keyboardState.IsKeyUp(Keys.W))
            {

                ship.Velocity.X += (float)Math.Cos(ship.Rotation);
                ship.Velocity.Y += (float)Math.Sin(ship.Rotation);
                shipCell -= 0.2f;
                if (shipCell < 0)
                {
                    shipCell = 0;
                }
                ship.Velocity.Normalize();
                ship.Position += ship.Velocity * shipCell;
                ship.Update();

            }

            if (keyboardState.IsKeyDown(Keys.Space) && bulletFlag == true)
            {

                
                bulletTexture = Content.Load<Texture2D>("bullet");
                bulletVelocity = new Vector2((float)Math.Cos(ship.Rotation), (float)Math.Sin(ship.Rotation));
                bulletPosition = ship.Position; //+ bullet.Velocity;          
                bulletScale = new Vector2(0.2f, 0.2f);
                bulletRotation = 0;
                bulletOrigin = new Vector2(bulletTexture.Width / 2,
                    bulletTexture.Height / 2);
                bulletSE = default(SpriteEffects);
                bulletLayer = 0;
                bulletHealth = 0;

                bullet = new GameObject(
                bulletTexture,
                bulletPosition,
                bulletVelocity,
                bulletScale,
                bulletRotation,
                bulletOrigin,
                bulletSE,
                bulletLayer,
                bulletHealth
                );


                bullets.Add(bullet);
                bullet.Update();
                bulletFlag = false;
             
        }
            bulletTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (bulletTimer >= 500)
            {
                bulletFlag = true;
                bulletTimer = 0;
            }

            
            
            meteorTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (meteorTimer >= 300)
            {
                bigMeteorTexture = Content.Load<Texture2D>("bigmeteor");
                int MeteorSwitch = meteorSwitch.Next(1, 5);// selecting position and flight diretion of meteors
                switch (MeteorSwitch)
                {
                    case 1:
                        meteorPosition = new Vector2(meteorRanPos.Next(100, 600), 10); //placed on top of screen
                        meteorRotation = (float)meteorRanDir.NextDouble() * (-5.5f - -3.9f) + -3.9f;//fires downward at angles
                        break;
                    case 2:
                        meteorPosition = new Vector2(meteorRanPos2.Next(100, 600), 590); //placed at bottom of screen 
                        meteorRotation = (float)meteorRanDir2.NextDouble() * (-0.9f - -2.2f) + -2.2f;//fires upward at angles
                        break;
                    case 3:
                        meteorPosition = new Vector2(10, meteorRanPos3.Next(150, 550)); //left
                        meteorRotation = (float)meteorRanDir3.NextDouble() * (0.6f - -0.6f) + -0.6f;
                        break;
                    case 4:
                        meteorPosition = new Vector2(790, meteorRanPos4.Next(150, 550)); //right
                        meteorRotation = (float)meteorRanDir4.NextDouble() * (-4.0f - -2.6f) + -2.6f;
                        break;
                    default:
                        break;
                }


                
                //meteorPosition = new Vector2(meteorRanPos2.Next(100, 600), 800); //placed at bottom of screen 
                //meteorRotation = (float)meteorRanDir2.NextDouble() * (-0.9f - -2.2f) + -2.2f;//fires upward at angles

                meteorVelocity = new Vector2((float)Math.Cos(meteorRotation), (float)Math.Sin(meteorRotation));
                meteorOrigin = new Vector2(bigMeteorTexture.Width / 2,
                     bigMeteorTexture.Height / 2);
                meteorScale = new Vector2(0.5f, 0.5f);
                meteorSE = default(SpriteEffects);
                meteorLayer = 0;
                bigMeteorHealth = 3;



                BigMeteor = new GameObject(
                   bigMeteorTexture,
                   meteorPosition,
                   meteorVelocity,
                   meteorScale,
                   meteorRotation,
                   meteorOrigin,
                   meteorSE,
                   meteorLayer,
                   bigMeteorHealth
                    );             

                meteors.Add(BigMeteor);
                BigMeteor.Update();
                meteorTimer = 0;

            }

            for (int x = 0; x < meteors.Count; x++) //meteor + ship collison
            {
                if (meteors[x].BoundingBox.Intersects(ship.BoundingBox))
                {
                    ship.Touched = true;
                    break;
                }
                else
                {
                    ship.Touched = false;
                }

            }
          
          

            for (int x = 0; x < bullets.Count; x++) //move bullets, destory on exit bounds
            {

                bullets[x].Position += bullets[x].Velocity * 10f;

                bullets[x].Update();
               if ( bullets[x].Position.X > GraphicsDevice.Viewport.Bounds.Width ||
                   bullets[x].Position.X < 0 ||
                   bullets[x].Position.Y > GraphicsDevice.Viewport.Bounds.Height ||
                   bullets[x].Position.Y < 0)
                {
                    bullets.RemoveAt(x);
                    
                }
            }




            for (int x = 0; x < meteors.Count; x++) //move meteors,  destory on exit bounds or no health
            {
                meteors[x].Position += meteors[x].Velocity * 1f;
                meteors[x].Update();


                if (meteors[x].Position.X > GraphicsDevice.Viewport.Bounds.Width ||
                    meteors[x].Position.X < 0 ||
                    meteors[x].Position.Y > GraphicsDevice.Viewport.Bounds.Height ||
                    meteors[x].Position.Y < 0 )
                {
                    meteors.RemoveAt(x);
                }
            }

            if (bullet != null) //bullet + meteor collison
            {
                for (int x = 0; x < meteors.Count; x++)
                {
                    for (int y = 0; y < bullets.Count; y++)
                    {
                        if (meteors[x].BoundingBox.Intersects(bullets[y].BoundingBox))
                        {

                            bullets.RemoveAt(y);
                            meteors[x].Health--;
                            break;

                        }

                    }

                }
            }

            for (int x = 0; x < meteors.Count; x++)//meteor fragmentation
            {
                if (meteors[x].Health == 0)
                {
                    if (meteors[x].Texture == bigMeteorTexture)
                    {
                        smallMeteorTexture = Content.Load<Texture2D>("smallmeteor");
                        smallMeteorVelocity = new Vector2((float)Math.Cos(smallMeteorRotation),
                            (float)Math.Sin(smallMeteorRotation));
                        smallMeteorVelocity2 = new Vector2((float)Math.Cos(smallMeteorRotation2),
                            (float)Math.Sin(smallMeteorRotation2));
                        smallMeteorRotation = (float)smallMeteorRanDir.NextDouble() * (6f - 0f) + 0f;
                        smallMeteorRotation2 = smallMeteorRotation + 3.14f;
                        smallMeteorOrigin = new Vector2(smallMeteorTexture.Width / 2,
                         smallMeteorTexture.Height / 2);
                        smallMeteorHealth = 2;


                        SmallMeteor = new GameObject(
                            smallMeteorTexture,
                            meteors[x].Position,
                            smallMeteorVelocity,
                            meteors[x].Scale,
                            smallMeteorRotation,
                            smallMeteorOrigin,
                            meteors[x].SE,
                            meteors[x].Layer,
                            smallMeteorHealth
                            );

                        SmallMeteor2 = new GameObject(
                            smallMeteorTexture,
                            meteors[x].Position,
                            smallMeteorVelocity2,
                            meteors[x].Scale,
                            smallMeteorRotation2,
                            smallMeteorOrigin,
                            meteors[x].SE,
                            meteors[x].Layer,
                            smallMeteorHealth
                            );


                        meteors.RemoveAt(x);
                        meteors.Add(SmallMeteor);
                        meteors.Add(SmallMeteor2);
                        SmallMeteor.Update();
                        break;
                    }
                    if (meteors[x].Texture == smallMeteorTexture)
                    {

                        tinyMeteorTexture = Content.Load<Texture2D>("tinymeteor");
                        tinyMeteorVelocity = new Vector2((float)Math.Cos(tinyMeteorRotation),
                            (float)Math.Sin(tinyMeteorRotation));
                        tinyMeteorVelocity2 = new Vector2((float)Math.Cos(tinyMeteorRotation2),
                           (float)Math.Sin(tinyMeteorRotation2));
                        tinyMeteorRotation = (float)tinyMeteorRanDir.NextDouble() * (6f - 0f) + 0f;
                        tinyMeteorRotation2 = tinyMeteorRotation + 3.14f;
                        tinyMeteorOrigin = new Vector2(tinyMeteorTexture.Width / 2,
                         tinyMeteorTexture.Height / 2);
                        tinyMeteorHealth = 1;



                        TinyMeteor = new GameObject(
                            tinyMeteorTexture,
                            meteors[x].Position,
                            tinyMeteorVelocity,
                            meteors[x].Scale,
                            tinyMeteorRotation,
                            tinyMeteorOrigin,
                            meteors[x].SE,
                            meteors[x].Layer,
                            tinyMeteorHealth
                            );

                        TinyMeteor2 = new GameObject(
                            tinyMeteorTexture,
                            meteors[x].Position,
                            tinyMeteorVelocity2,
                            meteors[x].Scale,
                            tinyMeteorRotation2,
                            tinyMeteorOrigin,
                            meteors[x].SE,
                            meteors[x].Layer,
                            tinyMeteorHealth
                            );


                        meteors.RemoveAt(x);
                        meteors.Add(TinyMeteor);
                        meteors.Add(TinyMeteor2);
                        TinyMeteor.Update();
                        break;
                    }
                    if (meteors[x].Texture == tinyMeteorTexture)
                    {
                        meteors.RemoveAt(x);
                        break;
                    }

                }
            }
            //for (int x = 0; x < meteors.Count; x++)//meteor fragmentation
            //{
            //    if (meteors[x].Health == 0)
            //    {
            //        if (meteors[x].Texture == smallMeteorTexture)
            //        {
            //            meteors.RemoveAt(x);
            //            break;
            //        }
            //    }

            //}
            //for (int x = 0; x < meteors.Count; x++)//meteor fragmentation
            //    {
            //    if (meteors[x].Health == 0 && meteors[x] == BigMeteor)
            //    {
            //        smallMeteorTexture = Content.Load<Texture2D>("smallmeteor");
            //        smallMeteorVelocity = new Vector2((float)Math.Cos(smallMeteorRotation),
            //            (float)Math.Sin(smallMeteorRotation));
            //        smallMeteorRotation = (float)smallMeteorRanDir.NextDouble() * (6f - 0f) + 0f;
            //        smallMeteorOrigin = new Vector2(smallMeteorTexture.Width / 2,
            //         smallMeteorTexture.Height / 2);

            //        SmallMeteor = new GameObject(
            //            smallMeteorTexture,
            //            meteors[x].Position,
            //            smallMeteorVelocity,
            //            meteors[x].Scale,
            //            smallMeteorRotation,
            //            smallMeteorOrigin,
            //            meteors[x].SE,
            //            meteors[x].Layer,
            //            smallMeteorHealth
            //            );
            //        meteors.Add(SmallMeteor);
            //        SmallMeteor.Update();
            //        meteors.RemoveAt(x);
            //        break;

            //    }
            //    if ((meteors[x].Health == 0 && meteors[x] == SmallMeteor))
            //    {
            //        meteors.RemoveAt(x);
            //        break;
            //    }
            //}




                if (ship.Position.X > GraphicsDevice.Viewport.Bounds.Width ) //ship bounds
            {
            ship.Position.X = 30;
               
            }
            if (ship.Position.X < 0)
            {
                ship.Position.X = GraphicsDevice.Viewport.Bounds.Width - 30;

            }
            if (ship.Position.Y < 0)
            {
                ship.Position.Y = GraphicsDevice.Viewport.Bounds.Height;
            }

            if (ship.Position.Y > GraphicsDevice.Viewport.Bounds.Height)
            {
                ship.Position.Y =  30;
            }

            //if (ship.Rotation < -3f)
            //    ship.Rotation = 3f;
            //if (ship.Rotation > 3f)
            //    ship.Rotation = -3f;


            base.Update(gameTime);
        }

        private Vector2 Direction(float rotator)
        {
            Vector2 direct = new Vector2((float)Math.Cos(rotator),
                                    (float)Math.Sin(rotator)); ;
            return direct;

        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            ship.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Ship position: " + ship.Position.X + " " 
               + ship.Position.Y, new Vector2(100, 130), Color.Black);
            spriteBatch.DrawString(font, "Ship rotation: " + ship.Rotation, new Vector2(100, 150), Color.Black);
            spriteBatch.DrawString(font, "Ship rotation vector: " + ship.Velocity.X + " " + ship.Velocity.Y,
                new Vector2(100, 170), Color.Black);

            for (int x = 0; x < bullets.Count; x++)
            {

                bullets[x].Draw(spriteBatch);
                spriteBatch.Draw(pixel, bullets[x].BoundingBox, Color.Pink);
            }
            for (int x = 0; x < meteors.Count; x++)
            {
                meteors[x].Draw(spriteBatch);
                spriteBatch.Draw(pixel, meteors[x].BoundingBox, Color.Blue);
            }

            if (ship.Touched == true)
            {
                GraphicsDevice.Clear(Color.Red);
            }   
            //spriteBatch.Draw(pixel, ship.BoundingBox, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    
}
