using SFML.Graphics;
using SFML.Window;

namespace TcGame
{
  public class Ship : StaticActor
  {
    private static Vector2f Up = new Vector2f(0.0f, -1.0f);
    private Vector2f Forward = Up;
    private float Speed;
    private float RotationSpeed = 100.0f;
    private float ShotFrequency = 0.20f;
    private Shield shield;
    
    public Ship()
    {
      Sprite = new Sprite(Resources.Texture("Textures/Ship"));
      Center();
      OnDestroy += OnShipDestroy;

      Engine.Get.Window.KeyPressed += HandleKeyPressed;

      var flame = Engine.Get.Scene.Create<Flame>(this);
      flame.Position = Origin + new Vector2f(20.0f, 62.0f);

      var flame2 = Engine.Get.Scene.Create<Flame>(this);
      flame2.Position = Origin + new Vector2f(-20.0f, 62.0f);
      shield = Engine.Get.Scene.Create<Shield>(this);
      shield.Origin = Origin + new Vector2f(35f, 20f);
    }

    private void HandleKeyPressed(object sender, KeyEventArgs e)
    {
      
      if (Keyboard.IsKeyPressed(Keyboard.Key.C))
      {
        Shoot<Rocket>();
      }

      if (Keyboard.IsKeyPressed(Keyboard.Key.G))
      {
        shield.Active();
      }
    }

    public override void Update(float dt)
    {
      if (Keyboard.IsKeyPressed(Keyboard.Key.LShift))
      {
        Speed = 900.0f;
      }
      else
      {
        Speed = 200.0f;

        if (Keyboard.IsKeyPressed(Keyboard.Key.A))
        {
          Rotation -= RotationSpeed * dt;
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.D))
        {
          Rotation += RotationSpeed * dt;
        }
      }
    
      Forward = Up.Rotate(Rotation);
      Position += Forward * Speed * dt;

      ShootWithCooldown(dt);
                  

      MyGame.ResolveLimits(this);
      CheckCollision();
    }

        private void CheckCollision()
        {

            if (!shield.activeShield)
            {
                var asteroids = Engine.Get.Scene.GetAll<Asteroid>();
                foreach (var a in asteroids)
                {
                    Vector2f toAsteroid = a.WorldPosition - WorldPosition;
                    if (toAsteroid.Size() < 50.0f)
                    {
                        Destroy();
                        a.Destroy();
                    }
                }
            }
        }

    void OnShipDestroy(Actor obj)
    {
      Engine.Get.Window.KeyPressed -= HandleKeyPressed;
    }

    private void Shoot<T>() where T : Bullet
    {
      var rocket = Engine.Get.Scene.Create<T>();
      rocket.WorldPosition = WorldPosition;
      rocket.Forward = Forward;
    }
           
    private void ShootWithDirection<T>(Vector2f direction) where T : Bullet
    {
      var rocket = Engine.Get.Scene.Create<T>();
      rocket.WorldPosition = WorldPosition;
      rocket.Forward = direction;
    }

    public void ShootWithMouse()
    {
      if (Mouse.IsButtonPressed(Mouse.Button.Left))
      {
        var mousePosition = (Engine.Get.MousePos - Position).Normal();
        ShootWithDirection<Bullet>(mousePosition);
      }
    }
    public void ShootWithCooldown(float dt)
    {
      ShotFrequency -= dt;
      if (ShotFrequency <= 0)
      {
        ShotFrequency = 0.20f;
        ShootWithMouse();
      }
   }
  }
}

