using UnityEngine;

namespace Utilities
{
    public static class DebugDraw
    {
        public static void DrawSphere(Vector3 center, float radius, Color color, float duration = 0, bool depthTest = true)
        {
            float step = 10.0f;
            for (float theta = 0; theta < 360; theta += step)
            {
                for (float phi = 0; phi < 360; phi += step)
                {
                    Vector3 start = center + radius * new Vector3(
                        Mathf.Sin(Mathf.Deg2Rad * theta) * Mathf.Cos(Mathf.Deg2Rad * phi),
                        Mathf.Sin(Mathf.Deg2Rad * theta) * Mathf.Sin(Mathf.Deg2Rad * phi),
                        Mathf.Cos(Mathf.Deg2Rad * theta));

                    Vector3 end = center + radius * new Vector3(
                        Mathf.Sin(Mathf.Deg2Rad * (theta + step)) * Mathf.Cos(Mathf.Deg2Rad * phi),
                        Mathf.Sin(Mathf.Deg2Rad * (theta + step)) * Mathf.Sin(Mathf.Deg2Rad * phi),
                        Mathf.Cos(Mathf.Deg2Rad * (theta + step)));

                    Debug.DrawLine(start, end, color, duration, depthTest);

                    end = center + radius * new Vector3(
                        Mathf.Sin(Mathf.Deg2Rad * theta) * Mathf.Cos(Mathf.Deg2Rad * (phi + step)),
                        Mathf.Sin(Mathf.Deg2Rad * theta) * Mathf.Sin(Mathf.Deg2Rad * (phi + step)),
                        Mathf.Cos(Mathf.Deg2Rad * theta));

                    Debug.DrawLine(start, end, color, duration, depthTest);
                }
            }
        }
    }
}