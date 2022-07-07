using UnityEngine;
using System.Collections.Generic;

namespace UnityEngine.AzureSky
{
    public class AzureEffectsController : MonoBehaviour
    {

        #if UNITY_EDITOR
        [SerializeField] private bool m_referencesHeaderGroup;
        [SerializeField] private bool m_settingsHeaderGroup;
        [SerializeField] private bool m_thunderSettingsHeaderGroup;
        [SerializeField] private bool m_enableParticlesTriggerModule;
        [SerializeField] private bool m_enableParticlesSubEmittersModule;
        #endif

        // References
        public WindZone windZone;
        public AudioSource lightRainAudioSource;
        public AudioSource mediumRainAudioSource;
        public AudioSource heavyRainAudioSource;
        public AudioSource lightWindAudioSource;
        public AudioSource mediumWindAudioSource;
        public AudioSource heavyWindAudioSource;
        public Material defaultRainMaterial;
        public Material heavyRainMaterial;
        public Material snowMaterial;
        public Material rippleMaterial;
        public ParticleSystem lightRainParticle;
        public ParticleSystem mediumRainParticle;
        public ParticleSystem heavyRainParticle;
        public ParticleSystem snowParticle;
		[SerializeField] private Collider m_particlesTriggerCollider;

		// Settings
		public float lightRainIntensity = 0.0f;
        public float mediumRainIntensity = 0.0f;
        public float heavyRainIntensity = 0.0f;
        public float snowIntensity = 0.0f;
        public Color rainColor = Color.white;
        public Color snowColor = Color.white;
        public float lightRainSoundFx = 0.0f;
        public float mediumRainSoundFx = 0.0f;
        public float heavyRainSoundFx = 0.0f;
        public float lightWindSoundFx = 0.0f;
        public float mediumWindSoundFx = 0.0f;
        public float heavyWindSoundFx = 0.0f;
        public float windSpeed = 0.0f;
        public float windDirection = 0.0f;

		private ParticleSystem.EmissionModule m_particleEmission;
		private ParticleSystem.TriggerModule m_particleTrigger;
		private ParticleSystem.SubEmittersModule m_particleSubEmitters;

		// Thunders
		public List<AzureThunderSettings> thunderSettingsList = new List<AzureThunderSettings>();

		private void Start()
		{
			UpdateParticlesMaterials();
		}

		private void Update()
		{
			UpdateParticlesMaterials();
			if (Application.isPlaying)
			{
				SoundEffectController(lightRainSoundFx, lightRainAudioSource);
				SoundEffectController(mediumRainSoundFx, mediumRainAudioSource);
				SoundEffectController(heavyRainSoundFx, heavyRainAudioSource);
				SoundEffectController(lightWindSoundFx, lightWindAudioSource);
				SoundEffectController(mediumWindSoundFx, mediumWindAudioSource);
				SoundEffectController(heavyWindSoundFx, heavyWindAudioSource);

				ParticleEffectController(lightRainIntensity * 4000.0f, lightRainParticle);
				ParticleEffectController(mediumRainIntensity * 4000.0f, mediumRainParticle);
				ParticleEffectController(heavyRainIntensity * 2000.0f, heavyRainParticle);
				ParticleEffectController(snowIntensity * 1000.0f, snowParticle);

				windZone.windMain = windSpeed;
				windZone.transform.rotation = Quaternion.Euler(new Vector3(0.0f, Mathf.Lerp(-180f, 180f, windDirection) + 180.0f, 0.0f));
			}
		}

		/// <summary>
		/// Start and stop the sounds effect.
		/// </summary>
		private void SoundEffectController(float volume, AudioSource sound)
		{
			sound.volume = volume;
			if (volume > 0)
			{
				if (!sound.isPlaying) sound.Play();
			}
			else if (sound.isPlaying) sound.Stop();
		}

