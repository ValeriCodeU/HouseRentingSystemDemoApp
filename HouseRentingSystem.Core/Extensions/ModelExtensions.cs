﻿using HouseRentingSystem.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HouseRentingSystem.Core.Extensions
{
    public static class ModelExtensions
    {
        public static string GetInformation(this IHouseModel house)
        {

           
            StringBuilder sb = new StringBuilder();
            sb.Append(house.Title.Replace(" ", "-"));
            sb.Append("-");
            sb.Append(GetAddress(house.Address));

            return sb.ToString().TrimEnd();
            
        }

        private static string GetAddress(string address)
        {
            string result = String.Join("-", address.Split(" ", StringSplitOptions.RemoveEmptyEntries).Take(3));

            return Regex.Replace(result, @"[^a-zA-Z0-9\-]", string.Empty);
        }
    }
}
