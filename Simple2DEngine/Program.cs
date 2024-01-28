using static SDL2.SDL;
using static SDL2.SDL_image;
using System;
using Engine2D;
using static Engine2D.Engine;
using static Engine2D.Engine.Graphic.DrawingMethods;
using static Engine2D.Engine.Graphic;
using static Engine2D.Engine.Physic;
using static Engine2D.Engine.EventHandler;
using static Engine2D.Engine.Scene.GameObject;
using static Engine2D.Engine.Scene.PhysicalObject;
using WM;
using System.Runtime.CompilerServices;

namespace Program
{


  //! Animated sprite example
  public class AnimatedSpriteExample : Scene.GameObject
  {
    public AnimatedSpriteExample(string name, string scene)
    {
      _name = name;
      _scene = scene;
      Window? win = GetScene(_scene)?.GetWindow();
      if (win != null)
      {
        LoadTextures(
          SDL_GetWindowID(win.GetWindowPtr()),
          "../images/sonic/", [
                                "1.png",  "2.png",  "3.png",
                                "4.png",  "5.png",  "6.png",
                                "1f.png", "2f.png", "3f.png",
                                "4f.png", "5f.png", "6f.png",
                              ]
        );
      }
      _textures = [ 
        Engine.GetTexture("1"),  Engine.GetTexture("2"),  Engine.GetTexture("3"),  
        Engine.GetTexture("4"),  Engine.GetTexture("5"),  Engine.GetTexture("6"),
        Engine.GetTexture("1f"), Engine.GetTexture("2f"), Engine.GetTexture("3f"),
        Engine.GetTexture("4f"), Engine.GetTexture("5f"), Engine.GetTexture("6f")
      ];

      int? windowH = GetScene(_scene)?.GetWindow()?.GetWindowH();
      int? windowW = GetScene(_scene)?.GetWindow()?.GetWindowW();
      if (windowH != null && windowW != null)
      {
        int size = 32*5;
        _textureDst = 
          new SDL_Rect
          {
            x = (int)windowW/2 - size/2,
            y = (int)windowH - size,
            w = size,
            h = size
          };

      }
      
      AddTimer(
        "Change" + _name + "Texture",
        50,
        delegate { AnimationChanger(); }
      );

    
    }
    
    public void AnimationChanger()
    {
      Scene.GameObject? player = GetScene(_scene)?.GetObject(_name);
      if (player != null)
      {
        if (GetKey((int)SDL_Scancode.SDL_SCANCODE_D) &&
            !GetKey((int)SDL_Scancode.SDL_SCANCODE_A))
        {
          player.SetCurrentTextureId(
            (player.GetCurrentTextureId() + 1) % 6
          );
        } 
        if (GetKey((int)SDL_Scancode.SDL_SCANCODE_A) &&
            !GetKey((int)SDL_Scancode.SDL_SCANCODE_D))
        {
          player.SetCurrentTextureId(
            (player.GetCurrentTextureId() + 1) % 6 + 6
          );
        } 
        if (!(GetKey((int)SDL_Scancode.SDL_SCANCODE_D) ^
              GetKey((int)SDL_Scancode.SDL_SCANCODE_A)))  // if A and D or not A and not D
        {
          player.SetCurrentTextureId(
            (int)(player.GetCurrentTextureId() / 6) * 6
          );
        }
      }
    }
  
  }

  public class Plushy : Scene.GameObject
  {
    public Plushy(string name, string scene) : base()
    {
      _name = name;
      _scene = scene; 
      Window? win = GetScenesWindow(_scene);
      if (win != null)
      {
        LoadTextures(
          SDL_GetWindowID(win.GetWindowPtr()),
          "../images/EvilPlush/", [
                                  "EvilPlush1.webp",
                                  "EvilPlush2.webp",
                                  "EvilPlush3.webp",
                                  "EvilPlush4.webp",
                                  "EvilPlush5.webp",
                                  "EvilPlush6.webp"
                                 ]);

        _textures = [
                      Engine.GetTexture("EvilPlush1"),
                      Engine.GetTexture("EvilPlush2"),
                      Engine.GetTexture("EvilPlush3"),
                      Engine.GetTexture("EvilPlush4"),
                      Engine.GetTexture("EvilPlush5"),
                      Engine.GetTexture("EvilPlush6")
                    ];
        _textureDst =   
          new SDL_Rect
          {
            x = win.GetWindowW()-220,
            y = 20,
            w = 200,
            h = 200
          };
        _isStatic = true;

      
        AddTimer(
          "Change" + _name + "Texture", 
          100,
          delegate { NextTexture(); }
        );
      }
    }
  }



  //! Physical object example
  // public class PhysicalObjectExample : Scene.PhysicalObject
  // {
  //   public PhysicalObjectExample(string name, string scene) : base()
  //   {
  //     _name  = name;
  //     _scene = scene;
  //     _isShowHitbox = true;

  //     Window? window = GetScenesWindow(_scene);
  //     if (window != null)
  //     {
  //       LoadTexture(SDL_GetWindowID(window.GetWindowPtr()), "../images/", "nuroPlush.png");
  //     }

  //     _textures = [ Engine.GetTexture("nuroPlush") ];
  //     _textureDst = new SDL_Rect
  //       {
  //         x = 300,
  //         y = 300,
  //         w = 300,
  //         h = 300
  //       };
      
  //     SetHitbox(new SDL_FRect 
  //       {
  //         x = 300,
  //         y = 300,
  //         w = 300,
  //         h = 300
  //       });

  //   }
  // }


  public class Program
  {
    //! Drawing example
    // private static int _angle = 0;

