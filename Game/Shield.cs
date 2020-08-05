using System;
using System.Collections.Generic;
using SFML.Window;

namespace TcGame
{
    public class Shield : AnimatedActor
    {
        private float delay;
        private float shieldState;
        public bool activeShield;
        private float upOrDown;
       

        public Shield()
        {
            shieldState = 0.0f;
            delay = 0.20f;
            activeShield = false;
            AnimatedSprite = new AnimatedSprite(Resources.Texture("Textures/Shield"), 3, 2);
            AnimatedSprite.FrameTime = 0.02f;
            Center();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            Scale = MathUtil.Lerp(new Vector2f(0.0f, 0.0f), new Vector2f(1.0f, 1.0f), shieldState);
            shieldState += dt / delay * DetermineIfShieldIsUP();
            shieldState = MathUtil.Clamp(shieldState, 0.0f, 1.0f);

        }

        public void Active()
        {
            if (!activeShield)
            {
                activeShield = true;
                Engine.Get.Timer.SetTimer(5.0f, new TimerDelegate(NotActive), false);
                
            }
        }

        public void NotActive()
        {
            activeShield = false;
        }

        public float DetermineIfShieldIsUP()
        {
            if (activeShield)
            {
                upOrDown = 1.0f;
            }
            else
            {
                upOrDown = -1.0f;
            }

            return upOrDown;
        }
       
    }
}