		/// <summary>
		/// Start and stop the particle effect.
		/// </summary>
		private void ParticleEffectController(float intensity, ParticleSystem particle)
		{
			m_particleEmission = particle.emission;
			m_particleEmission.rateOverTimeMultiplier = intensity;
			if (intensity > 0)
			{
				if (!particle.isPlaying) particle.Play();
			}
			else if (particle.isPlaying) particle.Stop();
		}

		/// <summary>
		/// Updates the particles color.
		/// </summary>
		private void UpdateParticlesMaterials()
		{
			defaultRainMaterial.SetColor("_TintColor", rainColor);
			heavyRainMaterial.SetColor("_TintColor", rainColor);
			snowMaterial.SetColor("_TintColor", snowColor);
			rippleMaterial.SetColor("_TintColor", rainColor);
		}

		/// <summary>
		/// Create a thunder effect in the scene. When the thunder sound is over, the instance is automatically deleted.
		/// </summary>
		public void InstantiateThunderEffect(int index)
        {
            Transform thunder = Instantiate(thunderSettingsList[index].thunderPrefab, thunderSettingsList[index].position, thunderSettingsList[index].thunderPrefab.rotation);
            AzureThunderEffect thunderEffect = thunder.GetComponent<AzureThunderEffect>();
            thunderEffect.audioClip = thunderSettingsList[index].audioClip;
            thunderEffect.audioDelay = thunderSettingsList[index].audioDelay;
            thunderEffect.lightFrequency = thunderSettingsList[index].lightFrequency;
        }

		/// <summary>
		/// Enables or Disables the particle's Sub Emitters Module. This feature kill the particles inside the attached collider.
		/// </summary>
		public void SetParticlesSubEmittersModuleState(bool value)
		{
			m_particleSubEmitters = lightRainParticle.subEmitters;
			m_particleSubEmitters.enabled = value;
			m_particleSubEmitters = mediumRainParticle.subEmitters;
			m_particleSubEmitters.enabled = value;
			m_particleSubEmitters = heavyRainParticle.subEmitters;
			m_particleSubEmitters.enabled = value;
			m_particleSubEmitters = snowParticle.subEmitters;
			m_particleSubEmitters.enabled = value;
		}

		/// <summary>
		/// Enables or Disables the particle's Trigger Module. This feature kill the particles inside the attached collider.
		/// </summary>
		public void SetParticlesTriggerModuleState(bool value)
        {
			m_particleTrigger = lightRainParticle.trigger;
			m_particleTrigger.enabled = value;
			m_particleTrigger = mediumRainParticle.trigger;
			m_particleTrigger.enabled = value;
			m_particleTrigger = heavyRainParticle.trigger;
			m_particleTrigger.enabled = value;
			m_particleTrigger = snowParticle.trigger;
			m_particleTrigger.enabled = value;
		}

		/// <summary>
		/// Sets a collider trigger to the first element of the particle trigger collider list.
		/// Note1: You can still manually add more collider to the list by editing directly in each Particle Inspector.
		/// Note2: It will only make a diffence if the particle Trigger Module is enabled, you can use SetParticlesTriggerModuleState(bool value)
		/// to enable the particle's Trigger Module by code.
		/// </summary>
		public void SetParticlesTriggerCollider(Collider collider)
        {
			m_particlesTriggerCollider = collider;
			m_particleTrigger = lightRainParticle.trigger;
			m_particleTrigger.SetCollider(0, collider);
			m_particleTrigger = mediumRainParticle.trigger;
			m_particleTrigger.SetCollider(0, collider);
			m_particleTrigger = heavyRainParticle.trigger;
			m_particleTrigger.SetCollider(0, collider);
			m_particleTrigger = snowParticle.trigger;
			m_particleTrigger.SetCollider(0, collider);
		}

		/// <summary>
		/// Returns the current collider attached to the particle's Trigger Module.
		/// Note: Take care that it is null by default, you must reference a collider manually by the Inspector field or
		/// you can use SetParticlesTriggerCollider(Collider collider) to attach it by code.
		/// </summary>
		public Collider GetParticlesTriggerCollider()
        {
			return m_particlesTriggerCollider;
		}
    }
}