﻿using System;
using UnityEngine;

// 생명체로서 동작할 게임 오브젝트들을 위한 뼈대를 제공
// 체력, 데미지 받아들이기, 사망 기능, 사망 이벤트를 제공
public class LivingEntity : MonoBehaviour, IDamageable {
    public float startHP = 100f; // 시작 체력
    public float HP; // 현재 체력
    public bool dead { get; protected set; } // 사망 상태
    public event Action onDeath; // 사망시 발동할 이벤트

    protected virtual void OnEnable() {
        dead = false;
        HP = startHP;
    }

    // 데미지를 입는 기능
    public virtual void OnDamage(float damage, bool isCrit, Vector3 hitPoint, Vector3 hitNormal, Collider hitCollider) {
        // 데미지만큼 체력 감소
        HP -= damage;
        if(HP<0) HP = 0;
        // 체력이 0 이하 && 아직 죽지 않았다면 사망 처리 실행
        if (HP == 0 && !dead)
        {
            Die();
        }
    }

    // 체력을 회복하는 기능
    public virtual void RestoreHealth(float newHealth) {
        if (dead)
        {
            return;
        }
        HP += newHealth;
    }

    // 사망 처리
    public virtual void Die() {
        // onDeath 이벤트에 등록된 메서드가 있다면 실행
        if (onDeath != null)
        {
            onDeath();
        }

        // 사망 상태를 참으로 변경
        dead = true;
    }
}