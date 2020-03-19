﻿using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        IEnumerable<Restaurant> GetRestaurantsByName(string SearchTerm);
        Restaurant GetById(int id);
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;

        public InMemoryRestaurantData() 
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant {Id = 1, Name = "Scott's Pizza", Location="Maryland", Cuisine=CuisineType.Italian},
                new Restaurant {Id = 2, Name = "Jersey Mikes", Location="Virginia", Cuisine=CuisineType.None},
                new Restaurant {Id = 3, Name = "Mint", Location="Virginia", Cuisine=CuisineType.Indian}
            };

        }
        public IEnumerable<Restaurant> GetAll()
        {
            return from r in restaurants
                   orderby r.Name
                   select r;


        }
        public IEnumerable<Restaurant>GetRestaurantsByName(string searchTerm)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(searchTerm) || r.Name.StartsWith(searchTerm)
                   orderby r.Name
                   select r;


        }
        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }
    }
}
