using System;

namespace AthenaEngine.Source.Utility;

public struct Vector3
{
    public float X, Y, Z;

    public Vector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static Vector3 operator +(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }

    public static Vector3 operator -(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    }

    public static Vector3 operator *(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
    }

    public static Vector3 operator /(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.X / v2.X,  v1.Y / v2.Y, v1.Z / v2.Z);
    }
    
    public static Vector3 Normalize(Vector3 vector)
    {
        float length = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
        return new Vector3(vector.X / length, vector.Y / length, vector.Z / length);
    }
    
    public static Vector3 operator *(Vector3 v, float scalar) =>
        new Vector3(v.X * scalar, v.Y * scalar, v.Z * scalar);
    
    public static Vector3 Cross(Vector3 v1, Vector3 v2)
    {
        return new Vector3(
            v1.Y * v2.Z - v1.Z * v2.Y,
            v1.Z * v2.X - v1.X * v2.Z,
            v1.X * v2.Y - v1.Y * v2.X
        );
    }
}

public struct Vector3Int
{
    public int X, Y, Z;

    public Vector3Int(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    
    public static Vector3Int operator +(Vector3Int v1, Vector3Int v2)
    {
        return new Vector3Int(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }

    public static Vector3Int operator -(Vector3Int v1, Vector3Int v2)
    {
        return new Vector3Int(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    }

    public static Vector3Int operator *(Vector3Int v1, Vector3Int v2)
    {
        return new Vector3Int(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
    }

    public static Vector3Int operator /(Vector3Int v1, Vector3Int v2)
    {
        return new Vector3Int(v1.X / v2.X,  v1.Y / v2.Y, v1.Z / v2.Z);
    }
}