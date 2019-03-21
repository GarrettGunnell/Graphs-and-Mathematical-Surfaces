using UnityEngine;


public delegate Vector3 ZGraphFunction(Vector3 position, int resolution, float time, float u, float v, float c1, float c2);

public enum ZGraphFunctionName {
    SineFunction,
    DoubleSineFunction,
    SineTimes2Function,
    WildinSineFunction,
    SineXZFunction,
    SineXYZFunction,
    TanFunction,
    TanSineFunction,
    Ripple,
    FlowCylinder,
    SpiralCylinder,
    Spiral,
    VerticalSine,
    Sphere,
    WildinSphere
}

public class ZGraph : MonoBehaviour {

    public Transform pointPrefab;
    [Range(10, 500)] public int resolution = 10;
    [Range(1, 5)] public int size;
    [Range(-20, 20)] public float c1 = 0;
    [Range(-20, 20)] public float c2 = 0;
    public ZGraphFunctionName activeFunction;
    Transform[] points;
    static ZGraphFunction[] functions = {
        SineFunction, DoubleSineFunction, SineTimes2Function, WildinSineFunction, SineXZFunction, SineXYZFunction, TanFunction, TanSineFunction, Ripple, FlowCylinder, SpiralCylinder, Spiral, VerticalSine,
        Sphere, WildinSphere
    };


    void Start() {
        points = new Transform[resolution * resolution];
        float step = (2f / resolution) * size;
        Vector3 scale = Vector3.one * step;
        Vector3 position;
        position.x = 0f;
        position.y = 0f;
        position.z = 0f;
        for(int i = 0, z = 0; z < resolution; ++z) {
            for(int x = 0; x < resolution; ++x, ++i) {
                Transform point = Instantiate(pointPrefab);
                point.localPosition = position;
                point.localScale = scale;
                point.SetParent(transform, false);
                points[i] = point;
            }
        }
    }

    void Update() {

        float time = Time.time;
        ZGraphFunction f = functions[(int)activeFunction];
        float step = (2f / resolution) * size;
        Vector3 scale = Vector3.one * step;

        for(int i = 0, z = 0; z < resolution; ++z) {
            float v = ((z + 0.5f) * step) - size;
            for(int x = 0; x < resolution; ++x, ++i) {
                float u = ((x + 0.5f) * step) - size;
                Transform point = points[i];
                Vector3 position = point.localPosition;
                point.localScale = scale;
                point.localPosition = f(position, resolution, time, u, v, c1, c2);
                points[i] = point;
            }
        }
    }

    public static Vector3 SineFunction(Vector3 position, int resolution, float time, float u, float v, float c1, float c2) {
        position.x = u;
        position.z = v;
        position.y = Mathf.Sin(Mathf.PI * (position.x + position.z + time) * c1) * c2;
        position.y *= 2f / 3f;

        return position;
    }

    public static Vector3 DoubleSineFunction(Vector3 position, int resolution, float time, float u, float v, float c1, float c2) {
        position.x = u;
        position.z = v;
        position.y = Mathf.Sin(Mathf.PI * (position.x + position.z + time) * c1) * c2;
        position.y += Mathf.Sin(2f * Mathf.PI * (position.x + position.z + time)) / 2f;
        position.y *= 2f / 3f;

        return position;
    }

    public static Vector3 SineTimes2Function(Vector3 position, int resolution, float time, float u, float v, float c1, float c2) {
        position.x = u;
        position.z = v;
        position.y = Mathf.Sin(Mathf.PI * (position.x + position.z + time) * c1) * c2;
        position.y += Mathf.Sin(2f * Mathf.PI * ((position.x + position.z) + 2f * time)) / 2f;
        position.y *= 2f / 3f;

        return position;
    }

    public static Vector3 WildinSineFunction(Vector3 position, int resolution, float time, float u, float v, float c1, float c2) {
        position.x = u;
        position.z = v;
        position.y = Mathf.Sin(Mathf.PI * (position.x + time) * c1) * c2;
        position.y += Mathf.Sin(2f * Mathf.PI * ((position.x + position.z) * time)) / 2f;
        position.y *= 2f / 3f;

        return position;
    }

    public static Vector3 SineXZFunction(Vector3 position, int resolution, float time, float u, float v, float c1, float c2) {
        position.x = u;
        position.z = v;
        position.y = Mathf.Sin(Mathf.PI * (position.x + time) * c1) * c2;
        position.y += Mathf.Sin(Mathf.PI * (position.z + time) * c1) * c2;
        position.y *= 2f / 3f;

        return position;
    }

