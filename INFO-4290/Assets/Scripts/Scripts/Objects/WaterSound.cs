using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSound : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float radius;
    [SerializeField] private float threshold;
    [SerializeField] private float soundFloor;
    [SerializeField] private float soundCieling;

    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(FindObjectOfType<PlayerController>().transform.position, this.transform.position) < radius)
        {
            distance = Vector2.Distance(FindObjectOfType<PlayerController>().transform.position, this.transform.position);
            distance = (radius/distance) - threshold;
            if (distance < soundFloor)
            {
                distance = soundFloor;
            }
            else if (distance > soundCieling)
            {
                distance = soundCieling;
            }
            Sound s = FindObjectOfType<AudioManager>().getSource("water");
            FindObjectOfType<AudioManager>().changeVolumePitch(s, distance, 1);
            if (!FindObjectOfType<AudioManager>().isPlaying("water"))
            {
                FindObjectOfType<AudioManager>().PlayWithSettings("water", distance, 1);
            }

        }
        else
        {
            Sound s = FindObjectOfType<AudioManager>().getSource("water");
            if (s.source.isPlaying && s!=null)
                s.source.Stop();
        }
    }
    private void OnDisable()
    {
        Sound s = FindObjectOfType<AudioManager>().getSource("water");
        if (s.source.isPlaying && s != null)
            s.source.Stop();
    }
}
