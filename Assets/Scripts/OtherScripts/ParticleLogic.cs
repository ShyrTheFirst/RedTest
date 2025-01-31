using UnityEngine;

public class ParticleLogic : MonoBehaviour
{
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        GameManager.Instance.RegisterEvent("TriggerParticle", OnEventCall);
    }
    private void OnEventCall(object data)
    {
        ps.Emit(1);
    }
}
