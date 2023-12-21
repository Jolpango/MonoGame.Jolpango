using Microsoft.Xna.Framework;
using System;
namespace MonoGame.Jolpango.Utilities
{
    public enum EasingFunction
    {
        EaseInOutQuad,
        EaseOutBack,
        EaseInCubic,
        EaseOutCubic
    }
    public static class JMath
    {
        private static Random random = new Random();
        public static Vector2 GetRandomDirection()
        {
            Vector2 randomDirection = new Vector2(
                ((float)random.NextDouble() * 2.0f) - 1,
                ((float)random.NextDouble() * 2.0f) - 1
            );
            randomDirection.Normalize();
            return randomDirection;
        }
        public static double GetRandomNumber(double minimum, double maximum)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public static Vector2 GetRandomPosition(Vector2 origin, int radius)
        {
            return origin + GetRandomDirection() * radius;
        }
        public static double EaseInOutQuad(double x)
        {
            return x < 0.5 ? 2 * x * x : 1 - Math.Pow(-2 * x + 2, 2) / 2;
        }
        public static double EaseInOutQuad(double x, double end)
        {
            return EaseInOutQuad(x / end);
        }
        public static double EaseOutBack(double x)
        {
            const double c1 = 1.70158;
            const double c3 = c1 + 1;

            return 1 + c3 * Math.Pow(x - 1, 3) + c1 * Math.Pow(x - 1, 2);
        }
        public static double EaseOutBack(double x, double end)
        {
            return EaseOutBack(x / end);
        }
        public static double EaseInCubic(double x)
        {
            return x * x * x;
        }

        public static double EaseInCubic(double x, double end)
        {
            return EaseInCubic(x / end);
        }

        public static double EaseOutCubic(double x)
        {
            double f = x - 1.0;
            return f * f * f + 1.0;
        }

        public static double EaseOutCubic(double x, double end)
        {
            return EaseOutCubic(x / end);
        }
        public static float Lerp(float firstFloat, float secondFloat, float weight)
        {
            return firstFloat * (1 - weight) + secondFloat * weight;
        }
        public static Vector2 AngleToVector(float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
        public static float VectorToAngle(Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }
        public static Vector2 RotateVector(Vector2 vector, float angleRadians)
        {
            float cos = (float)Math.Cos(angleRadians);
            float sin = (float)Math.Sin(angleRadians);

            float x = vector.X * cos - vector.Y * sin;
            float y = vector.X * sin + vector.Y * cos;

            return new Vector2(x, y);
        }
    }
}
