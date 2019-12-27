using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            string path = getPath(saveFile);
            print("Save to " + path);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                Transform playerTransform = getTransform();
                byte[] buffer = serializeVector(playerTransform.position);

                BinaryFormatter formatter = new BinaryFormatter();
                SerializableVector position = new SerializableVector(playerTransform.position);
                formatter.Serialize(stream, position);

                stream.Write(buffer, 0, buffer.Length);
            };
        }

        public void Load(string saveFile)
        {
            string path = getPath(saveFile);
            print("Loading from " + path);
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                Transform playerTransform = getTransform();

                BinaryFormatter formatter = new BinaryFormatter();
                SerializableVector position = (SerializableVector)formatter.Deserialize(stream);
                playerTransform.position = position.toVector();
            }
        }

        private Transform getTransform()
        {
            return GameObject.FindWithTag("Player").transform;
        }


        private byte[] serializeVector(Vector3 vector)
        {
            byte[] vectorBuffer = new byte[12]; //Store 3 floats
            BitConverter.GetBytes(vector.x).CopyTo(vectorBuffer, 0);
            BitConverter.GetBytes(vector.y).CopyTo(vectorBuffer, 4);
            BitConverter.GetBytes(vector.z).CopyTo(vectorBuffer, 8);
            return vectorBuffer;
        }

        private Vector3 deserializeVector(byte[] byteArray)
        {
            Vector3 result = new Vector3();
            result.x = BitConverter.ToSingle(byteArray, 0);
            result.y = BitConverter.ToSingle(byteArray, 4);
            result.z = BitConverter.ToSingle(byteArray, 8);
            return result;
        }

        private string getPath(string saveFile)
        {
            return Path.Combine(Application.dataPath, saveFile + ".sav");
        }
    }
}