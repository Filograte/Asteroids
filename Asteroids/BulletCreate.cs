using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids

{
    public class BulletCreate
    {
        Texture2D Texture;
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Scale;
        public float Rotation;
        public Vector2 Origin;
        public SpriteEffects SE;
        public float Layer;
        public Vector2 Accel;
        

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)Position.X, //investigate 
                    (int)Position.Y,
                    Texture.Width,
                    Texture.Height);
            }
        }

        public BulletCreate(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            this.Position = position;
        }

        public BulletCreate(Texture2D texture, Vector2 position, Vector2 velocity, Vector2 scale, float rotation, Vector2 origin, SpriteEffects se, float layer, Vector2 accel )
        {
            this.Texture = texture;
            this.Position = position;
            this.Velocity = velocity;
            this.Scale = scale;
            this.Rotation = rotation;
            this.Origin = origin;
            this.SE = se;
            this.Layer = layer;
            this.Accel = accel;
        }

        

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(Texture, Position, null, Color.White, Rotation, Origin, Scale, SE, Layer);
        }
    }
}
