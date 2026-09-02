using UnityEngine;

public class EnemySoundEffects : MonoBehaviour
{
    public static EnemySoundEffects Instance;
    public AudioClip[] enemyHurt;
    public AudioClip[] torchSwingEffect;

    public AudioClip GetRandomHurt()
    {
        return enemyHurt[Random.Range(0, enemyHurt.Length)];
    }

    public AudioClip GetTrochSwing()
    {
        return torchSwingEffect[Random.Range(0, torchSwingEffect.Length)];
    }
}
