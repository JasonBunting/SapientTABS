﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace SapientTABS
{
    public class PinaCollada : MonoBehaviour
    {
        public static CultureInfo invariant_culture;

        public static Dictionary<string, Mesh> imported_meshes;

        public static Dictionary<string, Material[]> imported_materials;

        public static Texture2D[] imported_textures;

        public static Dictionary<string, SoundEffectInstance> imported_sounds;

        public static string directory;

        public static bool Contains(int[][] array, int[] value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == value)
                {
                    return true;
                }
            }
            return false;
        }

        public static int[] GenerateRange(int floor, int ceiling)
        {
            List<int> list = new List<int>();
            for (int i = floor; i < ceiling; i++)
            {
                list.Add(i);
            }
            return list.ToArray();
        }

        public static string[] CutUp(string data, string separator)
        {
            return data.Split(new string[1]
            {
            separator
            }, StringSplitOptions.None);
        }

        public static string GetPerfectFileName(string raw_input)
        {
            string text = raw_input;
            if (text.Contains("."))
            {
                text = CutUp(text, ".")[0];
            }
            if (text.Contains("/"))
            {
                text = CutUp(text, "/")[1];
            }
            if (text.Contains("\\"))
            {
                text = CutUp(text, "\\")[CutUp(text, "\\").Length - 1];
            }
            return text;
        }

        public static string BasicCut(string data, string tag, string id)
        {
            int num = data.IndexOf("<" + tag + " id=\"" + id + "\">") + ("<" + tag + " id=\"" + id + "\">").Length;
            return data.Substring(num, data.Length - num);
        }

        public static string GetCut(string data, string start, string end)
        {
            int num = data.IndexOf(start) + start.Length;
            return data.Substring(num, data.IndexOf(end) - num);
        }

        public static string GetModelData(string filename)
        {
            return File.ReadAllText(directory + "\\Models\\" + filename);
        }

        public static string GetSection(string data, string tag, string id, string param = " id=\"", string close = "\"")
        {
            int num = data.IndexOf("<" + tag + param + id + close) + ("<" + tag).Length;
            data = data.Substring(num, data.IndexOf("</" + tag + ">") - num + 1);
            int num2 = data.IndexOf(">") + 1;
            return data.Substring(num2, data.Length - 1 - num2);
        }

        public static Vector3[] GetVector3(string data)
        {
            string[] array = data.Split(" "[0]);
            Vector3[] array2 = new Vector3[array.Length / 3];
            for (int i = 0; i < array.Length / 3; i++)
            {
                array2[i] = new Vector3(float.Parse(array[i * 3], invariant_culture), float.Parse(array[i * 3 + 1], invariant_culture), float.Parse(array[i * 3 + 2], invariant_culture));
            }
            return array2;
        }

        public static Vector4[] GetVector4(string data)
        {
            string[] array = data.Split(" "[0]);
            Vector4[] array2 = new Vector4[array.Length / 4];
            for (int i = 0; i < array.Length / 4; i++)
            {
                array2[i] = new Vector4(float.Parse(array[i * 4], invariant_culture), float.Parse(array[i * 4 + 1], invariant_culture), float.Parse(array[i * 4 + 2], invariant_culture), float.Parse(array[i * 4 + 3], invariant_culture));
            }
            return array2;
        }

        public static Vector2[] GetVector2(string data)
        {
            string[] array = data.Split(" "[0]);
            Vector2[] array2 = new Vector2[array.Length / 2];
            for (int i = 0; i < array.Length / 2; i++)
            {
                array2[i] = new Vector2(float.Parse(array[i * 2], invariant_culture), float.Parse(array[i * 2 + 1], invariant_culture));
            }
            return array2;
        }

        public static int[] GetInt(string data)
        {
            string[] array = data.Split(" "[0]);
            int[] array2 = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array2[i] = int.Parse(array[i], invariant_culture);
            }
            return array2;
        }

        public static float[] GetFloat(string data)
        {
            string[] array = data.Split(" "[0]);
            float[] array2 = new float[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array2[i] = float.Parse(array[i], invariant_culture);
            }
            return array2;
        }

        public static int GetIntfromBytes(byte[] array, int offset = 0)
        {
            int num = 0;
            for (int i = 0; i < 4; i++)
            {
                num |= array[offset + i] << i * 8;
            }
            return num;
        }

        public static float GetFloatFromBytes(byte start, byte finish)
        {
            return (float)(short)((finish << 8) | start) / 32768f;
        }

        public static SoundEffectInstance GetSoundEffect(string sound_reference, SoundEffectInstance effect_base)
        {
            string perfectFileName = GetPerfectFileName(sound_reference);
            SoundEffectInstance soundEffectInstance = imported_sounds[perfectFileName];
            soundEffectInstance.category = effect_base.category;
            soundEffectInstance.overrideMixerGroup = effect_base.overrideMixerGroup;
            return soundEffectInstance;
        }

        public static AudioClip GetAudioFromFile(string filename)
        {
            byte[] array = File.ReadAllBytes(filename);
            bool flag = array[22] == 2;
            int intfromBytes = GetIntfromBytes(array, 24);
            int num = 12;
            while (array[num] != 100 || array[num + 1] != 97 || array[num + 2] != 116 || array[num + 3] != 97)
            {
                num += 4;
                int num2 = array[num] + array[num + 1] * 256 + (array[num + 2] * 65536 + array[num + 3] * 16777216);
                num += 4 + num2;
            }
            num += 8;
            int num3 = (array.Length - num) / 2;
            if (flag)
            {
                num3 /= 2;
            }
            float[] array2 = new float[num3];
            float[] array3 = new float[num3];
            int i = 0;
            int num4 = array.Length - ((!flag) ? 1 : 3);
            for (; i < num3; i++)
            {
                if (num >= num4)
                {
                    break;
                }
                array2[i] = GetFloatFromBytes(array[num], array[num + 1]);
                num += 2;
                if (flag)
                {
                    array3[i] = GetFloatFromBytes(array[num], array[num + 1]);
                    num += 2;
                }
            }
            AudioClip audioClip = AudioClip.Create(GetPerfectFileName(filename), num3, 1, intfromBytes, false);
            audioClip.SetData(array2, 0);
            if (flag)
            {
                audioClip.SetData(array3, 1);
            }
            return audioClip;
        }

        public static Material[] ImportMaterials(string file)
        {
            Material[] array = null;
            if (GetModelData(file).Contains("library_effects"))
            {
                string[] array2 = CutUp(GetSection(GetModelData(file), "library_effects", "", "", ""), "effect");
                new List<string[]>();
                int num = 0;
                string[] array3 = array2;
                for (int i = 0; i < array3.Length; i++)
                {
                    if (array3[i].Contains("color"))
                    {
                        num++;
                    }
                }
                array = new Material[num];
                int num2 = 0;
                string[] array4 = array2;
                foreach (string text in array4)
                {
                    if (text.Contains("color"))
                    {
                        array[num2] = new Material(Shader.Find("Standard"));
                        if (text.Contains("diffuse"))
                        {
                            array[num2].color = GetVector4(GetSection(GetSection(text, "diffuse", "", "", ""), "color", "diffuse", "sid=\""))[0];
                        }
                        num2++;
                    }
                }
            }
            if (array == null)
            {
                array = new Material[1]
                {
                new Material(Shader.Find("Standard"))
                };
            }
            return array;
        }

        public static Mesh ImportMesh(string file)
        {
            bool flag = GetModelData(file).Contains("map-0");
            string cut = GetCut(GetModelData(file), "<geometry id=\"", "-mesh");
            int num = 0;
            num = ((!flag) ? 2 : 3);
            Vector3[] vector = GetVector3(GetSection(BasicCut(GetModelData(file), "source", cut + "-mesh-positions"), "float_array", cut + "-mesh-positions-array"));
            Vector3[] vector2 = GetVector3(GetSection(BasicCut(GetModelData(file), "source", cut + "-mesh-normals"), "float_array", cut + "-mesh-normals-array"));
            Vector2[] array = new Vector2[0];
            if (flag)
            {
                array = GetVector2(GetSection(BasicCut(GetModelData(file), "source", cut + "-mesh-map-0"), "float_array", cut + "-mesh-map-0-array"));
            }
            string[] array2 = CutUp(GetModelData(file), "triangles");
            List<int[]> list = new List<int[]>();
            for (int i = 0; i < array2.Length; i++)
            {
                if (i > 0 && i < array2.Length - 1 && array2[i].Contains("semantic"))
                {
                    list.Add(GetInt(GetSection(array2[i], "p", "", "", "")));
                }
            }
            int[] array3 = new int[list.Count];
            for (int j = 0; j < list.Count; j++)
            {
                array3[j] = list[j].Length / num;
            }
            List<int> list2 = new List<int>();
            foreach (int[] item2 in list)
            {
                foreach (int item in item2)
                {
                    list2.Add(item);
                }
            }
            int[] array4 = list2.ToArray();
            int[] array5 = new int[array4.Length / num];
            int[] array6 = new int[array4.Length / num];
            int[] array7 = new int[array4.Length / num];
            for (int l = 0; l < array4.Length / num; l++)
            {
                array5[l] = array4[l * num];
                array6[l] = array4[l * num + 1];
                if (flag)
                {
                    array7[l] = array4[l * num + 2];
                }
            }
            int[][] array8 = new int[array5.Length][];
            int[][] array9 = new int[array5.Length][];
            for (int m = 0; m < array5.Length; m++)
            {
                if (flag)
                {
                    array8[m] = new int[3]
                    {
                    array5[m],
                    array6[m],
                    array7[m]
                    };
                }
                else
                {
                    array8[m] = new int[2]
                    {
                    array5[m],
                    array6[m]
                    };
                }
            }
            int num2 = 0;
            for (int n = 0; n < array8.Length; n++)
            {
                if (!Contains(array9, array8[n]))
                {
                    array9[num2] = array8[n];
                    num2++;
                }
            }
            Vector3[] array10 = new Vector3[array9.Length];
            Vector3[] array11 = new Vector3[array9.Length];
            Vector2[] array12 = new Vector2[array9.Length];
            for (int num3 = 0; num3 < array9.Length; num3++)
            {
                array10[num3] = vector[array9[num3][0]];
                array11[num3] = vector2[array9[num3][1]];
                if (flag)
                {
                    array12[num3] = array[array9[num3][2]];
                }
            }
            Mesh mesh = new Mesh();
            mesh.Clear();
            mesh.vertices = array10;
            mesh.normals = array11;
            if (flag)
            {
                mesh.uv = array12;
            }
            mesh.subMeshCount = array3.Length;
            int num4 = 0;
            for (int num5 = 0; num5 < array3.Length; num5++)
            {
                mesh.SetTriangles(GenerateRange(num4, num4 + array3[num5]), num5);
                num4 += array3[num5];
            }
            return mesh;
        }

        public static Sprite GetSprite(string sprite_name, int? pixels_per_unit = null, FilterMode filter_mode = FilterMode.Point)
        {
            Texture2D texture2D = null;
            for (int i = 0; i < imported_textures.Length; i++)
            {
                if (imported_textures[i].name == sprite_name)
                {
                    texture2D = imported_textures[i];
                }
            }
            texture2D.filterMode = filter_mode;
            if (pixels_per_unit.HasValue)
            {
                return Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), pixels_per_unit.Value);
            }
            return Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
        }

        public static void SetStaticMesh(GameObject game_object, string mesh_key, float simple_scale = 1f, Vector3? rotation = null, Vector3? translation = null, string object_name = "PinaCollada Mesh Object")
        {
            MeshRenderer[] componentsInChildren = game_object.GetComponentsInChildren<MeshRenderer>();
            SkinnedMeshRenderer[] componentsInChildren2 = game_object.GetComponentsInChildren<SkinnedMeshRenderer>();
            Mesh mesh = imported_meshes[mesh_key];
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                componentsInChildren[i].enabled = false;
            }
            for (int j = 0; j < componentsInChildren2.Length; j++)
            {
                componentsInChildren2[j].enabled = false;
            }
            GameObject gameObject = new GameObject();
            gameObject.name = object_name;
            MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshFilter.mesh = mesh;
            meshRenderer.materials = imported_materials[mesh_key];
            gameObject.transform.position = game_object.transform.position;
            gameObject.transform.rotation = game_object.transform.rotation;
            gameObject.transform.localScale *= simple_scale;
            gameObject.transform.parent = game_object.transform;
            if (translation.HasValue)
            {
                gameObject.transform.position += translation.Value.x * gameObject.transform.parent.right + translation.Value.y * gameObject.transform.parent.up + translation.Value.z * gameObject.transform.parent.forward;
            }
            if (rotation.HasValue)
            {
                gameObject.transform.Rotate(rotation.Value);
            }
        }

        static PinaCollada()
        {
            directory = "ImportedAssets";
            invariant_culture = CultureInfo.InvariantCulture;
            string[] files = Directory.GetFiles(directory + "\\Models");
            int num = 0;
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].EndsWith(".dae"))
                {
                    num++;
                }
            }
            imported_meshes = new Dictionary<string, Mesh>();
            int num2 = 0;
            for (int j = 0; j < files.Length; j++)
            {
                if (Path.GetFileName(files[j]).EndsWith(".dae"))
                {
                    imported_meshes.Add(GetPerfectFileName(files[j]), ImportMesh(Path.GetFileName(files[j])));
                    num2++;
                }
            }
            num2 = 0;
            imported_materials = new Dictionary<string, Material[]>();
            for (int k = 0; k < files.Length; k++)
            {
                if (Path.GetFileName(files[k]).EndsWith(".dae"))
                {
                    imported_materials.Add(GetPerfectFileName(files[k]), ImportMaterials(Path.GetFileName(files[k])));
                    num2++;
                }
            }
            List<Texture2D> list = new List<Texture2D>();
            string[] files2 = Directory.GetFiles(directory + "\\Textures");
            foreach (string text in files2)
            {
                if (text.EndsWith(".png"))
                {
                    byte[] data = File.ReadAllBytes(text);
                    Texture2D texture2D = new Texture2D(64, 64, TextureFormat.ARGB32, false);
                    texture2D.LoadImage(data);
                    texture2D.name = GetPerfectFileName(text);
                    list.Add(texture2D);
                }
            }
            imported_textures = list.ToArray();
            imported_sounds = new Dictionary<string, SoundEffectInstance>();
            int num3 = 0;
            SoundEffectVariations.MaterialType[] array = new SoundEffectVariations.MaterialType[6]
            {
            SoundEffectVariations.MaterialType.Default,
            SoundEffectVariations.MaterialType.Ground,
            SoundEffectVariations.MaterialType.Meat,
            SoundEffectVariations.MaterialType.Metal,
            SoundEffectVariations.MaterialType.WarMachine,
            SoundEffectVariations.MaterialType.Wood
            };
            files2 = Directory.GetDirectories(directory + "\\Sounds");
            foreach (string text2 in files2)
            {
                if (text2.Contains("."))
                {
                    continue;
                }
                List<SoundEffectVariations> list2 = new List<SoundEffectVariations>();
                int num4 = 0;
                string[] directories = Directory.GetDirectories(text2);
                foreach (string path in directories)
                {
                    List<AudioClip> list3 = new List<AudioClip>();
                    string[] files3 = Directory.GetFiles(path);
                    foreach (string text3 in files3)
                    {
                        if (text3.EndsWith(".wav"))
                        {
                            list3.Add(GetAudioFromFile(text3));
                        }
                    }
                    list2.Add(new SoundEffectVariations());
                    list2[num4].clips = list3.ToArray();
                    list2[num4].materialType = array[num4];
                    num4++;
                }
                string perfectFileName = GetPerfectFileName(text2);
                if (File.Exists(directory + "\\Sounds\\" + perfectFileName + "\\" + perfectFileName + ".sfxml"))
                {
                    string text4 = File.ReadAllText(directory + "\\Sounds\\" + perfectFileName + "\\" + perfectFileName + ".sfxml");
                    imported_sounds.Add(perfectFileName, new SoundEffectInstance());
                    imported_sounds[perfectFileName].soundRef = perfectFileName;
                    imported_sounds[perfectFileName].clipTypes = list2.ToArray();
                    if (text4.Contains("volume"))
                    {
                        imported_sounds[perfectFileName].volume = GetVector2(GetSection(text4, "volume", "", "", ""))[0];
                    }
                    if (text4.Contains("pitch"))
                    {
                        imported_sounds[perfectFileName].pitch = GetVector2(GetSection(text4, "pitch", "", "", ""))[0];
                    }
                    if (text4.Contains("max_instances"))
                    {
                        imported_sounds[perfectFileName].maxInstances = GetInt(GetSection(text4, "max_instances", "", "", ""))[0];
                    }
                    if (text4.Contains("cooldown"))
                    {
                        imported_sounds[perfectFileName].cooldown = GetFloat(GetSection(text4, "cooldown", "", "", ""))[0];
                    }
                    if (text4.Contains("space_blend"))
                    {
                        imported_sounds[perfectFileName].spatialBlend = GetFloat(GetSection(text4, "space_blend", "", "", ""))[0];
                    }
                    if (text4.Contains("reverb_mix"))
                    {
                        imported_sounds[perfectFileName].reverbZoneMix = GetFloat(GetSection(text4, "reverb_mix", "", "", ""))[0];
                    }
                    if (text4.Contains("distance_min_max"))
                    {
                        imported_sounds[perfectFileName].minDistance = GetFloat(GetSection(text4, "distance_min_max", "", "", ""))[0];
                    }
                    if (text4.Contains("distance_min_max"))
                    {
                        imported_sounds[perfectFileName].maxDistance = GetFloat(GetSection(text4, "distance_min_max", "", "", ""))[1];
                    }
                    if (text4.Contains("song_length"))
                    {
                        imported_sounds[perfectFileName].lengthInMeasures = GetInt(GetSection(text4, "song_length", "", "", ""))[0];
                    }
                    if (text4.Contains("transitions"))
                    {
                        imported_sounds[perfectFileName].transitionMeasures = GetInt(GetSection(text4, "transitions", "", "", ""));
                    }
                    num3++;
                }
            }
        }
    }
}