    public static Vector3 SineXYZFunction(Vector3 position, int resolution, float time, float u, float v, float c1, float c2) {
        position.x = u;
        position.z = v;
        position.y = Mathf.Sin(Mathf.PI * (position.x + time) * c1) * c2;
        position.y += Mathf.Sin(Mathf.PI * (position.z + time) * c1) * c2;
        position.y += Mathf.Sin(Mathf.PI * (position.y + time) * c1) * c2;
        position.y *= 2f / 3f;

        return position;
    }

    public static Vector3 TanSineFunction(Vector3 position, int resolution, float time, float u, float v, float c1, float c2) {
        position.x = u;
        position.z = v;
        position.y = Mathf.Tan(Mathf.PI * (position.x + position.z + time) * c1) * c2;
        position.y += Mathf.Sin(2f * Mathf.PI * (position.x + position.z + time)) / 2f;
        position.y *= 2f / 3f;

        return position;
    }

    public static Vector3 TanFunction(Vector3 position, int resolution, float time, float u, float v, float c1, float c2) {
        position.x = u;
        position.z = v;
        position.y = Mathf.Tan(Mathf.PI * (position.x + position.z + time) * c1) * c2;

        return position;
    }

    public static Vector3 Ripple(Vector3 position, int resolution, float time, float u, float v, float c1, float c2) {
        float d = Mathf.Sqrt(u * u + v * v);
        position.x = u;
        position.z = v;
        position.y = Mathf.Sin(Mathf.PI * (4f * d - time) * c1) * c2;
        position.y /= 1f + 5f * d;

        return position;
    }

    public static Vector3 FlowCylinder(Vector3 position, int resolution, float time, float u, float v, float c1, float c2) {
        c2 = 1f + Mathf.Sin(c2 * Mathf.PI * (v + time)) * 0.2f;
        position.x = Mathf.Sin(Mathf.PI * (u) * c1) * c2;
        position.y = v;
        position.z = Mathf.Cos(Mathf.PI * (u) * c1) * c2;

        return position;
    }

    public static Vector3 SpiralCylinder(Vector3 position, int resolution, float time, float u, float v, float c1, float c2) {
        c2 = 1f + Mathf.Sin(c2 * Mathf.PI * (u + v + time)) * 0.2f;
        position.x = Mathf.Sin(Mathf.PI * (u) * c1) * c2;
        position.y = v;
        position.z = Mathf.Cos(Mathf.PI * (u) * c1) * c2;

        return position;
    }

    public static Vector3 Spiral(Vector3 position, int resolution, float time, float u, float v, float c1, float c2) {
        position.x = Mathf.Sin(Mathf.PI * (u + time) * c1) * c2;
        position.y = u;
        position.z = Mathf.Cos(Mathf.PI * (u + time) * c1) * c2;

        return position;
    }

    public static Vector3 VerticalSine(Vector3 position, int resolution, float time, float u, float v, float c1, float c2) {
        position.x = Mathf.Sin(Mathf.PI * (u + time) * c1) * c2;
        position.y = v;
        position.z = Mathf.Cos(Mathf.PI * (v + time) * c1) * c2;

        return position;
    }

    public static Vector3 Sphere(Vector3 position, int resolution, float time, float u, float v, float c1, float c2) {
        float r = Mathf.Cos(Mathf.PI * 0.5f * v);
        position.x = Mathf.Sin(Mathf.PI * (u) * c1) * r * c2;
        position.y = Mathf.Sin(Mathf.PI * 0.5f * v);
        position.z = Mathf.Cos(Mathf.PI * (u) * c1) * r * c2;

        return position;
    }

    public static Vector3 WildinSphere(Vector3 position, int resolution, float time, float u, float v, float c1, float c2) {
        float r = 0.8f + Mathf.Sin(Mathf.PI * (6f * u + time)) * 0.1f;
        r += Mathf.Sin(Mathf.PI * (4f * v + time)) * 0.1f;
        float s = Mathf.Cos(Mathf.PI * 0.5f * v) * r;
        position.x = Mathf.Sin(Mathf.PI * (u) * c1) * s * c2;
        position.y = Mathf.Sin(Mathf.PI * 0.5f * v) * r;
        position.z = Mathf.Cos(Mathf.PI * (u) * c1) * s * c2;

        return position;
    }
}