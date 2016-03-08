﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL
{
    public class StrafRepository:IStrafRepository
    {
        private readonly ProjectContext _context;

        public StrafRepository(ProjectContext context)
        {
            _context = context;
        }

        public IQueryable<Straf> FindAll()
        {
            return _context.StrafSet.OrderBy(s => s.Naam);
        }

        public Straf FindById(int id)
        {
            return _context.StrafSet.FirstOrDefault(s => s.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}