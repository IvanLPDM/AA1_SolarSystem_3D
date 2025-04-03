using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SceneManager : MonoBehaviour
{
    public float timeScale = 1f;
    private float elapsedTime = 0;

    public float Orbit_width;

    public List<Camera> cameras;
    private int camera_num;

    public List<Planet> planets;

    public bool real_size;
    public bool sun_move;

    public TextMeshProUGUI timer;

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

        if(timeScale >= 10000000)
        {
            timeScale = 10000000;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCameras();
        }

        if (real_size)
        {
            foreach (Planet body in planets)
            {
                body.transform.localScale = body.realSize;
                //body.lineRenderer.SetWidth(0.0005f, 0.0005f);
            }
        }
        else
        {
            foreach (Planet body in planets)
            {
                body.transform.localScale = body.size_Simulation;
                //body.lineRenderer.SetWidth(0.05f, 0.05f);
            }
        }

        if(sun_move)
        {
            planets[0].velocity += new Vector3(0, 1e-10f, 0);
        }
        

        elapsedTime += Time.deltaTime * timeScale;
        

        timer.SetText(Contador(elapsedTime));
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

    //chat gpt
    private string Contador(float scaledDeltaTime)
    {
       
        scaledDeltaTime += Time.deltaTime;

        
        int years = Mathf.FloorToInt(scaledDeltaTime / (365.25f * 24f * 60f * 60f)); 
        int months = Mathf.FloorToInt((scaledDeltaTime % (365.25f * 24f * 60f * 60f)) / (30f * 24f * 60f * 60f)); 
        int days = Mathf.FloorToInt((scaledDeltaTime % (30f * 24f * 60f * 60f)) / (24f * 60f * 60f)); 
        int hours = Mathf.FloorToInt((scaledDeltaTime % (24f * 60f * 60f)) / (60f * 60f));
        int minutes = Mathf.FloorToInt((scaledDeltaTime % (60f * 60f)) / 60f);
        int seconds = Mathf.FloorToInt(scaledDeltaTime % 60f);


        if (months >= 12)
        {
            years += months / 12; 
            months = months % 12; 
        }

        if (days >= 30)
        {
            months += days / 30; 
            days = days % 30;
        }

        if (hours >= 24)
        {
            days += hours / 24; 
            hours = hours % 24;
        }

        if (minutes >= 60)
        {
            hours += minutes / 60; 
            minutes = minutes % 60; 
        }

        if (seconds >= 60)
        {
            minutes += seconds / 60; 
            seconds = seconds % 60; 
        }

       
        Debug.Log(string.Format("Tiempo transcurrido: {0} años, {1} meses, {2} días, {3} horas, {4} minutos, {5} segundos",
            years, months, days, hours, minutes, seconds));

        return string.Format("Tiempo transcurrido: {0} años, {1} meses, {2} días, {3} horas, {4} minutos, {5} segundos",
            years, months, days, hours, minutes, seconds);
    }
}
