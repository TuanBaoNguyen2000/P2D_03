using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageReceiver : MonoBehaviour
{
    [Header("Damage Receiver")]
    [SerializeField] protected bool isDead = false;
    [SerializeField] protected int hp = 1;
    public int HP => hp;

    [SerializeField] protected int hpMax = 2;
    public int HPMax => hpMax;

    private void OnEnable()
    {
        this.Reborn();
    }

    public virtual void Reborn()
    {
        this.hp = this.hpMax;
        this.isDead = false;
    }

    public virtual void Add(int add)
    {
        if (this.isDead) return;

        this.hp += add;
        if (this.hp > this.hpMax) this.hp = this.hpMax;
    }

    public virtual void Deduct(int deduct)
    {
        if (this.isDead) return;

        this.hp -= deduct;
        if (this.hp < 0) this.hp = 0;
        this.CheckIsDead();
    }

    protected virtual bool IsDead()
    {
        return this.hp <= 0;
    }

    protected virtual void CheckIsDead()
    {
        if (!this.IsDead()) return;
        this.isDead = true;
        this.OnDead();
    }

    protected abstract void OnDead();
}
