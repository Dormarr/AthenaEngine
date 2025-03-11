namespace AthenaEngine.Source.Utility;

public struct Vector2
{
    public float X, Y;

    public Vector2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public static Vector2 operator +(Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
    }

    public static Vector2 operator -(Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
    }

    public static Vector2 operator *(Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.X * v2.X, v1.Y * v2.Y);
    }

    public static Vector2 operator /(Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.X / v2.X, v1.Y / v2.Y);
    }
}

public struct Vector2Int
{
    public int X, Y;

    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Vector2Int operator +(Vector2Int v1, Vector2Int v2)
    {
        return new Vector2Int(v1.X + v2.X, v1.Y + v2.Y);
    }

    public static Vector2Int operator -(Vector2Int v1, Vector2Int v2)
    {
        return new Vector2Int(v1.X - v2.X, v1.Y - v2.Y);
    }

    public static Vector2Int operator *(Vector2Int v1, Vector2Int v2)
    {
        return new Vector2Int(v1.X * v2.X, v1.Y * v2.Y);
    }

    public static Vector2Int operator /(Vector2Int v1, Vector2Int v2)
    {
        return new Vector2Int(v1.X / v2.X, v1.Y / v2.Y);
    }
}