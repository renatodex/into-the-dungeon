﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMenuUI : MonoBehaviour
{
    [SerializeField] BattleUnit target;
    [SerializeField] Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = BattleSystem.Instance.GetSelectedUnit().transform.position + offset;
    }
}
