using System.Collections.Generic;
using UnityEngine;

public class CannonControl : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    
    [SerializeField]
    private SliderController sliderController;
    
    [SerializeField]
    private Projectile fishTemplate;

    [SerializeField]
    private Transform instantiationPoint;

    private List<Projectile> projectiles = new List<Projectile>();

    [SerializeField] private AudioSource cannonAudio;
    
    public void Shoot()
    {
        var aimPosition = target.position;
        var time  = sliderController.TimeInSeconds;
        
        var initialVelocity = CalculateInitialVelocity(transform.position, aimPosition, Physics2D.gravity, time);
        var clone = Instantiate(fishTemplate, transform.position, Quaternion.identity);
        projectiles.Add(clone);
        clone.SetInitialVelocity(initialVelocity);
        cannonAudio.Play();
    }

    private static Vector2 CalculateInitialVelocity(Vector2 start, Vector2 end, Vector2 acceleration, float time)
    {
        /* 𝑠 = 𝑢𝑡 + 0.5 𝑎𝑡^2
         * ut = s - 0.5 at^2
         * u = s/t - 0.5 at
         */
        Vector2 distance = end - start;
        Vector2 u = (distance / time) - (0.5f * acceleration * time);
        return u;
    }
}
