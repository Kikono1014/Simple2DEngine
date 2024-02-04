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

namespace Program
{


  // ! Animated sprite example
  public class AnimatedSpriteExample : Scene.GameObject
  {
    private bool _isWaiting = false;
    public AnimatedSpriteExample(string name, string scene)
    {
      _name = name;
      _scene = scene;
      Window? win = GetScene(_scene)?.GetWindow();
      if (win != null)
      {
        int textureW = 78;
        int textureH = 82;

        Dictionary<string, SDL_Rect> nameAndAreaOfTexturesFromAtlas = new()
          {
            {"sonicStand",   new(){ x = 0,        y = 0,          w = textureW,   h = textureH } },
            {"sonicWait",    new(){ x = textureW, y = 0,          w = textureW*8, h = textureH } },
            {"sonicRun",     new(){ x = 0,        y = textureH,   w = textureW*8, h = textureH } },
            {"sonicRunFast", new(){ x = 0,        y = textureH*2, w = textureW*8, h = textureH } },
          };
        
        foreach (var item in nameAndAreaOfTexturesFromAtlas)
        {
          Engine.LoadTexturesFromAtlas(
            SDL_GetWindowID(win.GetWindowPtr()),
            "../images/sonicAtlas.png", item.Key,
            item.Value,
            textureW, textureH,
            [ RGBA(76, 131, 190, 255), RGBA(132, 161, 131, 255) ]
          );
          
        }
      }

      _textures.Add(Engine.GetTexture("sonicStand0"));
      
      for (int i = 0; i < 8; i++)
      {
        _textures.Add(Engine.GetTexture("sonicWait" + i.ToString()));
      }
      for (int i = 0; i < 8; i++)
      {
        _textures.Add(Engine.GetTexture("sonicRun" + i.ToString()));
      }
      for (int i = 0; i < 4; i++)
      {
        _textures.Add(Engine.GetTexture("sonicRunFast" + i.ToString()));
      }
      

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
        80,
        delegate { AnimationChanger(); }
      );
      AddTimer(
        "Enable" + _name + "WaitAnimation",
        2000,
        delegate { EnableWaitingAnimation(); }
      );
      AddTimer(
        "Change" + _name + "WaitAnimation",
        300,
        delegate { ChangeWaitingAnimation(); }
      );

    
    }
    
    public void AnimationChanger()
    {
      if (GetKey((int)SDL_Scancode.SDL_SCANCODE_D) &&
          !GetKey((int)SDL_Scancode.SDL_SCANCODE_A))
      {
        if (GetFlip() == SDL_RendererFlip.SDL_FLIP_HORIZONTAL)
        {
          SetFlip(SDL_RendererFlip.SDL_FLIP_NONE);
        }

        if(_isWaiting)
        {
          _isWaiting = false;
          GetTimer("Start" + _name + "WaitAnimation")?.Stop();
        }
        
        NextTextureInDiapason(9, 16);
      } 

      if (GetKey((int)SDL_Scancode.SDL_SCANCODE_A) &&
          !GetKey((int)SDL_Scancode.SDL_SCANCODE_D))
      {
        if (GetFlip() == SDL_RendererFlip.SDL_FLIP_NONE)
        {
          SetFlip(SDL_RendererFlip.SDL_FLIP_HORIZONTAL);
        }

        if(_isWaiting)
        {
          _isWaiting = false;
          GetTimer("Start" + _name + "WaitAnimation")?.Stop();
        }

        NextTextureInDiapason(9, 16);
      } 

      if (!(GetKey((int)SDL_Scancode.SDL_SCANCODE_D) ^
            GetKey((int)SDL_Scancode.SDL_SCANCODE_A))
            && !_isWaiting)  // if A and D or not A and not D (is standing) and is not waiting
      {
        SetCurrentTextureId(0);
      }
    }
    
    public void EnableWaitingAnimation()
    {
      _isWaiting = true;
      GetTimer("Start" + _name + "WaitAnimation")?.Stop();
    }



    public void ChangeWaitingAnimation()
    {
      if(_isWaiting)
      {
        NextTextureInDiapason(1, 9);
        if(GetCurrentTextureId() == 9)
        {
          SetCurrentTextureId(8);
          _isWaiting = false;
          GetTimer("Start" + _name + "WaitAnimation")?.Start();
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