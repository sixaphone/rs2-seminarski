﻿using Source.net.infrastructure.Dtos;
using Source.net.infrastructure.Entities;
using Source.net.infrastructure.SearchFilters;
using Source.net.infrastructure.Views;
using System.Collections.Generic;

namespace Source.net.services.Services.Interfaces
{
    public interface CategoryService : 
        BaseService<Category, CategoryDto, CategoryDto, CategoryView, CategoryFilter>
    {
    }
}