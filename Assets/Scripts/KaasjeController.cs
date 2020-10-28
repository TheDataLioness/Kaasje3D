using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KaasjeController : MonoBehaviour
{

    public float rotateSpeed = 1;
    public Color particleColor = Color.yellow;
    
    private ParticleSystem system;

    // Start is called before the first frame update
    void Start()
    {
        
        
        // A simple particle material with no texture.
        Material particleMaterial = new Material(Shader.Find("Particles/Standard Unlit"));
        particleMaterial.color = particleColor;

        // Create a Particle System.
        var go = new GameObject("Particle System");
        go.transform.position = transform.position;
        go.transform.localScale /= 10;
        go.transform.Rotate(-90, 0, 0);
        system = go.AddComponent<ParticleSystem>();
        go.GetComponent<ParticleSystemRenderer>().material = particleMaterial; 
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, rotateSpeed));
    }

    private void OnDestroy()
    {
        Destroy(system);
    }

}
