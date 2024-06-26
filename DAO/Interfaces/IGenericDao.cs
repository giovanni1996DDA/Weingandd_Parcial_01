﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interfaces
{
    public interface IGenericDao<T>
    {
        Guid Add(T obj);
        Guid Update(T obj);
        bool Remove(T obj);
        bool Exists(T obj);
        T GetById(Guid id);
        List<T> GetAll();
    }
}
