using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Repository
{
    public static class SerializerHelper
    {
        /// <summary>
        /// Write the object in a Json file
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="sourceObject">Source Object</param>
        /// <param name="fileName">File Name</param>
        public static bool WriteIntoFile(object sourceObject, string fileName)
        {

            try
            {                
                using (StreamWriter file = File.CreateText(fileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, sourceObject);
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Get the Object form Json file
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="fileName">File Name</param>
        /// <returns></returns>
        public static T GetObjectFromFile<T>(string fileName)
        {
            try
            {
                // deserialize JSON from a file
                using (StreamReader file = File.OpenText(fileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    return (T)serializer.Deserialize(file, typeof(T));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the object form Json text
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="JsonText">Json Text</param>
        /// <returns></returns>
        public static T GetObjectFromJsonText<T>(string JsonText)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(JsonText);

            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}