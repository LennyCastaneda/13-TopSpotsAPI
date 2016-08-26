using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TopSpotsAPI.Models;
using Newtonsoft.Json;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace TopSpotsAPI.Controllers
{
    public class TopSpotsController : ApiController
    {
        // GET /api/TopSpots
        // This Action should read and deserialize the JSON file of top spots into a C# object array, then return the array to end the method.
        public IEnumerable<TopSpot> Get()
        {
            // Took the topspots.json file from its filepath and deserialized the json content, 
            // Read the entire text with File.ReadAllText method and converted into an object then assigned into the new Maps[] named topspots.
            // TopSpot[] topspots = JsonConvert.DeserializeObject<TopSpot[]>(File.ReadAllText(@"Y:\Github\13-TopSpotsAPI\App_Data\topspots.json"));

            //Get the filename....Should put json file in AppData Folder
            string fileName = HttpContext.Current.Server.MapPath("../App_Data/topspots.json");

            //Read the contents of the file into a string
            string json = File.ReadAllText(fileName);

            // Convert the json string into an array of object of type TopSpot
            // In side <> brackets IEnumerable has a nest type -defines an array, a read only array.
            // ICollection - A collection that we can add to. ('I' stands for interface) a contract - a matching interface that matches what methods to attach to roles.
            // IEnumerable says any tpye of C# array you must be able to read only.
            // ICollection, you have read only but also add things to the array.
            // Tell JsonConvert class to convert Json file into an array.
            IEnumerable<TopSpot> topSpots = JsonConvert.DeserializeObject<IEnumerable<TopSpot>>(json);

            // Return new topspots array ending the method.
            return topSpots;

        }

        // POST: api/TopSpots
        // POST will involve reading the JSON from the file system, adding a TopSpot object to the end of the array and then saving the array back to the file system.
        public void Post([FromBody]TopSpot value)
        {

            // Take text object array and save it back to the file system.
            // File.WriteAllText(@"Y:\Github\13-TopSpotsAPI\App_Data\topspots.json", text);
            // Read the XML posted content from the file system and converting it into a JSON format and assign it to the string text variable object.

            // 1. Get file Name
            string fileName = HttpContext.Current.Server.MapPath("../App_Data/topspots.json");
            
            // 2. Read the JSON from the file System
            string json = File.ReadAllText(fileName);

            // 3. Deserialize - takes JSON and converts to C# objects.
            // Use List type Class that inherits from ICollections
            List<TopSpot> topSpots = JsonConvert.DeserializeObject<List<TopSpot>>(json);

            // 4. Add the top spot to the list
            // Value in this case is the topSpots being passed in.
            topSpots.Add(value);

            // 5. Serialize - Turn the list from C# objects back into JSON using SerializeObject
            json = JsonConvert.SerializeObject(topSpots);
            
            // 6. Write the JSON back to the file system, which replaces the file on the file system.
            // The second argument is the json file to write it to.
            File.WriteAllText(fileName, json);
        }

        // PUT: api/TopSpots
        // PUT (EDIT) will involve reading the JSON from the file system, finding the TopSpot to be updated, modifying those properties, then saving the array back to the file system.
        public void Put([FromBody]TopSpot updatedTopSpot)
        {
            // 1. Get file Name
            string fileName = HttpContext.Current.Server.MapPath("../App_Data/topspots.json");

            // 2. Read the JSON from the file System
            string json = File.ReadAllText(fileName);

            // 3. Deserialize - takes JSON and converts to C# objects.
            // Use List type Class that inherits from ICollections
            List<TopSpot> topSpots = JsonConvert.DeserializeObject<List<TopSpot>>(json);

            // 4. Find the top spot to be updated
            // foreach loop goes through every topspot location in the topSpots file and look for the top spot with matching laditude and longitude.
            foreach (var topSpot in topSpots)
            {
                // If there is a top spot location that matches the updated top spot location
                if (topSpot.Location[0] == updatedTopSpot.Location[0] &&
                    topSpot.Location[1] == updatedTopSpot.Location[1])
                {
                    // 5. Modify the properties - Take the topSpot value and overriding the values of the object.
                    // Update the name of the top spot
                    topSpot.Name = updatedTopSpot.Name;
                    // And update the top spot description
                    topSpot.Description = updatedTopSpot.Description;
                    // And update the top spot location because it was edited by user.
                    topSpot.Location = updatedTopSpot.Location;
                }
            }

            // 6. Turn the list back into JSON
            json = JsonConvert.SerializeObject(topSpots);

            // 7. Write the JSON back to the file system
            File.WriteAllText(fileName, json);

            //*************ONLY WAY TO TEST THIS IS TO UPDATE JSON FILE WITH ID'S ********************//
        }

        // DELETE: api/Topspots
        // DELETE will involve reading the JSON from the file system, finding the TopSpot to be deleted, removing it from the array, then saving the array back to the file system.
        public void Delete(int id)
        {
            // 1. Get file Name
            string fileName = HttpContext.Current.Server.MapPath("../App_Data/topspots.json");

            // 2. Read the JSON from the file System
            string json = File.ReadAllText(fileName);

            // 3. Deserialize - takes JSON and converts to C# objects.
            // Use List type Class that inherits from ICollections
            List<TopSpot> topSpots = JsonConvert.DeserializeObject<List<TopSpot>>(json);

            // 4. Find the top spot to remove
            foreach (var topSpot in topSpots)
            {
                if (topSpot.Id == id)
                {
                    // 5. Remove it from the list
                    // Remove is a method of the List object
                    // List is an objects instaniated from a class, and a Class has properties and methods.
                    // This List Class has a method called remove.
                    // Passing in the object to be removed, which is 'topSpot'
                    topSpots.Remove(topSpot);
                    break;
                }
            }

            // 6. Convert the list back to JSON
            json = JsonConvert.SerializeObject(topSpots);

            // 7. Save to the file system.
            File.WriteAllText(fileName, json);
        }
    }
}
