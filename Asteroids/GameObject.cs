using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids

{
    public class GameObject
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Scale;
        public float Rotation;
        public Vector2 Origin;
        public SpriteEffects SE;
        public float Layer;
        public Matrix Transform;
        public bool Touched;
        public Rectangle BoundingBox;
        public int Health;
        
      
        public GameObject(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            this.Position = position;
        }



        public GameObject(Texture2D texture, Vector2 position, Vector2 velocity, Vector2 scale, float rotation, Vector2 origin, SpriteEffects se, float layer, int health)
        {
            this.Texture = texture;
            this.Position = position;
            this.Velocity = velocity;
            this.Scale = scale;
            this.Rotation = rotation;
            this.Origin = origin;
            this.SE = se;
            this.Layer = layer;
            this.Health = health;

        }


        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Texture, Position, null, Color.White, Rotation, Origin, Scale, SE, Layer);
        }


        private void UpdateTransformMatrix()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Origin, 0)) *
                      Matrix.CreateScale(Scale.X * 0.7f) *
                      Matrix.CreateRotationZ(Rotation) *
                      Matrix.CreateTranslation(new Vector3(Position, 0));

        }




        private void UpdateRectangle()
        {

            Vector2 topLeft = Vector2.Transform(Vector2.Zero, Transform);
            Vector2 topRight = Vector2.Transform(new Vector2(Texture.Width, 0), Transform);
            Vector2 bottomLeft = Vector2.Transform(new Vector2(0, Texture.Height), Transform);
            Vector2 bottomRight = Vector2.Transform(new Vector2(Texture.Width, Texture.Height), Transform);

            Vector2 min = new Vector2(MathEx.Min(topLeft.X, topRight.X, bottomLeft.X, bottomRight.X),
                 MathEx.Min(topLeft.Y, topRight.Y, bottomLeft.Y, bottomRight.Y));

            Vector2 max = new Vector2(MathEx.Max(topLeft.X, topRight.X, bottomLeft.X, bottomRight.X),
                MathEx.Max(topLeft.Y, topRight.Y, bottomLeft.Y, bottomRight.Y));

            BoundingBox = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));


        }

        public void Update()
        {
            UpdateTransformMatrix();
            UpdateRectangle();

        }

        public bool Collided(GameObject Target)
        {
            bool intersects = BoundingBox.Intersects(Target.BoundingBox);
            Touched = intersects;
            Target.Touched = intersects;
            return intersects;
        }
    }



   


}

   
