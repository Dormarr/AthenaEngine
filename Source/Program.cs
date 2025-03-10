using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace AthenaEngine.Source
{
    class Game : GameWindow
    {
        private int _vao, _vbo;
        private Shader _shader;
        private float _totalTime = 0.0f;
        private int _gradientTexture;

        float[] _vertices = {
            // Positions       // Colors (R, G, B)    // Texture Coords (U, V)
            0.0f,  0.5f, 0.0f,  1.0f, 0.0f, 0.0f,    0.5f, 0.0f, // Top  (changed V from 1.0f to 0.0f)
            -0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f,    0.0f, 1.0f, // Bottom Left (changed V from 0.0f to 1.0f)
            0.5f, -0.5f, 0.0f,  0.0f, 0.0f, 1.0f,    1.0f, 1.0f  // Bottom Right (changed V from 0.0f to 1.0f)
        };


        public Game(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) { }

        protected override void OnLoad()
        {
            base.OnLoad();

            // Load shaders
            _shader = new Shader("Shaders/vertexShader.vert", "Shaders/fragmentShader.frag");

            _gradientTexture = GenerateGradientTexture(256, 256);
            
            // Bind the gradient texture
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, _gradientTexture);
            
            // Generate VAO & VBO
            _vao = GL.GenVertexArray();
            GL.BindVertexArray(_vao);

            _vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
            
            // Position attribute (location = 0)
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // Color attribute (location = 1)
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            // Texture Coord attribute (location = 2)
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
            GL.EnableVertexAttribArray(2);

            // Cleanup
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            _shader.Use();

            // Pass time value to the shader
            _totalTime += (float)args.Time;
            int timeLocation = GL.GetUniformLocation(_shader.Handle, "time");
            GL.Uniform1(timeLocation, _totalTime);

            // Create a transformation matrix
            Matrix4 transform = Matrix4.Identity; // Start with an identity matrix

            //Apply movement (oscillating left and right)
            float xOffset = MathF.Sin(_totalTime) * 0.5f; // Moves back and forth
            transform *= Matrix4.CreateTranslation(xOffset, 0.0f, 0.0f);
            
            // Apply rotation
            transform *= Matrix4.CreateRotationZ(_totalTime / 2); // Rotate over time
            transform *= Matrix4.CreateRotationY(_totalTime / 2); // Rotate over time
            transform *= Matrix4.CreateRotationX(_totalTime / 2); // Rotate over time

            // Send the matrix to the shader
            int transformLocation = GL.GetUniformLocation(_shader.Handle, "transform");
            GL.UniformMatrix4(transformLocation, false, ref transform);
            
            GL.BindVertexArray(_vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            SwapBuffers();
        }


        protected override void OnUnload()
        {
            base.OnUnload();

            _shader.Delete();
            GL.DeleteBuffer(_vbo);
            GL.DeleteVertexArray(_vao);
        }

        public static void Main()
        {
            var game = new Game(GameWindowSettings.Default,
                new NativeWindowSettings { ClientSize = (800,600), Title = "Athena Alpha" });
            game.Run();
        }
        
        // Assuming you have a method to create the texture
        private int GenerateGradientTexture(int width, int height)
        {
            // Create an array to hold the gradient data
            byte[] textureData = new byte[width * height * 4]; // 4 for RGBA

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Here we generate a simple gradient where R is the x position,
                    // G is the y position, and B is a combination of both.
                    float r = (float)x / width;
                    float g = (float)y / height;
                    float b = (r + g) / 2.0f;
                    
                    // Convert the floating point values to byte values for OpenGL
                    textureData[(y * width + x) * 4 + 0] = (byte)(r * 255);
                    textureData[(y * width + x) * 4 + 1] = (byte)(g * 255);
                    textureData[(y * width + x) * 4 + 2] = (byte)(b * 255);
                    textureData[(y * width + x) * 4 + 3] = 255; // Alpha is fully opaque
                }
            }

            // Generate the OpenGL texture and upload the data
            int textureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureID);
    
            // Upload the texture data to OpenGL
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, textureData);
    
            // Set texture parameters
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
    
            return textureID;
        }
    }
}
