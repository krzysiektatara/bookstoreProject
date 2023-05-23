﻿using BookStoreApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApplicationAPI.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<ActionResult<T>> Geta(int id);
        Task<T> Delete(int id);
    }
}
