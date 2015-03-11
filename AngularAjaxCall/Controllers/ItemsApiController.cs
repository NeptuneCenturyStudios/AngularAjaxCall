
using AngularAjaxCall.Models.Examples.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Caching;
using System.Web.Http;

namespace AngularAjaxCall.Controllers
{

    [RoutePrefix("Examples")]
    public class ItemsApiController : ApiController
    {
        // GET api/items
        [Route("Items")]
        public IEnumerable<Item> Get()
        {
            //set our cache key
            var key = "examples.items";

            //create a list of items
            List<Item> items;

            //attempt to get items from cache
            items = (List<Item>)HttpRuntime.Cache.Get("examples.items");

            //if we don't have any items, create a new list and store it in cache for next time
            if (items == null)
            {
                //create randomizer
                var rnd = new Random();

                //create new list
                items = new List<Item>();
                //add some new items
                for (var x = 0; x < 20; x++) { 
                
                    //create new item
                    var item = new Item()
                    {
                        Name = string.Format("Item {0}", x + 1),
                        Description = string.Format("This is a description for Item {0}.", x + 1),
                        Quantity = rnd.Next(0, 100),
                        Price = (decimal)rnd.NextDouble() * 100
                    };

                    //add item to list
                    items.Add(item);
                }

                //add items to cache
                HttpRuntime.Cache.Add(key, items, null, Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0, 0, 0), CacheItemPriority.Default, null);
            }

            return items;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
