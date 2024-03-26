using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Events
{
    public static readonly Evt<float> onHpLostCell = new Evt<float>();
    public static readonly Evt<Enemy, float> onHpLostEnemy = new Evt<Enemy, float>();
    public static readonly Evt<BossEnemy, float> onHpLostBoss = new Evt<BossEnemy, float>();
    public static readonly Evt<float> onDNAGained = new Evt<float>();
    public static readonly Evt<int> onSlotClicked = new Evt<int>();
    public static readonly Evt<Medicine> onMedicineCollected = new Evt<Medicine>();

    public static readonly Evt<BossEnemy> onBossSpawn = new Evt<BossEnemy>();
    public static readonly Evt<BossEnemy> onBossDeath = new Evt<BossEnemy>();
    public static readonly Evt<Enemy> onEnemyDeath = new Evt<Enemy>();
    public static readonly Evt<float> onLevelUp = new Evt<float>();
    public static readonly Evt<Skill_LineProjectile, Enemy> onProjectileHitEnemy = new Evt<Skill_LineProjectile, Enemy>();
    public static readonly Evt<Skill_SpinningAround, Enemy> onMineHitEnemy = new Evt<Skill_SpinningAround, Enemy>();

}
public class Evt
{
    private event Action _action = delegate { };

    public void Invoke() => _action.Invoke();
    public void AddListener(Action listener) => _action += listener;
    public void RemoveListener(Action listener) => _action -= listener;
}
public class Evt<T>
{
    private event Action<T> _action = delegate { };

    public void Invoke(T param) => _action.Invoke(param);
    public void AddListener(Action<T> listener) => _action += listener;
    public void RemoveListener(Action<T> listener) => _action -= listener;
}

public class Evt<T0, T1>
{
    private event Action<T0, T1> _action = delegate { };

    public void Invoke(T0 param1, T1 param2) => _action.Invoke(param1, param2);
    public void AddListener(Action<T0, T1> listener) => _action += listener;
    public void RemoveListener(Action<T0, T1> listener) => _action -= listener;
}
