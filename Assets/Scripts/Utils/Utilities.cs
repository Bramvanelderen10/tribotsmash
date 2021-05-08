using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Tribot
{
    /// <summary>
    /// An audio wrapper which makes it easy to play clips with a variety of custom settings
    /// </summary>
    [System.Serializable]
    public class CustomClip
    {
        [Header("Clip Settings")]
        public List<AudioClip> Clips = new List<AudioClip> { null };
        public float Volume = 1f;
        [MinMaxRange(0f, 2f)]
        public Range Pitch = new Range() { Min = 1f, Max = 1f };
        [MinMaxRange(0f, 1f)]
        public Range LowPass = new Range() { Min = 1f, Max = 1f };
        public float Reverb = 1f;
        public bool Loop = false;
        public AudioRolloffMode Rolloff = AudioRolloffMode.Logarithmic;
        public AudioMixerGroup Group;

        AudioClip Clip
        {
            get
            {
                if (Clips == null || Clips.Count == 0)
                    return null;

                return Clips[UnityEngine.Random.Range(0, Clips.Count)];
            }
        }

        /// <summary>
        /// Play an audio clip with all the custom settings
        /// </summary>
        /// <param name="audio"></param>
        public void Play(AudioSource audio)
        {
            if (Clip == null)
                return;

            //Apply filters
            var lowPass = audio.gameObject.GetComponent<AudioLowPassFilter>();
            if (!lowPass)
                lowPass = audio.gameObject.AddComponent<AudioLowPassFilter>();
            lowPass.cutoffFrequency = 22000f * LowPass.Random;

            //Apply Audio source settings and play
            audio.Stop();
            audio.clip = Clip;
            audio.volume = Volume;
            audio.pitch = Pitch.Random;
            audio.loop = Loop;
            audio.reverbZoneMix = Reverb;
            audio.rolloffMode = Rolloff;
            audio.outputAudioMixerGroup = Group;
            audio.Play();
        }

        public void Play(MonoBehaviour component)
        {
            var audio = component.GetComponent<AudioSource>();
            if (!audio)
                audio = component.gameObject.AddComponent<AudioSource>();

            Play(audio);
        }
    }

    /// <summary>
    /// Generic singleton class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        /// <summary>
        /// Returns an instance of the singleton
        /// </summary>
        public static T Instance { get { return Nested._instance; } }

        class Nested
        {
            static Nested() {}
            internal static readonly T _instance = new T();
        }
    }

    /// <summary>
    /// Range class
    /// </summary>
    [System.Serializable]
    public class Range
    {
        public float Min;
        public float Max;

        /// <summary>
        /// Returns a random value within the min max range
        /// </summary>
        public float Random
        {
            get
            {
                return UnityEngine.Random.Range(Min, Max);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MinMaxRangeAttribute : Attribute
    {
        public MinMaxRangeAttribute(float min, float max)
        {
            Min = min;
            Max = max;
        }
        public float Min { get; private set; }
        public float Max { get; private set; }
    }

    public static class Extensions
    {
        /// <summary>
        /// Copies a given component
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comp"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static T GetCopyOf<T>(this Component comp, T other) where T : Component
        {
            Type type = comp.GetType();
            if (type != other.GetType()) return null; // type mis-match
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
            PropertyInfo[] pinfos = type.GetProperties(flags);
            foreach (var pinfo in pinfos)
            {
                if (pinfo.CanWrite)
                {
                    try
                    {
                        pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                    }
                    catch { } // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
                }
            }
            FieldInfo[] finfos = type.GetFields(flags);
            foreach (var finfo in finfos)
            {
                finfo.SetValue(comp, finfo.GetValue(other));
            }
            return comp as T;
        }

        /// <summary>
        /// Adds a copy of a given component to the object
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        /// <param name="go"></param>
        /// <param name="toAdd">The component to copy</param>
        /// <returns></returns>
        public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
        {
            return go.AddComponent<T>().GetCopyOf(toAdd) as T;
        }

        /// <summary>
        /// Retrieves a quaternion rotation in the direction of a given point
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="lookAt"></param>
        /// <returns></returns>
        public static Quaternion LookRotation(this Transform transform, Vector3 lookAt)
        {
            var dir = (lookAt - transform.position).normalized;

            return Quaternion.LookRotation(dir);
        }

        /// <summary>
        /// Retrieves a quaternion rotation in the direction of a given point
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Quaternion LookRotation(this Transform transform, float x, float y, float z)
        {
            var lookAt = new Vector3(x, y, z);
            var dir = (lookAt - transform.position).normalized;

            return Quaternion.LookRotation(dir);
        }

        public static Quaternion Multiply(this Quaternion quat0, Quaternion quat1)
        {
            long w0 = (long)quat0.w;
            var v0 = new Vector3(quat0.x, quat0.y, quat0.z);

            long w1 = (long)quat1.w;
            var v1 = new Vector3(quat1.x, quat1.y, quat1.z);


            long w2 = (long)(w1 *w0 - Vector3.Dot(v1, v0));
            var v2 = w0*v1 + Vector3.Cross(v1, v0);
            var result = new Quaternion(v2.x, v2.y, v2.z, w2);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Vector3 PositionXZ(this Transform transform, Vector3 target)
        {
            var pos = new Vector3(transform.position.x, target.y, transform.position.z);

            return pos;
        }

        /// <summary>
        /// Orbits a certain position around a given center position and axis by a given angle
        /// </summary>
        /// <param name="position"></param>
        /// <param name="center"></param>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector3 RotateAround(this Vector3 position, Vector3 center, Vector3 axis, float angle)
        {
            var result = Vector3.zero;
            var rot = Quaternion.AngleAxis(angle, axis); // get the desired rotation
            var dir = position - center; // find current direction relative to center
            dir = rot * dir; // rotate the direction
            result = center + dir; // define new position

            return result;
        }

        /// <summary>
        /// Returns the direction towards the target
        /// </summary>
        /// <param name="current"></param>
        /// <param name="lookAt"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static Vector3 Direction(this Transform current, Transform lookAt, bool ignoreVerticalDifference = false)
        {
            var result = current.forward;

            if (ignoreVerticalDifference)
            {
                var currentPos = current.position;
                var targetPos = new Vector3(lookAt.position.x, current.position.y, lookAt.position.z);
                result = (targetPos - currentPos).normalized;
            }
            else
            {
                result = (lookAt.position - current.position).normalized;
            }

            return result;
        }

        /// <summary>
        /// Adds an audiosource to a new gameobject that is a child to the given gameobject
        /// </summary>
        /// <param name="component"></param>
        /// <param name="playOnWake"></param>
        /// <returns></returns>
        public static AudioSource AddAudioSource(this GameObject component, bool playOnWake = false)
        {
            var obj = new GameObject();
            obj.transform.parent = component.transform;
            obj.transform.localPosition = Vector3.zero;
            obj.name = "AudioObject";
            var audio = obj.AddComponent<AudioSource>();
            audio.playOnAwake = playOnWake;

            return audio;
        }

        /// <summary>
        /// Checks if an object of type already exists, 
        /// if so that depending on destroy value it will destroy the existing object
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="destroy"></param>
        /// <returns></returns>
        public static bool CheckIfAlreadyExists(this Component comp, bool destroy = false)
        {
            var result = false;
            foreach (var item in GameObject.FindObjectsOfType(comp.GetType()))
            {
                if (item != comp)
                    result = true;
            }

            if (destroy && result)
                Component.Destroy(comp.gameObject);

            return result;
        }

        public static Color SetColor(this Color color, float r, float g, float b, float a)
        {
            color.r = r;
            color.g = g;
            color.b = b;
            color.a = a;

            return color;
        }

        public static Color SetColor(this Color color, Color newColor, float a)
        {
            color = newColor;
            color.a = a;
            return color;
        }
        public static Color SetColor(this Color color, float a)
        {
            color.a = a;
            return color;
        }
    }

    public static class Utilities
    {
        /// <summary>
        /// Sets the photon player properties
        /// </summary>
        /// <param name="player"></param>
        /// <param name="position"></param>
        public static void SetCustomProperties(PhotonPlayer player, int position)
        {
            ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable()
                {
                    {
                        "Index", position
                    },
                    {
                        "Name", player.NickName
                    }
                };

            player.SetCustomProperties(customProperties);
        }
    }
}
