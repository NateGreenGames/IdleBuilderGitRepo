using System.Collections;
using UnityEngine;

public class GameTickManager : MonoBehaviour
{
    [SerializeField] int ticksPerSecond;
    public delegate void action();
    public static event action OnTick;

    private float timeBetweenTicks;

    private void Start()
    {
        timeBetweenTicks = 60f / ticksPerSecond;
        StartCoroutine(TickRoutine());
    }

    private IEnumerator TickRoutine()
    {
        yield return new WaitForSeconds(timeBetweenTicks);
        if(OnTick != null)
        {
            OnTick.Invoke();
        }
        StartCoroutine(TickRoutine());
    }
}
