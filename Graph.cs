using UnityEngine;


public class Point {
    public Transform point;
    public float time;

}

public delegate Vector3 GraphFunction(Vector3 position, int resolution, float time, float c1, float c2, int i);

public enum GraphFunctionName {
    SpiralDiffEq,
    HourGlassDiffEq,
    CircleDiffEq,
    SineFunction,
    DoubleSineFunction,
    SineTimes2Function,
    WildinSineFunction,
    TanFunction,
    TanSineFunction
}

public class Graph : MonoBehaviour {

    public Transform pointPrefab;
    [Range(10, 500)] public int resolution = 10;
    [Range(-20, 20)] public float c1 = 0;
    [Range(-20, 20)] public float c2 = 0;
    public GraphFunctionName activeFunction;
    Point[] points;
    static GraphFunction[] functions = {
        SpiralDiffEq, HourGlassDiffEq, CircleDiffEq, SineFunction, DoubleSineFunction, SineTimes2Function, WildinSineFunction, TanFunction, TanSineFunction
    };
    

    void Start() {
        float time = 0f;
        points = new Point[resolution];
        float step = (2f / resolution) * 5f;
        Vector3 scale = Vector3.one * step;
        Vector3 position;
        position.y = 0f;
        position.z = 0f;
        for (int i = 0; i < points.Length; ++i) {
            Transform point = Instantiate(pointPrefab);
            position.x = ((i + 0.5f) * step) - 5f;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = new Point {
                point = point,
                time = time
            };
            time += 0.01f;
        }
    }

    void Update() {

        float time = Time.time;
        GraphFunction f = functions[(int)activeFunction];

        for (int i = 0; i < points.Length; ++i) {
            Transform point = points[i].point;
            Vector3 position = point.localPosition;
            if(activeFunction == 0) {
                point.localPosition = f(position, resolution, points[i].time, c1, c2, i);
                if(points[i].time > 10) {
                    points[i].time = 0;
                }
            }
            else if((int)activeFunction == 1 || (int)activeFunction == 2) {
                point.localPosition = f(position, resolution, points[i].time, c1, c2, i);
            } else {
                point.localPosition = f(position, resolution, time, c1, c2, i);
            }
            points[i].point = point;
            points[i].time += 0.02f;
        }
    }

    public static Vector3 SpiralDiffEq(Vector3 position, int resolution, float time, float c1, float c2, int i) {
        float e = 1 * Mathf.Exp(-time);
        position.x = ((c1 * e) * ((1 * Mathf.Cos(Mathf.PI * time)) - Mathf.Sin(Mathf.PI * time))) + ((c2 * e) * (Mathf.Cos(Mathf.PI * time) + (1 * Mathf.Sin(Mathf.PI * time))));
        position.y = ((c1 * e) * (1 * Mathf.Cos(Mathf.PI * time))) + ((c2 * e) * (1 * Mathf.Sin(Mathf.PI * time)));

        return position;
    }

    public static Vector3 HourGlassDiffEq(Vector3 position, int resolution, float time, float c1, float c2, int i) {
        position.x = c1 * (Mathf.Cos(4 * time)) + c2 * (Mathf.Sin(4 * time));
        position.y = c1 * (Mathf.Cos(time) + 3 * Mathf.Sin(time)) + c2 * (Mathf.Sin(time) - 3 * Mathf.Cos(time));

        return position;
    }

    public static Vector3 CircleDiffEq(Vector3 position, int resolution, float time, float c1, float c2, int i) {
        position.x = c1 * (-2 * Mathf.Cos(time)) + c2 * (-2 * Mathf.Sin(time));
        position.y = c1 * (Mathf.Cos(time) + 3 * Mathf.Sin(time)) + c2 * (Mathf.Sin(time) - 3 * Mathf.Cos(time));

        return position;
    }

    public static Vector3 SineFunction(Vector3 position, int resolution, float time, float c1, float c2, int i) {
        position.x = ((i + 0.5f) * (2f / resolution) * 5f) - 5f;
        position.y = Mathf.Sin(Mathf.PI * (position.x + time) * c1) * c2;

        return position;
    }

    public static Vector3 DoubleSineFunction(Vector3 position, int resolution, float time, float c1, float c2, int i) {
        position.x = ((i + 0.5f) * (2f / resolution) * 5f) - 5f;
        position.y = Mathf.Sin(Mathf.PI * (position.x + time) * c1) * c2;
        position.y += Mathf.Sin(2f * Mathf.PI * (position.x + time)) / 2f;
        position.y *= 2f / 3f;

        return position;
    }

    public static Vector3 SineTimes2Function(Vector3 position, int resolution, float time, float c1, float c2, int i) {
        position.x = ((i + 0.5f) * (2f / resolution) * 5f) - 5f;
        position.y = Mathf.Sin(Mathf.PI * (position.x + time) * c1) * c2;
        position.y += Mathf.Sin(2f * Mathf.PI * (position.x + 2f * time)) / 2f;
        position.y *= 2f / 3f;

        return position;
    }

    public static Vector3 WildinSineFunction(Vector3 position, int resolution, float time, float c1, float c2, int i) {
        position.x = ((i + 0.5f) * (2f / resolution) * 5f) - 5f;
        position.y = Mathf.Sin(Mathf.PI * (position.x + time) * c1) * c2;
        position.y += Mathf.Sin(2f * Mathf.PI * (position.x * time)) / 2f;
        position.y *= 2f / 3f;

        return position;
    }

    public static Vector3 TanSineFunction(Vector3 position, int resolution, float time, float c1, float c2, int i) {
        position.x = ((i + 0.5f) * (2f / resolution) * 5f) - 5f;
        position.y = Mathf.Tan(Mathf.PI * (position.x + time) * c1) * c2;
        position.y += Mathf.Sin(2f * Mathf.PI * (position.x + time)) / 2f;
        position.y *= 2f / 3f;

        return position;
    }

    public static Vector3 TanFunction(Vector3 position, int resolution, float time, float c1, float c2, int i) {
        position.x = ((i + 0.5f) * (2f / resolution) * 5f) - 5f;
        position.y = Mathf.Tan(Mathf.PI * (position.x + time) * c1) * c2;

        return position;
    }
}