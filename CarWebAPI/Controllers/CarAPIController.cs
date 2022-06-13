using CarWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarWebAPI.Controllers
{
    public class CarAPIController : ApiController
    {

        public IEnumerable<Car> Get()
        {
            using(carEntities entities = new carEntities())
            {
                return entities.Cars.ToList();
            }
        }

        public HttpResponseMessage Get(int id)
        {
            using(carEntities entities = new carEntities())
            {
                var entity = entities.Cars.FirstOrDefault(e => e.Id == id);

                if( entity != null )
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound,
                        "Car with Id " + id.ToString() + " not found");

                } 

            }
        }

        public HttpResponseMessage Post([FromBody] Car car)
        {
            try
            {
                using (carEntities entities = new carEntities())
                {
                    entities.Cars.Add(car);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, car);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        car.Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }


        public HttpResponseMessage Put(int id, [FromBody] Car car)
        {
            try
            {
                using(carEntities entities = new carEntities())
                {
                    var entity = entities.Cars.FirstOrDefault(e => e.Id == id);
                    if(entity == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound,
                            "Car with Id " + id.ToString() + "not found to update");
                    }
                    else
                    {
                        entity.Name = car.Name;
                        entity.Color = car.Color;
                        entity.YearMade = car.YearMade;

                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);

                    }
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using(carEntities entities = new carEntities())
                {
                    var entity = entities.Cars.FirstOrDefault(e => e.Id == id);
                    if(entity == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound,
                            "Car with id " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.Cars.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        //////////////////////////////
    }
}
