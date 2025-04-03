using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Planet : MonoBehaviour
{
    public float mass; 
    public Vector3 initialSpeed; 
    public Vector3 velocity;

    public List<Planet> attractors; 

    const float G = 6.67430e-11f; 
    const float Gastronomic = 39.478f;


    public SceneManager sceneManager;


    public LineRenderer lineRenderer;
    public Color orbit_Color;
    private List<Vector3> positions = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
    {
        // Inicializar la velocidad con la velocidad inicial
        velocity = initialSpeed;

        lineRenderer.SetColors(orbit_Color, orbit_Color);
        lineRenderer.SetWidth(0.05f, 0.05f);
        lineRenderer.positionCount = 0;
    }

    

    // Aplicar la gravedad con el método de Euler
    //public void ApplyGravityEuler(float deltaTime)
    //{
    //    // Dirección de la fuerza gravitatoria 
    //    Vector3 direction = sun.transform.position - transform.position;

    //    // Distancia entre el planeta y el sol
    //    float distance = direction.magnitude;

    //    // Calcular la magnitud de la fuerza gravitatoria
    //    float forceMagnitude = G * (sun.mass * mass) / (distance * distance);

    //    // Normalizar la dirección para obtener la fuerza en vector
    //    Vector3 force = direction.normalized * forceMagnitude;

    //    // Aceleración (F = ma → a = F/m)
    //    Vector3 acceleration = force / mass;

    //    // Actualizar la velocidad usando el método de Euler
    //    velocity += acceleration * deltaTime;
    //}


    Vector3 GravitationalAcceleration(Vector3 position)
    {
        Vector3 totalAcceleration = Vector3.zero;

        foreach (Planet body in attractors)
        {
            if (body != this) // Evitar que un planeta se atraiga a sí mismo
            {
                Vector3 direction = body.transform.position - position;
                float distance = direction.magnitude;
                float forceMagnitude = G * (body.mass * mass) / (distance * distance);
                totalAcceleration += direction.normalized * forceMagnitude / mass;
            }
        }

        return totalAcceleration;
    }

    // Método de Runge-Kutta de 4to orden
    public void ApplyGravityRK4(float deltaTime)
    {
        // k1
        Vector3 k1v = GravitationalAcceleration(transform.position);
        Vector3 k1x = velocity;

        // k2
        Vector3 k2v = GravitationalAcceleration(transform.position + k1x * deltaTime / 2);
        Vector3 k2x = velocity + k1v * deltaTime / 2;

        // k3
        Vector3 k3v = GravitationalAcceleration(transform.position + k2x * deltaTime / 2);
        Vector3 k3x = velocity + k2v * deltaTime / 2;

        // k4
        Vector3 k4v = GravitationalAcceleration(transform.position + k3x * deltaTime);
        Vector3 k4x = velocity + k3v * deltaTime;

        // Actualizar la posición y velocidad con Runge-Kutta
        velocity += (k1v + 2 * k2v + 2 * k3v + k4v) * deltaTime / 6;
        transform.position += (k1x + 2 * k2x + 2 * k3x + k4x) * deltaTime / 6;
    }

    // Update is called once per frame
    void Update()
    {
        float scaledDeltaTime = Time.deltaTime * sceneManager.timeScale;

        // Aplicar la gravedad y actualizar la velocidad
        //ApplyGravityEuler(scaledDeltaTime);
        //transform.position += velocity * scaledDeltaTime;

        ApplyGravityRK4(scaledDeltaTime);



        // Guardar la posición actual de la Tierra para la trayectoria
        positions.Add(transform.position);

        // Actualizar el LineRenderer con las nuevas posiciones
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());

    }

    
}
