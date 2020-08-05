using SFML.Graphics;
using SFML.Window;

namespace TcGame
{
  public class Asteroid : StaticActor
  {
    public float RotationSpeed = 20.0f;
    public float Speed = 200.0f;
    public Vector2f Forward = new Vector2f(1.0f, 0.0f);
    public int damageReceived;
    private Sprite[] asteroidStatus;

    public Asteroid()
    {
      asteroidStatus = new Sprite[]
      {
        new Sprite(Resources.Texture("Textures/Asteroid00")),
        new Sprite(Resources.Texture("Textures/Asteroid01")),
        new Sprite(Resources.Texture("Textures/Asteroid02"))
      };
      Sprite = asteroidStatus[0];
      Center();
      OnDestroy += OnAsteroidDestroyed;
      damageReceived = 0;
    }

    public override void Update(float dt)
    {
      Position += Forward * Speed * dt;
      Rotation += RotationSpeed * dt;
      MyGame.ResolveLimits(this);
    }

    void OnAsteroidDestroyed(Actor obj)
    {
      var hud = Engine.Get.Scene.GetFirst<HUD>();
      if (hud != null)
      {
        hud.Points += 100;
      }

      var explosion = Engine.Get.Scene.Create<Explosion>();
      explosion.WorldPosition = WorldPosition;
    }

    public void OnAsteroidDamaged()
        {
            damageReceived++;

            if (damageReceived < asteroidStatus.Length)
            {
                Sprite = asteroidStatus[damageReceived];
                var explosion = Engine.Get.Scene.Create<Explosion>();
                explosion.WorldPosition = WorldPosition;

            }
            else
            {
                Destroy();
            }

        }
  }
}
