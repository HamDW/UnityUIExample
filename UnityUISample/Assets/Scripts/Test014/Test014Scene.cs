﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test014Scene : MonoBehaviour
{
    [SerializeField] GameObject m_Sphere = null;

    [SerializeField] Slider m_sliderR = null;
    [SerializeField] Slider m_sliderG = null;
    [SerializeField] Slider m_sliderB = null;
    [SerializeField] Slider m_sliderA = null;

    [SerializeField] InputField m_editR = null;
    [SerializeField] InputField m_editG = null;
    [SerializeField] InputField m_editB = null;
    [SerializeField] InputField m_editA = null;


    private Material m_SphereMaterial = null;
    private Color m_CurColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer kRenderer = m_Sphere.GetComponent<MeshRenderer>();
        m_SphereMaterial = kRenderer.material;

        InitSlider();
        m_CurColor = m_SphereMaterial.color;
    }

    public void InitSlider()
    {
        m_sliderR.minValue = 0;
        m_sliderR.maxValue = 1;
        m_sliderG.minValue = 0;
        m_sliderG.maxValue = 1;
        m_sliderB.minValue = 0;
        m_sliderB.maxValue = 1;
        m_sliderA.minValue = 0;
        m_sliderA.maxValue = 1;

        m_sliderR.value = 1;
        m_sliderG.value = 1;
        m_sliderB.value = 1;
        m_sliderA.value = 1;
    }


    public void OnSliderChanged_R()
    {
        m_CurColor.r = m_sliderR.value;
        m_SphereMaterial.color = m_CurColor;
        m_editR.text = "" + (int)(m_CurColor.r * 255); 
    }

    public void OnSliderChanged_G()
    {
        m_CurColor.g = m_sliderG.value;
        m_SphereMaterial.color = m_CurColor;
        m_editG.text = "" + (int)(m_CurColor.g * 255);
    }
    public void OnSliderChanged_B()
    {
        m_CurColor.b = m_sliderB.value;
        m_SphereMaterial.color = m_CurColor;
        m_editB.text = "" + (int)(m_CurColor.b * 255);
    }
    public void OnSliderChanged_A()
    {
        m_CurColor.a = m_sliderA.value;
        m_SphereMaterial.color = m_CurColor;
        m_editA.text = "" + (int)(m_CurColor.a * 255);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