    //! Physical object example
    // public static PhysicalObjectExample physicalObjectExample = new("Player", "Main");

    public static void Setup()
    {
       SetupEngine(
        SDL_INIT_VIDEO,

        IMG_InitFlags.IMG_INIT_PNG |
        IMG_InitFlags.IMG_INIT_JPG |
        IMG_InitFlags.IMG_INIT_WEBP
      );

      CreateWindow(
        "SDLTest",
        SDL_WINDOWPOS_UNDEFINED, 
        SDL_WINDOWPOS_UNDEFINED, 
        1024,
        1024,
        SDL_WindowFlags.SDL_WINDOW_SHOWN |
        SDL_WindowFlags.SDL_WINDOW_RESIZABLE /*|
        SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP*/,

        SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
        SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC
      );
      CreateScene("Main", Engine.GetWindow(1));
    }

    public static void Main(string[] args)
    {
      Setup();

      //! Animated sprite example
      GetScene("Main")?.AddObject(new AnimatedSpriteExample("Sonic", "Main"));


      //! Physical object example
      // AddObjectToScene("Main", physicalObjectExample);


      AddObjectToScene("Main", new Plushy("EvilPlushy", "Main"));

      LoadTexture(1, "../images/", "sonanoka.jpg");
      SDL_Rect sonanokaDst = new() {
        x = 300,
        y = 500,
        w = 600,
        h = 300
      };
      AddObjectToScene("Main", new Scene.GameObject("sonanoka", [GetTexture("sonanoka")], sonanokaDst));
      GetObjectFromScene("Main", "sonanoka")?.SetIsShow(false);
      GetObjectFromScene("Main", "sonanoka")?.SetIsStatic(true);


      //! Drawing example
      // SetDrawFunc(Draw);

      SetHandleKeyboardInputFunc(HandleKeyboardInput);

      RunEngine();

    }

    //! Drawing example
    // private static void Draw()
    // {
    //   WM.Window? win = Engine.GetWindow(1);
    //   if (win != null)
    //   {
    //     int centerX = 50;
    //     int centerY = win.H / 2;
    //     int r = 50;

    //     for (int n = 3; n < 16; ++n)
    //     {
    //       var verts = CreateNgonalVertexShape(n, 
    //                                           new SDL_FPoint { x = centerX, y = centerY },
    //                                           r, RGBA(255, 200, 0, 255));
          
    //       var center = GetCenterOfVertex(verts);

    //       for (int i = 0; i < verts.Length; ++i)
    //       {
    //         ref var p = ref verts[i].position;
    //         RotatePointRelatively(center, ref p, _angle);
    //       }

    //       if (n < 8) {
    //         RenderConvexShape(win.Renderer, verts);
    //       }
    //       centerX += 150;
    //     }
    //   }
    // }

    private static void HandleKeyboardInput()
    {
      if (( GetKey((int)SDL_Scancode.SDL_SCANCODE_LCTRL) ||
            GetKey((int)SDL_Scancode.SDL_SCANCODE_RCTRL) ) &&
          GetKey((int)SDL_Scancode.SDL_SCANCODE_Q))
      {
        StopEngine();
      }

      //! Drawing example
      // if (GetKey((int)SDL_Scancode.SDL_SCANCODE_SPACE))
      // {
      //   _angle = (10 + _angle) % 360;
      // }


      //! Physical object example
      // if (GetKey((int)SDL_Scancode.SDL_SCANCODE_A))
      // {
      //   ApplyForce(physicalObjectExample, new SDL_FPoint { x = -5, y = 0 });
      // }
      // if (GetKey((int)SDL_Scancode.SDL_SCANCODE_D))
      // {
      //   ApplyForce(physicalObjectExample, new SDL_FPoint { x = 5, y = 0 });
      // }
      // if (GetKey((int)SDL_Scancode.SDL_SCANCODE_W))
      // {
      //   ApplyForce(physicalObjectExample, new SDL_FPoint { x = 0, y = -5 });
      // }
      // if (GetKey((int)SDL_Scancode.SDL_SCANCODE_S))
      // {
      //   ApplyForce(physicalObjectExample, new SDL_FPoint { x = 0, y = 5 }); 
      // }

      if (GetKey((int)SDL_Scancode.SDL_SCANCODE_UP))
      {
        Scene? scene = GetScene("Main");
        if (scene != null)
        {
          ref SDL_Point camCoord = ref scene.GetCamCoord();
          camCoord.y -= 5;
        }
      }
      if (GetKey((int)SDL_Scancode.SDL_SCANCODE_LEFT))
      {
        Scene? scene = GetScene("Main");
        if (scene != null)
        {
          ref SDL_Point camCoord = ref scene.GetCamCoord();
          camCoord.x -= 5;
        }
      }
      if (GetKey((int)SDL_Scancode.SDL_SCANCODE_DOWN))
      {
        Scene? scene = GetScene("Main");
        if (scene != null)
        {
          ref SDL_Point camCoord = ref scene.GetCamCoord();
          camCoord.y += 5;
        }
      }
      if (GetKey((int)SDL_Scancode.SDL_SCANCODE_RIGHT))
      {
        Scene? scene = GetScene("Main");
        if (scene != null)
        {
          ref SDL_Point camCoord = ref scene.GetCamCoord();
          camCoord.x += 5;
        }
      }
      
      if (GetKey((int)SDL_Scancode.SDL_SCANCODE_N))
      {
        UnpressKey((int)SDL_Scancode.SDL_SCANCODE_N);
        Scene.GameObject? sonanoka = GetObjectFromScene("Main", "sonanoka");
        sonanoka?.SetIsShow(!sonanoka.GetIsShow());
      }
    }

  }
}