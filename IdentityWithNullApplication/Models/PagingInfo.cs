﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityWithNullApplication.Models
{
    public class PagingInfo
    {
        // Кол-во новостей
        public int TotalItems { get; set; }

        // Кол-во новостей на одной странице
        public int ItemsPerPage { get; set; }

        // Номер текущей страницы
        public int CurrentPage { get; set; }

        // Общее кол-во страниц
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}