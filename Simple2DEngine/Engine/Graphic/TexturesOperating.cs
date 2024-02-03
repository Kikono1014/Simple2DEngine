﻿using static SDL2.SDL;
using static SDL2.SDL_image;
using static Engine2D.Engine.Graphic.DrawingMethods;
using System;
using WM;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Engine2D
{
  public partial class Engine : WindowManager
  {

    public static nint GetTexture(string name)
    {
      if (_textures.TryGetValue(name, out nint texture))
      {
        return texture;
      } else {
        return _textures["MissingTexture"];
      }
    }

    /// <summary>
    /// Load textures to window GetRenderer()
    /// </summary>
    /// <param name="windowId">Id of window to add</param>
    /// <param name="directoryPath">Path to directory where texture are hidden, 
    ///                             for example: "./Engine/standartth/images/". </param>
    /// <param name="fileNames">Array of texture files' names, with extension, 
    ///                         for example: "MissingTexture.png".</param>
    public static void LoadTextures(uint windowId, string directoryPath, string[] fileNames)
    {
      foreach (string fileName in fileNames)
      {
        LoadTexture(windowId, directoryPath, fileName);
      }
    }

    /// <summary>
    /// Load texture to window GetRenderer()
    /// </summary>
    /// <param name="windowId">Id of window to add</param>
    /// <param name="directoryPath">Path to directory where texture are hidden, 
    ///                             for example: "./Engine/standartth/images/". </param>
    /// <param name="fileName">Name of texture file, with extension, 
    ///                        for example: "MissingTexture.png".</param>
    public static void LoadTexture(uint windowId, string directoryPath, string fileName)
    {
      _textures
        [ fileName[.. ^(fileName.Length - fileName.LastIndexOf('.'))] ] =
        IMG_LoadTexture(_windows[windowId].GetRenderer(), directoryPath + fileName);
    }

    public static void LoadTexturesFromAtlas(
      uint windowId,
      string path, string nameToLoad,
      int columns, int rows,
      int width, int height)
    {
      string format        = path[path.LastIndexOf('.') ..];
      string name          = path[(path.LastIndexOf('/') + 1) .. path.LastIndexOf('.')];
      string directoryPath = path[.. (path.LastIndexOf('/') + 1)] + "temp/";
      
      Image image = Image.Load(path);
      Directory.CreateDirectory(directoryPath);

      int i = 0;

      for (int y = 0; y < rows; y++)
      {
        for (int x = 0; x < columns; x++)
        {
          if (image.Width - x * width >= width && image.Height - y * height >= height)
          {
            Image clone = image.Clone(
                  p => p.Crop(new Rectangle(x * width, y * height, width, height)));

            clone.Save(directoryPath + nameToLoad + i.ToString() + format);

            _textures[ nameToLoad + i.ToString() ] =
                IMG_LoadTexture(
                  _windows[windowId].GetRenderer(),
                  directoryPath + nameToLoad + i.ToString() + format
                );

            File.Delete(directoryPath + nameToLoad + i.ToString() + format);
            i++;
          }
        }
      }

      Directory.Delete(directoryPath);
    }

    private static void DestroyTextures()
    {
      foreach (var texture in _textures)
      {
        SDL_DestroyTexture(texture.Value);
      }
    }
    
  }
}