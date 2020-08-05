using SFML.Graphics;
using SFML.Window;

namespace TcGame
{
  public class Background : StaticActor
  {
  //  private Shader shader;
    private Texture texture;

    public Background()
    {
      var screenSize = Engine.Get.Window.Size;

      texture = Resources.Texture("Textures/Background");
      Sprite = new Sprite(texture);
      Sprite.TextureRect = new IntRect(0, 0, (int)screenSize.X, (int)screenSize.Y);

     //shader = new Shader();
     // shader.SetParameter("texture", texture);
     //shader.SetParameter("color", Color.White);
    }

    public override void Draw(RenderTarget target, RenderStates states)
    {
     // states.Shader = shader;
    //  states.Texture = texture;
      base.Draw(target, states);
    }
  }
}

