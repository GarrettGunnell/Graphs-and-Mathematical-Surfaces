using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Point {
    public Transform point;
    public float time;

}

public class Graph : MonoBehaviour {

    Vector3 SpiralDiffEq(Vector3 position, float time, float c1, float c2) {
        float e = 1 * Mathf.Exp(-time);
        position.x = ((c1 * e) * ((1 * Mathf.Cos(Mathf.PI * time)) - Mathf.Sin(Mathf.PI * time))) + ((c2 * e) * (Mathf.Cos(Mathf.PI * time) + (1 * Mathf.Sin(Mathf.PI * time))));
        position.y = ((c1 * e) * (1 * Mathf.Cos(Mathf.PI * time))) + ((c2 * e) * (1 * Mathf.Sin(Mathf.PI * time)));

        return position;
    }

    Vector3 SineFunction(Vector3 position, float time, float c1, float c2) {
        position.y = Mathf.Sin(Mathf.PI * (position.x + Time.time) * c1) * c2;

        return position;
    }

    public Transform pointPrefab;
    [Range(10, 500)] public int resolution = 10;
    [Range(-20, 20)] public float c1 = 0;
    [Range(-20, 20)] public float c2 = 0;
    [Range(0, 1)] public int activeFunction;
    Point[] points;

    void Awake() {
        float time = 0f;
        points = new Point[resolution];
        float step = (2f / resolution) * 5f;
        Vector3 scale = Vector3.one * step;
        Vector3 position;
        position.y = 0f;
        position.z = 0f;
        for (int i = 0; i < resolution; ++i) {
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

        for (int i = 0; i < resolution; ++i) {
            Transform point = points[i].point;
            Vector3 position = point.localPosition;
            if (activeFunction == 0) {
                position.x = ((i + 0.5f) * (2f / resolution) * 5f) - 5f;
                point.localPosition = SineFunction(position, time, c1, c2);
            } else {
                point.localPosition = SpiralDiffEq(position, points[i].time, c1, c2);
            }
            points[i].point = point;
            points[i].time += 0.02f;
            if (points[i].time > 10) {
                points[i].time = 0;
            }
        }
    }
}