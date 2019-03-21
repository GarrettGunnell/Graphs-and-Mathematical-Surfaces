using UnityEngine;


public delegate Vector3 ZGraphFunction(Vector3 position, int resolution, float time, float c1, float c2, int i);

public enum ZGraphFunctionName {
    SpiralDiffEq,
    SineFunction,
    DoubleSineFunction,
    SineTimes2Function,
    WildinSineFunction,
    TanFunction,
    TanSineFunction
}

public class ZGraph : MonoBehaviour {

    public Transform pointPrefab;
    [Range(10, 500)] public int resolution = 10;
    [Range(-20, 20)] public float c1 = 0;
    [Range(-20, 20)] public float c2 = 0;
    public ZGraphFunctionName activeFunction;
    Point[] points;
    static ZGraphFunction[] functions = {
        SpiralDiffEq, SineFunction, DoubleSineFunction, SineTimes2Function, WildinSineFunction, TanFunction, TanSineFunction
    };


    void Start() {
        float time = 0f;
        points = new Point[resolution * resolution];
        float step = (2f / resolution) * 5f;
        Vector3 scale = Vector3.one * step;
        Vector3 position;
        position.y = 0f;
        position.z = 0f;
        for(int i = 0, x = 0, z = 0; i < points.Length; ++i, ++x) {
            if(x == resolution) {
                x = 0;
                z += 1;
            }
            Transform point = Instantiate(pointPrefab);
            position.x = ((i + 0.5f) * step) - 5f;
            position.z = ((z + 0.5f) * step) - 5f;
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
        ZGraphFunction f = functions[(int)activeFunction];

        for(int i = 0, x = 0; i < points.Length; ++i, ++x) {
            if(x == resolution) {
                x = 0;
            }
            Transform point = points[i].point;
            Vector3 position = point.localPosition;
            if(activeFunction == 0) {
                point.localPosition = f(position, resolution, points[i].time, c1, c2, x);
            }
            else {
                point.localPosition = f(position, resolution, time, c1, c2, x);
            }
            points[i].point = point;
            points[i].time += 0.02f;
            if(points[i].time > 10) {
                points[i].time = 0;
            }
        }
    }

    public static Vector3 SpiralDiffEq(Vector3 position, int resolution, float time, float c1, float c2, int i) {
        float e = 1 * Mathf.Exp(-time);
        position.x = ((c1 * e) * ((1 * Mathf.Cos(Mathf.PI * time)) - Mathf.Sin(Mathf.PI * time))) + ((c2 * e) * (Mathf.Cos(Mathf.PI * time) + (1 * Mathf.Sin(Mathf.PI * time))));
        position.y = ((c1 * e) * (1 * Mathf.Cos(Mathf.PI * time))) + ((c2 * e) * (1 * Mathf.Sin(Mathf.PI * time)));

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