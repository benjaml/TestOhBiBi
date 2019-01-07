using UnityEngine;

public class AttackSoundHandler : MonoBehaviour
{
    [SerializeField]
    private AudioClip _meleeAttack;
    [SerializeField]
    private AudioClip _rangeAttack;

    private AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public void PlayMeleeAttackSound()
    {
        _source.PlayOneShot(_meleeAttack);
    }

    public void PlayRangeAttackSound()
    {
        _source.PlayOneShot(_rangeAttack);
    }
}
