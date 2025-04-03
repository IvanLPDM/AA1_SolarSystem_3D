using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SceneManager : MonoBehaviour
{
    public float timeScale = 1f;
    private float elapsedTime = 0;

    public float Orbit_width;

    public List<Camera> cameras;
    private int camera_num;

    public List<Planet> attractors;

    public bool real_size;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            if (i <= 0) 
            {
                cameras[0].gameObject.SetActive(true);
            }
            else
                cameras[i].gameObject.SetActive(false);
        }
        


    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            timeScale *= 10f;
        }

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            timeScale *= 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCameras();
        }

        if (real_size)
        {
            foreach (Planet body in attractors)
            {
                body.transform.localScale = body.realSize;
                //body.lineRenderer.SetWidth(0.0005f, 0.0005f);
            }
        }
        else
        {
            foreach (Planet body in attractors)
            {
                body.transform.localScale = body.size_Simulation;
                //body.lineRenderer.SetWidth(0.05f, 0.05f);
            }
        }


        

        elapsedTime += Time.deltaTime * timeScale;
        Contador(elapsedTime);
    }

    void SwitchCameras()
    {
        for(int i = 0; i < cameras.Count; i++)
        {
            if (cameras[i].gameObject.activeSelf && i!=0)
            {
                cameras[i].gameObject.SetActive(false);
                cameras[i - 1].gameObject.SetActive(true);
            }
            else if(cameras[i].gameObject.activeSelf && i == 0)
            {
                cameras[i].gameObject.SetActive(false);
                cameras[cameras.Count - 1].gameObject.SetActive(true);
            }

        }
        
    }

    private void Contador(float scaledDeltaTime)
    {
        // Aumentar el tiempo transcurrido por el deltaTime (el tiempo entre fotogramas).
        scaledDeltaTime += Time.deltaTime;

        // Calcular el tiempo transcurrido en a�os, meses, d�as, horas, minutos y segundos.
        int years = Mathf.FloorToInt(scaledDeltaTime / (365.25f * 24f * 60f * 60f)); // 1 a�o = 365.25 d�as
        int months = Mathf.FloorToInt((scaledDeltaTime % (365.25f * 24f * 60f * 60f)) / (30f * 24f * 60f * 60f)); // Promedio de 30 d�as por mes
        int days = Mathf.FloorToInt((scaledDeltaTime % (30f * 24f * 60f * 60f)) / (24f * 60f * 60f)); // Un d�a tiene 86400 segundos
        int hours = Mathf.FloorToInt((scaledDeltaTime % (24f * 60f * 60f)) / (60f * 60f));
        int minutes = Mathf.FloorToInt((scaledDeltaTime % (60f * 60f)) / 60f);
        int seconds = Mathf.FloorToInt(scaledDeltaTime % 60f);

        // Reseteamos las unidades a sus valores apropiados si alcanzan el l�mite
        if (months >= 12)
        {
            years += months / 12; // A�adimos los a�os al contador de a�os
            months = months % 12; // Restablecemos los meses a un valor entre 0 y 11
        }

        if (days >= 30)
        {
            months += days / 30; // A�adimos los meses al contador de meses
            days = days % 30; // Restablecemos los d�as a un valor entre 0 y 29
        }

        if (hours >= 24)
        {
            days += hours / 24; // A�adimos los d�as al contador de d�as
            hours = hours % 24; // Restablecemos las horas a un valor entre 0 y 23
        }

        if (minutes >= 60)
        {
            hours += minutes / 60; // A�adimos las horas al contador de horas
            minutes = minutes % 60; // Restablecemos los minutos a un valor entre 0 y 59
        }

        if (seconds >= 60)
        {
            minutes += seconds / 60; // A�adimos los minutos al contador de minutos
            seconds = seconds % 60; // Restablecemos los segundos a un valor entre 0 y 59
        }

        // Imprimir el tiempo en formato a�os, meses, d�as, horas, minutos y segundos.
        Debug.Log(string.Format("Tiempo transcurrido: {0} a�os, {1} meses, {2} d�as, {3} horas, {4} minutos, {5} segundos",
            years, months, days, hours, minutes, seconds));
    }
}
