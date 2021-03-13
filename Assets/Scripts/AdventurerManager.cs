using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AdventurerManager : MonoBehaviour
{
    [SerializeField] private List<Holder> potionSlots;
    public List<Command> commands;

    private float tickPeriod = 3f;
    private float nextTick;

    [SerializeField] private float potionUsageChance = 0.03f;
    [SerializeField] private float potionUseTimeMarginSeconds = 10;

    private void Awake()
    {
        nextTick = Time.time + tickPeriod;
    }
    
    private void Update()
    {
        if ((Time.time) >= nextTick)
        {
            ExecuteCommand();
            nextTick = Time.time + tickPeriod;
        }
    }

    private void ExecuteCommand()
    {
        var commandIndex = Random.Range(0, commands.Count);
        commands[commandIndex].execute();
    }
}
