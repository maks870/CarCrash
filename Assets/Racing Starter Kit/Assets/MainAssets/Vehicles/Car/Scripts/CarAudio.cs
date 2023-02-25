using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarAudio : MonoBehaviour
    {
        public AudioClip lowAccelClip;                                              // Audio clip for low acceleration
        public AudioClip lowDecelClip;                                              // Audio clip for low deceleration
        public AudioClip highAccelClip;                                             // Audio clip for high acceleration
        public AudioClip highDecelClip;                                             // Audio clip for high deceleration
        public float pitchMultiplier = 1f;                                          // Used for altering the pitch of audio clips
        public float lowPitchMin = 1f;                                              // The lowest possible pitch for the low sounds
        public float lowPitchMax = 6f;                                              // The highest possible pitch for the low sounds
        public float highPitchMultiplier = 0.25f;                                   // Used for altering the pitch of high sounds
        public float maxRolloffDistance = 500;                                      // The maximum distance where rollof starts to take place
        public float dopplerLevel = 1;                                              // The mount of doppler effect used in the audio
        public bool useDoppler = true;                                              // Toggle for using doppler
        public AudioSource source;
        private AudioSource m_HighAccel; // Source for the high acceleration sounds
        private bool m_StartedSound; // flag for knowing if we have started sounds
        private CarController m_CarController; // Reference to car we are controlling
        [SerializeField] private Camera cam;

        private void Start()
        {

            if (cam == null)
                cam = Camera.main;
            // get the carcontroller ( this will not be null as we have require component)
            m_CarController = GetComponent<CarController>();

            // setup the simple audio source
            m_HighAccel = SetUpEngineAudioSource(highAccelClip);
        }

        private void StartSound()
        {
            m_HighAccel.mute = false;
            m_StartedSound = true;
        }

        private void StopSound()
        {
            m_HighAccel.mute = true;
            m_StartedSound = false;
        }

        // Update is called once per frame
        private void Update()
        {
            if (!m_StartedSound)
                StartSound();

            // get the distance to main camera
            float camDist = (cam.transform.position - transform.position).sqrMagnitude;

            // stop sound if the object is beyond the maximum roll off distance
            if (m_StartedSound && camDist > maxRolloffDistance * maxRolloffDistance || cam == null)
            {
                StopSound();
            }

            // start the sound if not playing and it is nearer than the maximum distance
            if (!m_StartedSound && camDist < maxRolloffDistance * maxRolloffDistance)
            {
                StartSound();
            }

            if (m_StartedSound)
            {
                // The pitch is interpolated between the min and max values, according to the car's revs.
                float pitch = ULerp(lowPitchMin, lowPitchMax, m_CarController.Revs);

                // clamp to minimum pitch (note, not clamped to max for high revs while burning out)
                pitch = Mathf.Min(lowPitchMax, pitch);

                m_HighAccel.pitch = pitch * pitchMultiplier * highPitchMultiplier;
                m_HighAccel.dopplerLevel = useDoppler ? dopplerLevel : 0;
                m_HighAccel.volume = 0.3f;

            }
        }


        // sets up and adds new audio source to the gane object
        private AudioSource SetUpEngineAudioSource(AudioClip clip)
        {
            if (source == null)
                Debug.LogError("Отсутствует ссылка на вывод звука");
            source.enabled = true;
            source.clip = clip;
            source.volume = 0;
            source.loop = true;

            // start the clip from a random point
            source.time = Random.Range(0f, clip.length);
            source.Play();
            source.minDistance = 5;
            source.maxDistance = maxRolloffDistance;
            source.dopplerLevel = 0;
            return source;
        }


        // unclamped versions of Lerp and Inverse Lerp, to allow value to exceed the from-to range
        private static float ULerp(float from, float to, float value)
        {
            return (1.0f - value) * from + value * to;
        }
    }
}
