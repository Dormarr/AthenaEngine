using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Diagnostics;

namespace AthenaEngine.Source;

public class Camera
{
    public Vector3 Position { get; set; }
    public Vector3 Front { get; private set; }
    public Vector3 Up { get; private set; }
    public Vector3 Right { get; private set; }

    private float _yaw;
    private float _pitch;
    
    private float _speed = 5.0f;
    private float _sensitivity = 0.1f;
    
    public float AspectRatio { get; set; }
    public bool IsOrthographic { get; set; }
    public float FOV { get; set; } = 45.0f;

    public Camera(Vector3 position, float aspectRatio, bool isOrthographic = false)
    {
        Position = position;
        AspectRatio = aspectRatio;
        IsOrthographic = isOrthographic;
        
        _yaw = -90.0f;
        _pitch = 0.0f;

        Front = new Vector3(0f, 0f, -1f);
        Up = Vector3.UnitY;

        UpdateCameraVectors();
        
        Debug.Print("Camera initialized.");
    }

    private void UpdateCameraVectors()
    {
        Vector3 front = new Vector3
        (
            (float)Math.Cos(MathHelper.DegreesToRadians(_yaw)) * (float)Math.Cos(MathHelper.DegreesToRadians(_pitch)),
            (float)Math.Sin(MathHelper.DegreesToRadians(_pitch)),
            (float)Math.Sin(MathHelper.DegreesToRadians(_yaw)) * (float)Math.Cos(MathHelper.DegreesToRadians(_pitch))
        );

        Front = Vector3.Normalize(front);
        Right = Vector3.Normalize(Vector3.Cross(Front, Vector3.UnitY));
        Up = Vector3.Normalize(Vector3.Cross(Right, Front));
    }

    public void ProcessKeyboard(KeyboardState state, float deltaTime)
    {
        float velocity = _speed * deltaTime;

        if (state.IsKeyDown(Keys.W)) Position += Front * velocity;
        if (state.IsKeyDown(Keys.S)) Position -= Front * velocity;
        if (state.IsKeyDown(Keys.A)) Position -= Right * velocity;
        if (state.IsKeyDown(Keys.D)) Position += Right * velocity;
    }

    public void ProcessMouseMovement(float deltaX, float deltaY)
    {
        _yaw += deltaX * _sensitivity;
        _pitch += deltaY * _sensitivity;

        _pitch = MathHelper.Clamp(_pitch, -89.0f, 89.0f);
        UpdateCameraVectors();
    }

    public Matrix4 GetViewMatrix()
    {
        return Matrix4.LookAt(Position, Position + Front, Up);
    }

    public Matrix4 GetProjectionMatrix()
    {
        if (IsOrthographic)
        {
            float orthoSize = 30.0f;
            return Matrix4.CreateOrthographic(orthoSize * AspectRatio, orthoSize, -1.0f, 100.0f);
        }
        else
        {
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FOV), AspectRatio, -1.0f, 100.0f);
        }
    }
}
