using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace AthenaEngine.Source;

public class Texture
{
    public int Handle { get; private set; }

    public Texture(int width, int height, int binVal = 0)
    {
        // Handle = GL.GenTexture();
        // GL.BindTexture(TextureTarget.Texture2D, Handle);

        byte b = (byte)(binVal * 180 + 25);
        
        // Create a simple white texture (RGBA, 255,255,255,255)
        byte[] pixels = new byte[width * height * 4]; // 4 channels (R, G, B, A)
        for (int i = 0; i < pixels.Length; i += 4)
        {
            pixels[i] = b;     // R
            pixels[i + 1] = b; // G
            pixels[i + 2] = b; // B
            pixels[i + 3] = 255; // A
        }

        // Load texture into OpenGL
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
            width, height, 0, PixelFormat.Rgba,
            PixelType.UnsignedByte, pixels);

        // Texture parameters
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

        GL.BindTexture(TextureTarget.Texture2D, 0);
    }

    public void Bind()
    {
        GL.BindTexture(TextureTarget.Texture2D, Handle);
    }
}
