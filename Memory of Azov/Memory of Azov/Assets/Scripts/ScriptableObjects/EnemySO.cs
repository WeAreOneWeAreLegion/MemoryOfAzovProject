﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_Data", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemySO : ScriptableObject
{
    [Header("\tGame Designers Variables")]

    [Header("Type Of Ghost")]
    [Tooltip("Color del fantasma. Neutral = Blanco, Secondary = Verde, Third = Rojo")]
    public PlayerController.LightColor ghostColor;

    [Header("Movement Variables")]
    [Tooltip("Velocidad a la que se mueve el personaje")]
    [Range(0, 500)] public float speed = 100f;
    [Tooltip("Velocidad a la que rota el personaje")]
    [Range(0, 360)] public float rotationSpeed = 120f;
    [Tooltip("Velocidad a la que oscila el personaje")]
    [Range(0, 400)] public float oscilationSpeed = 40;
    [Tooltip("Cuantas veces ira mas rapido mientras lo enfocan")]
    [Range(0, 10)] public float speedFactorWhenLightened = 2;
    [Tooltip("Cantidad de veces que se mueve de lado a lado por segundo")]
    [Range(0, 10)] public float oscilationsPerSecond = 1f;
    [Tooltip("Longitud de la oscilacion de lado a lado (Linea verde)")]
    [Range(0, 10)] public float oscilationAmplitude = 2;
    [Tooltip("Mira automaticamente al objectivo al qual esta dirigiendose si esta activado")]
    public bool immediateFacing;
    [Tooltip("Se mueve oscilatoriamente si esta activado")]
    public bool oscillationMovement = true;

    [Header("Health Variables")]
    [Tooltip("Vida inicia del personaje")] public int initialHp = 100;
    [Tooltip("Tiempo que el personaje estara aturdido si lo aturden")]
    [Range(0, 10)] public float timeStuned = 2f;

    [Header("Attack Variables")]
    [Tooltip("Valor del ataque del personaje")]
	[Range(0, 100)] public int ghostDamage = 20;
    [Tooltip("Radio de ataque del personaje")]
    [Range(0, 20)] public float attackRadius = 2f;
    [Tooltip("Tiempo entre ataques")]
    [Range(0, 10)] public float attackDelay = 2f;

}