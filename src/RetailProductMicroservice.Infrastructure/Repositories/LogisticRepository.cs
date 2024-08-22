using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.Interfaces;

namespace RetailProductMicroservice.Infrastructure.Repositories
{
    public class LogisticRepository : ILogisticRepository
    {
        private readonly RetailProductContext _context;

        public LogisticRepository(RetailProductContext context)
        {
            _context = context;
        }

        public IEnumerable<Logistic> GetAllLogistics()
        {
            return _context.Logistics.ToList();
        }

        public Logistic GetLogisticById(int id)
        {
            return _context.Logistics.FirstOrDefault(l => l.Id == id);
        }

        public void AddLogistic(Logistic logistic)
        {
            _context.Logistics.Add(logistic);
            _context.SaveChanges();
        }

        public void UpdateLogistic(Logistic logistic)
        {
            _context.Entry(logistic).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteLogistic(int id)
        {
            var logistic = _context.Logistics.Find(id);
            if (logistic != null)
            {
                _context.Logistics.Remove(logistic);
                _context.SaveChanges();
            }
        }
    }
}