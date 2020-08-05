using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace TcGame
{
    class Rocket : Bullet
    {
        private float rotationSpeed;

        public Rocket()
        {
            Sprite = new Sprite(Resources.Texture("Textures/Rocket"));
            Speed = 300.0f;
            rotationSpeed = 90.0f;
            Center();
            Flame flame = Engine.Get.Scene.Create<Flame>(this);
            flame.Position = Origin + new Vector2f(0f, 36f);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            ChaseAsteroid(dt);
        }

        public Asteroid ClosestAsteroid()
        {
            Asteroid closestAsteroid = null;
            float minDistance = 800.0f;
            List<Asteroid> asteroids = Engine.Get.Scene.GetAll<Asteroid>();

            foreach (Asteroid a in asteroids)
            {
                var toAsteroid = a.WorldPosition - WorldPosition;
                if (toAsteroid.Size() < minDistance)
                {
                    minDistance = toAsteroid.Size();
                    closestAsteroid = a;
                }
            }

            return closestAsteroid;
        }

        public void ChaseAsteroid(float dt)
        {
            Asteroid closest = ClosestAsteroid();

            if (closest != null)
            {
                var distanceToClosest = closest.WorldPosition - WorldPosition;
                Forward = Forward.Rotate(rotationSpeed * dt * MathUtil.Sign(distanceToClosest.Normal(), Forward));
            }
        }
    }

    
}
