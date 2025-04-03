using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public float timeScale = 1f;
    private float elapsedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

        elapsedTime += Time.deltaTime * timeScale;
        Contador(elapsedTime);
    }

    private void FixedUpdate()
    {
        

        
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
